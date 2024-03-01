internal class DisplayBalanceMenuOption : IDisplayBalanceMenuOption
{
    public string Name { get; } = "Display Balance";

    private IAccountDAL _AccountDAL;

    public DisplayBalanceMenuOption(IAccountDAL AccountDAL)
    {
        _AccountDAL = AccountDAL;
    }

    public void Run(int user_id)
    {
        int account_id = _AccountDAL.GetAccountIDFromUserID(user_id);

        // display account details
        Console.WriteLine($"Account #{account_id}");
        Console.WriteLine($"Date: {GetTodaysDateString()}");
        Console.WriteLine($"Balance: {_AccountDAL.GetBalance(account_id)}");
    }

    private static string GetTodaysDateString()
    {
        return DateTime.Now.ToString("MM/dd/yyyy");
    }
}
