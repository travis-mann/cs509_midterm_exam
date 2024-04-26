namespace Atm.Data;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public virtual DbSet<AccountRepository> Account { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        _ = optionsBuilder.UseMySQL(connectionString);
    }
}
