# StreamAdminApp

# Contexto do projeto: StreamAdmin

Estou desenvolvendo uma aplicação chamada **StreamAdmin** com foco em estudo e aplicação prática de arquitetura de microsserviços utilizando .NET.

Apesar de o projeto ter origem educacional, meu objetivo é tratá-lo como uma aplicação real: quero tomar decisões arquiteturais justificáveis, manter boa separação de responsabilidades e evoluir o sistema gradualmente, evitando complexidade desnecessária.

## Objetivo do produto

O StreamAdmin pretende permitir que usuários gerenciem suas assinaturas de serviços de streaming.

A aplicação deve permitir, futuramente:

* visualizar serviços de streaming utilizados;
* registrar e acompanhar assinaturas;
* controlar plano contratado;
* valor mensal;
* data de início;
* data de renovação;
* cancelamento;
* status da assinatura;
* acompanhar gastos mensais;
* acompanhar utilização dos serviços;
* comparar custo x utilização;
* gerar indicadores e recomendações;
* permitir pausa/cancelamento de assinaturas;
* futuramente gerar alertas e insights.

Uma decisão importante já tomada foi separar:

* **catálogo de plataformas e planos**
* **assinaturas do usuário**
* **dados de utilização**

O usuário não deverá precisar cadastrar manualmente Netflix, Disney+, HBO Max etc. O sistema terá um catálogo base de plataformas e planos para seleção.

---

# Arquitetura

O projeto está sendo estruturado em **microsserviços**.

A intenção futura é utilizar:

* .NET;
* APIs REST;
* Entity Framework Core;
* MySQL;
* RabbitMQ para mensageria entre serviços;
* containers futuramente;
* comunicação assíncrona quando fizer sentido.

Não quero introduzir RabbitMQ ou eventos apenas para "usar microsserviços". A comunicação assíncrona deve existir quando houver uma necessidade arquitetural concreta.

O serviço no qual estou trabalhando atualmente é o:

## Catalog

Responsabilidade:

> Manter o catálogo base de plataformas de streaming e seus respectivos planos.

Exemplos:

```text
Netflix
├── Padrão com anúncios
├── Padrão
└── Premium

Disney+
├── Padrão com anúncios
├── Padrão
└── Premium
```

Outros serviços poderão futuramente consumir essas informações.

---

# Entidades principais do Catalog

As entidades persistidas atualmente são:

## StreamingPlatform

Conceitualmente:

```csharp
public long Id { get; set; }
public string Name { get; set; }
public string Description { get; set; }
public string WebSiteUrl { get; set; }
public bool IsActive { get; set; }
public ICollection<StreamingPlan> Plans { get; set; }
```

Relacionamento:

```text
StreamingPlatform 1:N StreamingPlan
```

Uma plataforma possui vários planos.

---

## StreamingPlan

Conceitualmente:

```csharp
public long Id { get; set; }

public long StreamingPlatformId { get; set; }
public StreamingPlatform StreamingPlatform { get; set; }

public string Name { get; set; }
public string Description { get; set; }

public decimal ReferencePrice { get; set; }
public string Currency { get; set; }

public int? MaximumScreens { get; set; }
public string MaximumResolution { get; set; }

public bool HasAds { get; set; }
public bool AllowsDownloads { get; set; }
public bool IsActive { get; set; }
```

`StreamingPlatformId` é FK de `StreamingPlan`.

---

# ReferencePrice

A propriedade foi propositalmente chamada:

```csharp
ReferencePrice
```

e não simplesmente `Price`.

A razão é que o Catalog representa o valor conhecido/referencial do plano, enquanto uma assinatura real poderá ter:

* desconto;
* promoção;
* preço antigo;
* pacote;
* cobrança diferenciada;
* reajuste;
* benefício de operadora.

Portanto, não renomear para `Price` sem uma justificativa arquitetural forte.

Uma evolução que quero considerar é:

```csharp
public DateTime PriceLastUpdatedAt { get; set; }
```

para saber quando o preço de referência foi atualizado.

---

# Banco

Banco atual:

```text
MySQL
```

ORM:

```text
Entity Framework Core
```

Context:

```csharp
MySQLContext : DbContext
```

DbSets existentes:

```csharp
public DbSet<StreamingPlatform> StreamingPlatforms { get; set; }
public DbSet<StreamingPlan> StreamingPlans { get; set; }
```

O relacionamento esperado é:

```csharp
modelBuilder.Entity<StreamingPlatform>()
    .HasMany(x => x.Plans)
    .WithOne(x => x.StreamingPlatform)
    .HasForeignKey(x => x.StreamingPlatformId);
```

Antes de alterar esse mapeamento, verifique como ele está realmente configurado no repositório.

---

# Seed

Estou atualmente implementando o seed inicial do Catalog.

Quero inicialmente quatro plataformas:

```text
Id 1 - Netflix
Id 2 - Disney+
Id 3 - HBO Max
Id 4 - Prime Video
```

A organização planejada dos planos é:

```text
Netflix
1 - Padrão com anúncios
2 - Padrão
3 - Premium

Disney+
4 - Padrão com anúncios
5 - Padrão
6 - Premium

HBO Max
7 - Básico com anúncios
8 - Standard
9 - Platinum

Prime Video
10 - Amazon Prime
```

Os planos devem ser seedados separadamente das plataformas.

Não fazer:

```csharp
StreamingPlatform.HasData(
    new StreamingPlatform
    {
        Plans = ...
    }
);
```

Preferir:

```csharp
modelBuilder.Entity<StreamingPlatform>().HasData(...);

modelBuilder.Entity<StreamingPlan>().HasData(
    new StreamingPlan
    {
        StreamingPlatformId = 1,
        ...
    }
);
```

---

# Estado atual do banco

Antes do seed definitivo, foram inseridos manualmente registros experimentais.

Existe ou existiu algo semelhante a:

```text
StreamingPlatform

Id: 1
Name: CineWave+
```

e um plano semelhante a:

```text
Id: 2
StreamingPlatformId: 1
Name: Premium
ReferencePrice: 49.90
MaximumScreens: 4
MaximumResolution: 4K
HasAds: false
AllowsDownloads: true
```

Esses registros eram apenas testes.

Antes de aplicar migrations contendo `HasData`, verificar se esses IDs continuam existentes no banco.

Não assumir que o EF irá reconciliar automaticamente dados manuais com `InsertData` das migrations.

Se houver conflito de PK, indicar a solução antes de executar a migration.

---

# Tipos importantes

Quero que:

```csharp
ReferencePrice
```

seja armazenado aproximadamente como:

```text
DECIMAL(10,2)
```

No EF:

```csharp
.HasPrecision(10, 2)
```

O banco atualmente apresentou valores semelhantes a:

```text
49.900000000000000000000000000000
```

o que indica escala desnecessariamente alta.

Também verificar o tipo das colunas:

```text
Currency
MaximumResolution
```

porque os resultados do banco apresentaram padding semelhante a:

```text
"BRL          "
"4K                    "
```

Pode existir `CHAR` onde seria mais apropriado usar `VARCHAR`.

Expectativa aproximada:

```text
Currency            VARCHAR(3)
MaximumResolution   VARCHAR(30)
```

Não altere sem antes conferir o mapeamento real e a migration atual.

---

# VOs

Existem classes como:

```text
PlatformVO
PlanVO
```

O objetivo original ao introduzir VOs foi evitar exposição direta das entidades de persistência.

Estou estudando e revisando a diferença entre:

* Entity;
* DTO;
* VO;
* objetos de request/response.

Não assuma que a nomenclatura atual está necessariamente correta.

Se `PlatformVO` ou `PlanVO` estiverem sendo utilizados apenas para transportar dados entre API e aplicação, analise se conceitualmente deveriam ser DTOs.

Entretanto:

**não faça uma refatoração massiva apenas por nomenclatura.**

Primeiro explique:

1. o problema;
2. o impacto;
3. a alternativa;
4. se vale a pena corrigir agora.

---

# Minha intenção de aprendizado

Além de concluir o projeto, quero entender as decisões.

Quando identificar alguma decisão arquitetural relevante, explique brevemente:

```text
O que está acontecendo?
Por que isso é um problema?
Qual alternativa você recomenda?
Por que essa alternativa é melhor neste contexto?
```

Evite transformar cada alteração simples em uma aula extensa.

Quero explicações principalmente quando houver:

* arquitetura;
* modelagem;
* EF Core;
* relacionamentos;
* microsserviços;
* mensageria;
* concorrência;
* consistência;
* contratos entre serviços;
* abstrações importantes.

---

# Filosofia de desenvolvimento

Priorizar:

* código simples;
* legibilidade;
* baixo acoplamento;
* responsabilidade bem definida;
* evolução incremental;
* decisões justificáveis;
* YAGNI;
* evitar overengineering.

Não introduzir automaticamente:

* CQRS;
* Event Sourcing;
* MediatR;
* Repository genérico;
* Unit of Work customizado;
* abstrações adicionais;
* padrões complexos;

apenas porque são comuns em projetos de microsserviços.

Se algum deles resolver um problema real do StreamAdmin, apresente a justificativa antes.

---

# RabbitMQ

RabbitMQ será estudado e utilizado futuramente.

Não introduza mensageria no Catalog apenas para demonstrar RabbitMQ.

Quando houver integração entre microsserviços, quero avaliar primeiro se ela deve ser:

```text
síncrona
```

ou:

```text
assíncrona
```

e por quê.

Eventos devem representar acontecimentos relevantes de domínio, por exemplo futuramente algo semelhante a:

```text
SubscriptionCreated
SubscriptionCancelled
SubscriptionRenewed
```

em vez de simplesmente transformar cada CRUD em evento.

---

# Forma de trabalhar comigo

Quando eu pedir análise, **não modifique código imediatamente**.

Primeiro:

1. leia os arquivos relacionados;
2. entenda a implementação existente;
3. identifique problemas;
4. apresente as alterações propostas;
5. indique impacto;
6. espere minha autorização quando a alteração for relevante.

Quando eu disser explicitamente algo como:

```text
implemente
corrija
prossiga
pode alterar
```

você pode modificar os arquivos.

Depois de modificações relevantes:

* execute `dotnet build`;
* execute testes existentes quando aplicável;
* informe o resultado;
* mostre ou resuma o `git diff`.

Não esconda warnings relevantes.

Não corrija código não relacionado ao objetivo atual sem me avisar.

---

# Fonte da verdade

Este documento representa contexto e decisões anteriores, mas o **repositório atual é a fonte da verdade sobre a implementação**.

Se houver divergência entre este contexto e o código:

1. identifique a divergência;
2. não presuma qual lado está correto;
3. explique;
4. proponha a correção.

Não crie classes que já existam apenas porque não foram mencionadas aqui.

Pesquise o projeto antes.

---

# Primeira tarefa

Antes de alterar qualquer arquivo:

Analise a implementação atual do microsserviço Catalog.

Quero que você verifique principalmente:

* estrutura do projeto;
* `StreamingPlatform`;
* `StreamingPlan`;
* `PlatformVO`;
* `PlanVO`;
* `MySQLContext`;
* configurações do Entity Framework;
* relacionamento Platform x Plan;
* migrations existentes;
* seed atual;
* tipos das colunas MySQL;
* configuração de `ReferencePrice`;
* possíveis conflitos entre dados manuais existentes e o novo seed.

Depois me entregue:

### 1. O que está correto

Identifique o que deve ser mantido.

### 2. Problemas encontrados

Classifique quando possível em:

```text
erro
risco
melhoria
opcional
```

### 3. Correções recomendadas

Explique quais mudanças faria agora.

### 4. Melhorias futuras

Separe aquilo que não precisa ser resolvido neste momento.

### 5. Plano de alteração

Informe quais arquivos pretende modificar.

**Não altere nenhum arquivo nessa primeira análise.**
