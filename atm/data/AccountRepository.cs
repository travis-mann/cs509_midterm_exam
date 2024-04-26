namespace Atm.Data;

/// <summary>
/// C# Entity Framework Object for account Table in MySQL
/// </summary>
public class AccountRepository
{
    /// <summary>
    /// Account Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Account Login
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Account Pin
    /// </summary>
    public int Pin { get; set; }

    /// <summary>
    /// Account Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Account Role
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Account Status
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Account Balance
    /// </summary>
    public int Balance { get; set; }

    /// <summary>
    /// Constructor for AccountRepository class
    /// </summary>
    /// <param name="login">Account Login</param>
    /// <param name="pin">Account Pin</param>
    /// <param name="name">Account Name</param>
    /// <param name="role">Account Role</param>
    /// <param name="status">Account Status</param>
    /// <param name="balance">Account Balance</param>
    public AccountRepository(string login, int pin, string name, string role, string status, int balance)
    {
        this.Login = login;
        this.Pin = pin;
        this.Name = name;
        this.Role = role;
        this.Status = status;
        this.Balance = balance;
    }
}
