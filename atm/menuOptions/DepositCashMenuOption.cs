using System.Text.RegularExpressions;

internal sealed class DepositCashMenuOption : IDepositCashMenuOption
{
    public string Name { get; } = "Deposit Cash";

    private IInputGetter _InputGetter;
    private IAccountDAL _AccountDAL;
    private IRegexConstants _RegexConstants;

    public DepositCashMenuOption(IInputGetter InputGetter, IAccountDAL AccountDAL, IRegexConstants RegexConstants) 
    {
        _InputGetter = InputGetter;
        _AccountDAL = AccountDAL;
        _RegexConstants = RegexConstants;
    }

    public void Run(int user_id) 
    {
        // get input
        int amount = Convert.ToInt32(_InputGetter.GetInput(isValidInput, "Enter the cash amount to deposit:"));
        int account_id = _AccountDAL.GetAccountIDFromUserID(user_id);

        // add to account
        int balance = _AccountDAL.UpdateBalance(amount, account_id);
        Console.WriteLine("Cash Deposited Successfully.");

        // display updated account details
        Console.WriteLine($"Account #{account_id}");
        Console.WriteLine($"Date: {GetTodaysDateString()}");
        Console.WriteLine($"Deposited: {amount}");
        Console.WriteLine($"Balance: {balance}");
    }

    private static string GetTodaysDateString()
    {
        return DateTime.Now.ToString("MM/dd/yyyy");
    }

    private bool isValidInput(string input)
    {
        if (!new Regex(_RegexConstants.balance).Match(input).Success)
            return false;
        return true;
    }
}
