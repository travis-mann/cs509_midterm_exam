using Microsoft.EntityFrameworkCore;
using System.Configuration;

internal class Context : DbContext
{
    public DbSet<UserRepository> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        optionsBuilder.UseMySQL(connectionString);
    }
}
