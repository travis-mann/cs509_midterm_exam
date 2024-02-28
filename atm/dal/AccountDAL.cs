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

    public void UpdateBalance(int AmountToAdd, int AccountID)
    {
        using (var context = new Context())
        {
            AccountRepository account = GetAccount(AccountID, context);
            if (account.balance + AmountToAdd < 0)
            {
                throw new InvalidBalanceUpdateException(account.balance, AmountToAdd, AccountID);
            }
            account.balance += AmountToAdd;
            context.SaveChanges();
        }
    }

    public int GetBalance(int AccountID)
    {
        using (var context = new Context())
        {
            AccountRepository account = GetAccount(AccountID, context);
            return account.balance;
        }
    }

    public int GetAccountIDFromUser(int UserID) 
    {
        try
        {
            using (var context = new Context())
            {
                var result = context.Account.Single(a => a.user_id == UserID);
                return result.id;
            }
        }
        catch (InvalidOperationException)
        {
            throw new AccountNotFoundException();
        }
    }

    private AccountRepository GetAccount(int AccountID, Context context)
    {
        try
        {
            return context.Account.Single(a => a.id == AccountID);
        }
        catch (InvalidOperationException)
        {
            throw new AccountNotFoundException(AccountID);
        }
    }
}

