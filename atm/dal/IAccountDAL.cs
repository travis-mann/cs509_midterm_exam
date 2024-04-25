namespace Atm.Dal;

public interface IAccountDAL
{
    public int CreateAccount(string login, int pin, string name, string role, string status, int balance);
    public int DeleteAccount(int accountId);

    public bool IsValidLogin(string login);
    public bool IsValidPin(string login, int pin);
    public bool IsValidAccount(int accountId);
    public bool IsAdmin(int accountId);

    public int GetBalance(int accountId);
    public string GetUserName(int accountId);
    public string GetUserLogin(int accountId);
    public string GetStatus(int accountId);
    public string GetRole(int accountId);
    public int GetPin(int accountId);
    public int GetAccountIdFromLogin(string login);

    public int UpdateBalance(int amountToAdd, int accountId);
    public int UpdateUserName(int accountId, string name);
    public int UpdateUserStatus(int accountId, string status);
    public int UpdateUserLogin(int accountId, string login);
    public int UpdateUserPin(int accountId, int pin);
}
