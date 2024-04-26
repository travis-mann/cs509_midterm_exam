namespace Atm.Data;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Manages MySQL database connections
/// </summary>
public class Context : DbContext
{
#pragma warning disable
    /// <summary>
    /// Reference to AccountRepository Table
    /// </summary>
    public virtual DbSet<AccountRepository> Account { get; set; }
#pragma warning restore

    /// <summary>
    /// Creates connection to MySQL database
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        _ = optionsBuilder.UseMySQL(connectionString);
    }
}
