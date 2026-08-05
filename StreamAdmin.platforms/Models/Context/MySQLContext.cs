using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace StreamAdmin.platforms.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(){}
        public MySQLContext(DbContextOptions<MySQLContext> options): base(options){}
    }
}
