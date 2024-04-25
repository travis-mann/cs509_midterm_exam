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
    private readonly IContextFactory contextFactory;

    public AccountDAL(IContextFactory contextFactory) => this.contextFactory = contextFactory;

    public int CreateAccount(string login, int pin, string name, string role, string status, int balance)
    {
        using var context = this.contextFactory.CreateContext();
        AccountRepository account = new(login, pin, name, role, status, balance);
        _ = context.Account.Add(account);
        _ = context.SaveChanges();
        return account.Id;
    }

    public int UpdateBalance(int amountToAdd, int accountId)
    {
        using var context = this.contextFactory.CreateContext();
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
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        return account.Balance;
    }

    public int DeleteAccount(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        _ = context.Account.Remove(account);
        _ = context.SaveChanges();
        return account.Id;
    }

    public bool IsValidLogin(string login) => this.GetAccountIdFromLogin(login) != 0;

    public bool IsValidAccount(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
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

    public bool IsValidPin(string login, int pin)
    {
        using var context = this.contextFactory.CreateContext();
        var accountId = this.GetAccountIdFromLogin(login);
        return accountId != 0 && this.GetPin(accountId) == pin;
    }

    public bool IsAdmin(int accountId) => this.GetRole(accountId) == "admin";

    public int GetAccountIdFromLogin(string login)
    {
        using var context = this.contextFactory.CreateContext();
        return context.Account.Where(a => a.Login == login).Select(a => a.Id).SingleOrDefault();
    }

    public string GetUserLogin(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Login;
    }

    public string GetUserName(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Name;
    }

    public string GetStatus(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Status;
    }

    public string GetRole(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Role;
    }

    public int GetPin(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Pin;
    }

    public int UpdateUserName(int accountId, string name)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        account.Name = name;
        _ = context.SaveChanges();
        return account.Id;
    }

    public int UpdateUserStatus(int accountId, string status)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        account.Status = status;
        _ = context.SaveChanges();
        return account.Id;
    }

    public int UpdateUserLogin(int accountId, string login)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        account.Login = login;
        _ = context.SaveChanges();
        return account.Id;
    }

    public int UpdateUserPin(int accountId, int pin)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        account.Pin = pin;
        _ = context.SaveChanges();
        return account.Id;
    }

    private static AccountRepository GetAccount(int accountId, Context context)
    {
        try
        {
            return context.Account.Single(a => a.Id == accountId);
        }
        catch (InvalidOperationException)
        {
            throw new AccountNotFoundException(accountId);
        }
    }
}
