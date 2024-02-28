using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class Context : DbContext
{
    public DbSet<UserRepository> User { get; set; }
    public DbSet<RoleRepository> Role { get; set; }
    public DbSet<StatusRepository> Status { get; set; }
    public DbSet<AccountRepository> Account { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        optionsBuilder.UseMySQL(connectionString);
    }
}
