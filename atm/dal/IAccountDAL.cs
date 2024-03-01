public interface IAccountDAL
{
    public int CreateAccount(int userID, int statusID, int balance);

    public int UpdateBalance(int amountToAdd, int accountID);

    public int GetBalance(int accountID);

    public int GetAccountIDFromUserID(int userID);
}

