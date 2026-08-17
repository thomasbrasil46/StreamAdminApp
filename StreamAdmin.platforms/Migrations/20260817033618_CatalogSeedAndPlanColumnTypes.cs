using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StreamAdmin.Catalog.Migrations
{
    /// <inheritdoc />
    public partial class CatalogSeedAndPlanColumnTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "spln_referenceprice",
                table: "streaming_plans",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "spln_name",
                table: "streaming_plans",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "spln_maximumresolution",
                table: "streaming_plans",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "stream_platforms",
                columns: new[] { "id", "sp_description", "sp_isactive", "sp_name", "sp_websiteurl" },
                values: new object[,]
                {
                    { 1L, "Serviço de streaming de filmes, séries e jogos.", true, "Netflix", "https://www.netflix.com/br/" },
                    { 2L, "Serviço de streaming da Disney, Pixar, Marvel, Star Wars, National Geographic e ESPN.", true, "Disney+", "https://www.disneyplus.com/pt-br" },
                    { 3L, "Serviço de streaming da Warner Bros. Discovery.", true, "HBO Max", "https://www.max.com/br/pt" },
                    { 4L, "Serviço de streaming incluído na assinatura Amazon Prime.", true, "Prime Video", "https://www.primevideo.com/" }
                });

            migrationBuilder.InsertData(
                table: "streaming_plans",
                columns: new[] { "id", "spln_allowsdownloads", "spln_currency", "spln_description", "spln_hasads", "spln_isactive", "spln_maximumresolution", "spln_maximumscreens", "spln_name", "spln_referenceprice", "spln_streamingplatformid" },
                values: new object[,]
                {
                    { 1L, true, "BRL", "Plano com anúncios e resolução Full HD.", true, true, "Full HD", 2, "Padrão com anúncios", 20.90m, 1L },
                    { 2L, true, "BRL", "Plano sem anúncios e resolução Full HD.", false, true, "Full HD", 2, "Padrão", 44.90m, 1L },
                    { 3L, true, "BRL", "Plano sem anúncios com resolução 4K e HDR.", false, true, "4K + HDR", 4, "Premium", 59.90m, 1L },
                    { 4L, false, "BRL", "Plano com anúncios e resolução Full HD.", true, true, "Full HD", 2, "Padrão com anúncios", 29.90m, 2L },
                    { 5L, true, "BRL", "Plano sem intervalos comerciais e resolução Full HD.", false, true, "Full HD", 2, "Padrão", 49.90m, 2L },
                    { 6L, true, "BRL", "Plano sem intervalos comerciais com resolução 4K UHD e HDR.", false, true, "4K UHD/HDR", 4, "Premium", 69.90m, 2L },
                    { 7L, false, "BRL", "Plano com anúncios e resolução Full HD.", true, true, "Full HD", 2, "Básico com anúncios", 29.90m, 3L },
                    { 8L, true, "BRL", "Plano com resolução Full HD e downloads para visualização offline.", false, true, "Full HD", 2, "Standard", 39.90m, 3L },
                    { 9L, true, "BRL", "Plano com resolução 4K UHD e downloads para visualização offline.", false, true, "4K UHD", 4, "Platinum", 55.90m, 3L },
                    { 10L, true, "BRL", "Plano Amazon Prime com acesso ao catálogo do Prime Video.", true, true, "4K UHD", 3, "Amazon Prime", 19.90m, 4L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "streaming_plans",
                keyColumn: "id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "stream_platforms",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "stream_platforms",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "stream_platforms",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "stream_platforms",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.AlterColumn<decimal>(
                name: "spln_referenceprice",
                table: "streaming_plans",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "spln_name",
                table: "streaming_plans",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "spln_maximumresolution",
                table: "streaming_plans",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
