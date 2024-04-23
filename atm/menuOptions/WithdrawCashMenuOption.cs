using System.Text.RegularExpressions;

internal sealed class WithdrawCashMenuOption : IWithdrawCashMenuOption
{
    public string Name { get; } = "Withdraw Cash";

    private IInputGetter _InputGetter;
    private IAccountDAL _AccountDAL;
    private IRegexConstants _RegexConstants;

    public WithdrawCashMenuOption(IInputGetter InputGetter, IAccountDAL AccountDAL, IRegexConstants RegexConstants) 
    {
        _InputGetter = InputGetter;
        _AccountDAL = AccountDAL;
        _RegexConstants = RegexConstants;
    }

    public void Run(int user_id)
    {
        // get input
        int amount = Convert.ToInt32(_InputGetter.GetInput(isValidInput, "Enter the withdrawal amount: ")) * -1;
        int account_id = _AccountDAL.GetAccountIDFromUserID(user_id);

        // remove from account
        int amountWithdrawn;
        try
        {
            _AccountDAL.UpdateBalance(amount, account_id);
            Console.WriteLine("Cash Successfully Withdrawn");
            amountWithdrawn = Math.Abs(amount);
        }
        catch (InvalidBalanceUpdateException)
        {
            Console.WriteLine("ERROR: Invalid withdrawal amount");
            amountWithdrawn = 0;
        }

        // display updated account details
        Console.WriteLine($"Account #{account_id}");
        Console.WriteLine($"Date: {GetTodaysDateString()}");
        Console.WriteLine($"Withdrawn: {amountWithdrawn}");
        Console.WriteLine($"Balance: {_AccountDAL.GetBalance(account_id)}");
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
