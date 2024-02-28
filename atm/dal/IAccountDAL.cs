public interface IAccountDAL
{

    public void UpdateBalance(int AmountToAdd, int AccountID);

    public int GetBalance(int AccountID);

    public int GetAccountIDFromUser(int UserID);
}

