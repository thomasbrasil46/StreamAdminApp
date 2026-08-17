using Microsoft.EntityFrameworkCore;

namespace StreamAdmin.Catalog.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(){}
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options){}

        public DbSet<StreamingPlatform> StreamingPlatforms { get; set; }
        public DbSet<StreamingPlan> StreamingPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StreamingPlatform>().HasData(new StreamingPlatform 
            { 
                Id = 2,
                Name = "Name",
                Description = "Popular Streaming Platform",
                WebSiteUrl = "https://www.name.com",
                IsActive = true,
                Plans = new[] {
                    new StreamingPlan
                    {
                        Id = 1,
                        Name = "Basic",
                        Description = "Basic plan with limited content",
                        ReferencePrice = 9.99m,
                        Currency = "USD",
                        MaximumScreens = 1,
                        MaximumResolution = "720p",
                        HasAds = true,
                        AllowsDownloads = true,
                        IsActive = true
                    }
                }
            });
            modelBuilder.Entity<StreamingPlan>().ToTable("StreamingPlans");
        }
    }
}

//ToDo: Add more seed data for StreamingPlatform and StreamingPlan entities as needed. It is important to search and seed with real data from the streaming platforms. 