namespace Atm.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class Context : DbContext
{
    public virtual DbSet<AccountRepository> Account { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        _ = optionsBuilder.UseMySQL(connectionString);
    }
}
