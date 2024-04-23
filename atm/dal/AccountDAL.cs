namespace Atm.Dal;
using Atm.Data;

internal class AccountNotFoundException : Exception
{
    public AccountNotFoundException(int accountId)
        : base($"ERROR: Account #{accountId} not found")
    {

    }
}

internal class InvalidBalanceUpdateException : Exception
{
    public InvalidBalanceUpdateException(int balance, int amount, int accountID)
        : base($"ERROR: Unable to add ${amount} from Account #{accountID} with total balance ${balance}")
    {

    }
}

public class AccountDAL : IAccountDAL
{

    public int CreateAccount(string login, int pin, string name, string role, string status, int balance)
    {
        using Context context = new();
        AccountRepository account = new(login, pin, name, role, status, balance);
        _ = context.Account.Add(account);
        _ = context.SaveChanges();
        return account.Id;
    }

    public int UpdateBalance(int amountToAdd, int accountId)
    {
        using Context context = new();
        var account = GetAccount(accountId, context);
        if (account.Balance + amountToAdd < 0)
        {
            throw new InvalidBalanceUpdateException(account.Balance, amountToAdd, accountId);
        }
        account.Balance += amountToAdd;
        _ = context.SaveChanges();
        return account.Balance;
    }

    public int GetBalance(int accountId)
    {
        using Context context = new();
        var account = GetAccount(accountId, context);
        return account.Balance;
    }

    public int DeleteAccount(int accountId)
    {
        using Context context = new();
        var account = GetAccount(accountId, context);
        _ = context.Account.Remove(account);
        _ = context.SaveChanges();
        return account.Id;
    }

    public bool IsValidLogin(string login) => this.GetAccountIdFromLogin(login) != 0;

    public int GetAccountIdFromLogin(string login)
    {
        using Context context = new();
        return context.Account.Where(a => a.Login == login).Select(a => a.Id).SingleOrDefault();
    }

    public bool IsValidAccount(int accountId)
    {
        using Context context = new();
        try
        {
            _ = GetAccount(accountId, context);
            return true;
        }
        catch (AccountNotFoundException)
        {
            return false;
        }

    }

    public string GetUserLogin(int accountId)
    {
        using Context context = new();
        return GetAccount(accountId, context).Login;
    }

    public string GetUserName(int accountId)
    {
        using Context context = new();
        return GetAccount(accountId, context).Name;
    }

    public string GetStatus(int accountId)
    {
        using Context context = new();
        return GetAccount(accountId, context).Status;
    }

    public string GetRole(int accountId)
    {
        using Context context = new();
        return GetAccount(accountId, context).Role;
    }

    public int GetPin(int accountId)
    {
        using Context context = new();
        return GetAccount(accountId, context).Pin;
    }

    public bool IsValidPin(string login, int pin)
    {
        using Context context = new();
        var accountId = this.GetAccountIdFromLogin(login);
        return accountId != 0 && this.GetPin(accountId) == pin;
    }

    public int UpdateUserName(int accountId, string name)
    {
        using Context context = new();
        var account = GetAccount(accountId, context);
        account.Name = name;
        _ = context.SaveChanges();
        return account.Id;
    }

    public int UpdateUserStatus(int accountId, string status)
    {
        using Context context = new();
        var account = GetAccount(accountId, context);
        account.Status = status;
        _ = context.SaveChanges();
        return account.Id;
    }

    public int UpdateUserLogin(int accountId, string login)
    {
        using Context context = new();
        var account = GetAccount(accountId, context);
        account.Login = login;
        _ = context.SaveChanges();
        return account.Id;
    }

    public int UpdateUserPin(int accountId, int pin)
    {
        using Context context = new();
        var account = GetAccount(accountId, context);
        account.Pin = pin;
        _ = context.SaveChanges();
        return account.Id;
    }

    private static AccountRepository GetAccount(int accountID, Context context)
    {
        try
        {
            return context.Account.Single(a => a.Id == accountID);
        }
        catch (InvalidOperationException)
        {
            throw new AccountNotFoundException(accountID);
        }
    }
}
