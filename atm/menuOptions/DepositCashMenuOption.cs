using System.Text.RegularExpressions;

internal class DepositCashMenuOption : IDepositCashMenuOption
{
    public string Name { get; } = "Deposit Cash";

    private IInputGetter _InputGetter;
    private IAccountDAL _AccountDAL;

    public DepositCashMenuOption(IInputGetter InputGetter, IAccountDAL AccountDAL) 
    {
        _InputGetter = InputGetter;
        _AccountDAL = AccountDAL;
    }

    public void Run(int user_id) 
    {
        // get input
        int amount = Convert.ToInt32(_InputGetter.GetInput(isValidInput, "Enter the cash amount to deposit:"));
        int account_id = _AccountDAL.GetAccountIDFromUser(user_id);

        // add to account
        _AccountDAL.UpdateBalance(amount, account_id);
        Console.WriteLine("Cash Deposited Successfully.");

        // display updated account details
        Console.WriteLine($"Account #{account_id}");
        Console.WriteLine($"Date: {GetTodaysDateString()}");
        Console.WriteLine($"Deposited {amount}");
        Console.WriteLine($"Balance: {_AccountDAL.GetBalance(account_id)}");
    }

    private static string GetTodaysDateString()
    {
        return DateTime.Now.ToString("MM/d/yyyy");
    }

    private static bool isValidInput(string input)
    {
        if (!new Regex("[0-9]+").Match(input).Success)
            return false;
        return true;
    }
}
