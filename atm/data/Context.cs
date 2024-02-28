using Microsoft.EntityFrameworkCore;
using System.Configuration;

internal class Context : DbContext
{
    public DbSet<UserRepository> User { get; set; }
    public DbSet<RoleRepository> Role { get; set; }
    public DbSet<StatusRepository> Status { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        optionsBuilder.UseMySQL(connectionString);
    }
}
