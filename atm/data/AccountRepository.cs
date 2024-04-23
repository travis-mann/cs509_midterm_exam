namespace Atm.Data;

public class AccountRepository
{
    public int Id { get; set; }
    public string Login { get; set; }
    public int Pin { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Status { get; set; }
    public int Balance { get; set; }

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
