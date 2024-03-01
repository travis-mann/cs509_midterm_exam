class AccountNotFoundException: Exception
{
    public AccountNotFoundException()
    {

    }

    public AccountNotFoundException(int AccountID)
        : base($"ERROR: Account #{AccountID} not found")
    {

    }
}

class InvalidBalanceUpdateException : Exception
{
    public InvalidBalanceUpdateException(int Balance, int Amount, int AccountID)
        : base($"ERROR: Unable to add ${Amount} from Account #{AccountID } with total balance ${Balance}")
    {

    }
}

public class AccountDAL: IAccountDAL
{
    public int CreateAccount(int userID, int statusID, int balance)
    {
        using (Context context = new())
        {
            AccountRepository account = new AccountRepository(userID, statusID, balance);
            context.Account.Add(account);
            context.SaveChanges();
            return account.id;
        }
    }
    
    public int UpdateBalance(int amountToAdd, int accountID)
    {
        using (Context context = new())
        {
            AccountRepository account = GetAccount(accountID, context);
            if (account.balance + amountToAdd < 0)
            {
                throw new InvalidBalanceUpdateException(account.balance, amountToAdd, accountID);
            }
            account.balance += amountToAdd;
            context.SaveChanges();
            return account.balance;
        }
    }

    public int GetBalance(int accountID)
    {
        using (Context context = new())
        {
            AccountRepository account = GetAccount(accountID, context);
            return account.balance;
        }
    }

    public int GetUserID(int accountID)
    {
        using (Context context = new())
        {
            AccountRepository account = GetAccount(accountID, context);
            return account.user_id;
        }
    }

    public int DeleteAccount(int accountID)
    {
        using (Context context = new())
        {
            AccountRepository account = GetAccount(accountID, context);
            context.Account.Remove(account);
            context.SaveChanges();
            return account.id;
        }
    }

    public int GetAccountIDFromUserID(int userID)
    {
        using (Context context = new())
        {
            int? accountID = context.Account.Where(a => a.user_id == userID).Select(a => a.id).SingleOrDefault();
            if (accountID == 0)
            {
                throw new AccountNotFoundException();
            }
            else
            {
                return (int)accountID;
            }
        }
    }

    private AccountRepository GetAccount(int accountID, Context context)
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
