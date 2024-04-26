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

/// <summary>
/// MySQL data access layer control implementation for interacting with persistant data storage
/// </summary>
public class AccountDAL : IAccountDAL
{
    private readonly IContextFactory contextFactory;

    /// <summary>
    /// Constructor for AccountDal class
    /// </summary>
    /// <param name="contextFactory">class to create database contexts</param>
    public AccountDAL(IContextFactory contextFactory) => this.contextFactory = contextFactory;

    /// <summary>
    /// Creates a new account
    /// </summary>
    /// <param name="login">login for new account</param>
    /// <param name="pin">pin for new account</param>
    /// <param name="name">name for new account</param>
    /// <param name="role">role for new account</param>
    /// <param name="status">status for new account</param>
    /// <param name="balance">starting balance for new account</param>
    /// <returns></returns>
    public int CreateAccount(string login, int pin, string name, string role, string status, int balance)
    {
        using var context = this.contextFactory.CreateContext();
        AccountRepository account = new(login, pin, name, role, status, balance);
        _ = context.Account.Add(account);
        _ = context.SaveChanges();
        return account.Id;
    }

    /// <summary>
    /// Deletes an existing account
    /// </summary>
    /// <param name="accountId">Id associated with account to delete</param>
    /// <returns></returns>
    public int DeleteAccount(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        _ = context.Account.Remove(account);
        _ = context.SaveChanges();
        return account.Id;
    }

    /// <summary>
    /// Checks if a given login exists
    /// </summary>
    /// <param name="login">login to check</param>
    /// <returns></returns>
    public bool IsValidLogin(string login) => this.GetAccountIdFromLogin(login) != 0;

    /// <summary>
    /// Checks if a given pin is correct for a given login
    /// </summary>
    /// <param name="login">login to check against</param>
    /// <param name="pin">pin to check</param>
    /// <returns></returns>
    public bool IsValidPin(string login, int pin)
    {
        using var context = this.contextFactory.CreateContext();
        var accountId = this.GetAccountIdFromLogin(login);
        return accountId != 0 && this.GetPin(accountId) == pin;
    }

    /// <summary>
    /// Checks if the given accountId exists
    /// </summary>
    /// <param name="accountId">account id to check</param>
    /// <returns></returns>
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

    /// <summary>
    /// Checks if the account associated with the given account id is an admin account
    /// </summary>
    /// <param name="accountId">account id to check</param>
    /// <returns></returns>
    public bool IsAdmin(int accountId) => this.GetRole(accountId) == "admin";

    /// <summary>
    /// Returns the current balance
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve balance from</param>
    /// <returns></returns>
    public int GetBalance(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        return account.Balance;
    }

    /// <summary>
    /// Get name associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve name from</param>
    /// <returns></returns>
    public string GetUserName(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Name;
    }

    /// <summary>
    /// Get login associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve login from</param>
    /// <returns></returns>
    public string GetUserLogin(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Login;
    }

    /// <summary>
    /// Get status associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve status from</param>
    /// <returns></returns>
    public string GetStatus(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Status;
    }

    /// <summary>
    /// Get role associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve role from</param>
    /// <returns></returns>
    public string GetRole(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Role;
    }

    /// <summary>
    /// Get pin associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve pin from</param>
    /// <returns></returns>
    public int GetPin(int accountId)
    {
        using var context = this.contextFactory.CreateContext();
        return GetAccount(accountId, context).Pin;
    }

    /// <summary>
    /// Get account id associated with given login
    /// </summary>
    /// <param name="login">login for account to get id from</param>
    /// <returns></returns>
    public int GetAccountIdFromLogin(string login)
    {
        using var context = this.contextFactory.CreateContext();
        return context.Account.Where(a => a.Login == login).Select(a => a.Id).SingleOrDefault();
    }

    /// <summary>
    /// Update balance for a given account
    /// </summary>
    /// <param name="amountToAdd">amount to add to account balance</param>
    /// <param name="accountId">id associated with account to update</param>
    /// <returns></returns>
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

    /// <summary>
    /// Update name for a given account
    /// </summary>
    /// <param name="accountId">id associated with account to update</param>
    /// <param name="name">new name</param>
    /// <returns></returns>
    public int UpdateUserName(int accountId, string name)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        account.Name = name;
        _ = context.SaveChanges();
        return account.Id;
    }

    /// <summary>
    /// Update status for a given account
    /// </summary>
    /// <param name="accountId">id associated with account to update</param>
    /// <param name="status">new status</param>
    /// <returns></returns>
    public int UpdateUserStatus(int accountId, string status)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        account.Status = status;
        _ = context.SaveChanges();
        return account.Id;
    }

    /// <summary>
    /// Update login for a given account
    /// </summary>
    /// <param name="accountId">id associated with account to update</param>
    /// <param name="login">new login</param>
    /// <returns></returns>
    public int UpdateUserLogin(int accountId, string login)
    {
        using var context = this.contextFactory.CreateContext();
        var account = GetAccount(accountId, context);
        account.Login = login;
        _ = context.SaveChanges();
        return account.Id;
    }

    /// <summary>
    /// Update pin for a given account
    /// </summary>
    /// <param name="accountId">id associated with account to update</param>
    /// <param name="pin">new pin</param>
    /// <returns></returns>
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
