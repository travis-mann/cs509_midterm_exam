using System.Text.RegularExpressions;

internal class WithdrawCashMenuOption : IWithdrawCashMenuOption
{
    public string Name { get; } = "Withdraw Cash";

    private IInputGetter _InputGetter;
    private IAccountDAL _AccountDAL;

    public WithdrawCashMenuOption(IInputGetter InputGetter, IAccountDAL AccountDAL) 
    {
        _InputGetter = InputGetter;
        _AccountDAL = AccountDAL;
    }

    public void Run(int user_id)
    {
        // get input
        int amount = Convert.ToInt32(_InputGetter.GetInput(isValidInput, "amount to withdraw")) * -1;
        int account_id = _AccountDAL.GetAccountIDFromUser(user_id);

        // remove from account
        try
        {
            _AccountDAL.UpdateBalance(amount, account_id);
            Console.WriteLine("Cash Successfully Withdrawn");
        }
        catch (InvalidBalanceUpdateException)
        {
            Console.WriteLine("ERROR: Invalid withdrawl amount");
        }

        // display updated account details
        Console.WriteLine($"Account #{account_id}");
        Console.WriteLine($"Date: {GetTodaysDateString()}");
        Console.WriteLine($"Account #{account_id}");
        Console.WriteLine($"Balance: {_AccountDAL.GetBalance(account_id)}");
    }

    private static string GetTodaysDateString()
    {
        return DateTime.Now.ToString("M/d/yyyy");
    }

    private static bool isValidInput(string input)
    {
        if (!new Regex("[0-9]+").Match(input).Success)
            return false;
        return true;
    }
}
