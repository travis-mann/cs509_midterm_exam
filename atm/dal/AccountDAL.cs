namespace Atm.Dal;

internal class AccountNotFoundException : Exception
{
    public AccountNotFoundException()
    {

    }

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
    public int CreateAccount(int userID, int statusID, int balance)
    {
        using Context context = new();
        var account = new AccountRepository(userID, statusID, balance);
        _ = context.Account.Add(account);
        _ = context.SaveChanges();
        return account.id;
    }

    public int UpdateBalance(int amountToAdd, int accountID)
    {
        using Context context = new();
        var account = GetAccount(accountID, context);
        if (account.balance + amountToAdd < 0)
        {
            throw new InvalidBalanceUpdateException(account.balance, amountToAdd, accountID);
        }
        account.balance += amountToAdd;
        _ = context.SaveChanges();
        return account.balance;
    }

    public int GetBalance(int accountID)
    {
        using Context context = new();
        var account = GetAccount(accountID, context);
        return account.balance;
    }

    public int GetUserID(int accountID)
    {
        using Context context = new();
        var account = GetAccount(accountID, context);
        return account.userId;
    }

    public int DeleteAccount(int accountID)
    {
        using Context context = new();
        var account = GetAccount(accountID, context);
        _ = context.Account.Remove(account);
        _ = context.SaveChanges();
        return account.id;
    }

    public int GetAccountIDFromUserID(int userID)
    {
        using Context context = new();
        int? accountID = context.Account.Where(a => a.userId == userID).Select(a => a.id).SingleOrDefault();
        if (accountID == 0)
        {
            throw new AccountNotFoundException();
        }
        else
        {
            return (int)accountID;
        }
    }

    private static AccountRepository GetAccount(int accountID, Context context)
    {
        try
        {
            return context.Account.Single(a => a.id == accountID);
        }
        catch (InvalidOperationException)
        {
            throw new AccountNotFoundException(accountID);
        }
    }
}
