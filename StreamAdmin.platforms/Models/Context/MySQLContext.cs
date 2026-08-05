using Microsoft.EntityFrameworkCore;

namespace StreamAdmin.platforms.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(){}
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options){}
    }
}
