using Microsoft.EntityFrameworkCore;

namespace StreamAdmin.Catalog.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<StreamingPlatform> StreamingPlatforms => Set<StreamingPlatform>();
        public DbSet<StreamingPlan> StreamingPlans => Set<StreamingPlan>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StreamingPlatform>()
                .HasMany(platform => platform.Plans)
                .WithOne(plan => plan.StreamingPlatform)
                .HasForeignKey(plan => plan.StreamingPlatformId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StreamingPlan>(entity =>
            {
                entity.Property(plan => plan.ReferencePrice).HasPrecision(10, 2);
                entity.Property(plan => plan.Currency).HasMaxLength(3).HasColumnType("varchar(3)");
                entity.Property(plan => plan.MaximumResolution).HasMaxLength(30).HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<StreamingPlatform>().HasData(
                new { Id = 1L, Name = "Netflix", Description = "Serviço de streaming de filmes, séries e jogos.", WebSiteUrl = "https://www.netflix.com/br/", IsActive = true },
                new { Id = 2L, Name = "Disney+", Description = "Serviço de streaming da Disney, Pixar, Marvel, Star Wars, National Geographic e ESPN.", WebSiteUrl = "https://www.disneyplus.com/pt-br", IsActive = true },
                new { Id = 3L, Name = "HBO Max", Description = "Serviço de streaming da Warner Bros. Discovery.", WebSiteUrl = "https://www.max.com/br/pt", IsActive = true },
                new { Id = 4L, Name = "Prime Video", Description = "Serviço de streaming incluído na assinatura Amazon Prime.", WebSiteUrl = "https://www.primevideo.com/", IsActive = true });

            modelBuilder.Entity<StreamingPlan>().HasData(
                new { Id = 1L, StreamingPlatformId = 1L, Name = "Padrão com anúncios", Description = "Plano com anúncios e resolução Full HD.", ReferencePrice = 20.90m, Currency = "BRL", MaximumScreens = (int?)2, MaximumResolution = "Full HD", HasAds = true, AllowsDownloads = true, IsActive = true },
                new { Id = 2L, StreamingPlatformId = 1L, Name = "Padrão", Description = "Plano sem anúncios e resolução Full HD.", ReferencePrice = 44.90m, Currency = "BRL", MaximumScreens = (int?)2, MaximumResolution = "Full HD", HasAds = false, AllowsDownloads = true, IsActive = true },
                new { Id = 3L, StreamingPlatformId = 1L, Name = "Premium", Description = "Plano sem anúncios com resolução 4K e HDR.", ReferencePrice = 59.90m, Currency = "BRL", MaximumScreens = (int?)4, MaximumResolution = "4K + HDR", HasAds = false, AllowsDownloads = true, IsActive = true },
                new { Id = 4L, StreamingPlatformId = 2L, Name = "Padrão com anúncios", Description = "Plano com anúncios e resolução Full HD.", ReferencePrice = 29.90m, Currency = "BRL", MaximumScreens = (int?)2, MaximumResolution = "Full HD", HasAds = true, AllowsDownloads = false, IsActive = true },
                new { Id = 5L, StreamingPlatformId = 2L, Name = "Padrão", Description = "Plano sem intervalos comerciais e resolução Full HD.", ReferencePrice = 49.90m, Currency = "BRL", MaximumScreens = (int?)2, MaximumResolution = "Full HD", HasAds = false, AllowsDownloads = true, IsActive = true },
                new { Id = 6L, StreamingPlatformId = 2L, Name = "Premium", Description = "Plano sem intervalos comerciais com resolução 4K UHD e HDR.", ReferencePrice = 69.90m, Currency = "BRL", MaximumScreens = (int?)4, MaximumResolution = "4K UHD/HDR", HasAds = false, AllowsDownloads = true, IsActive = true },
                new { Id = 7L, StreamingPlatformId = 3L, Name = "Básico com anúncios", Description = "Plano com anúncios e resolução Full HD.", ReferencePrice = 29.90m, Currency = "BRL", MaximumScreens = (int?)2, MaximumResolution = "Full HD", HasAds = true, AllowsDownloads = false, IsActive = true },
                new { Id = 8L, StreamingPlatformId = 3L, Name = "Standard", Description = "Plano com resolução Full HD e downloads para visualização offline.", ReferencePrice = 39.90m, Currency = "BRL", MaximumScreens = (int?)2, MaximumResolution = "Full HD", HasAds = false, AllowsDownloads = true, IsActive = true },
                new { Id = 9L, StreamingPlatformId = 3L, Name = "Platinum", Description = "Plano com resolução 4K UHD e downloads para visualização offline.", ReferencePrice = 55.90m, Currency = "BRL", MaximumScreens = (int?)4, MaximumResolution = "4K UHD", HasAds = false, AllowsDownloads = true, IsActive = true },
                new { Id = 10L, StreamingPlatformId = 4L, Name = "Amazon Prime", Description = "Plano Amazon Prime com acesso ao catálogo do Prime Video.", ReferencePrice = 19.90m, Currency = "BRL", MaximumScreens = (int?)3, MaximumResolution = "4K UHD", HasAds = true, AllowsDownloads = true, IsActive = true });
        }
    }
}
