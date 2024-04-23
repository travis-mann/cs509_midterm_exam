namespace Atm.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class Context : DbContext
{
    public DbSet<AccountRepository> Account { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        optionsBuilder.UseMySQL(connectionString);
    }
}
