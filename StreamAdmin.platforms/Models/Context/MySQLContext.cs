using Microsoft.EntityFrameworkCore;

namespace StreamAdmin.Catalog.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(){}
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options){}

        public DbSet<StreamingPlatform> StreamingPlatforms { get; set; }
        public DbSet<StreamingPlan> StreamingPlans { get; set; }
    }
}
