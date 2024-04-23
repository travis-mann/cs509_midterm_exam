namespace Atm.MenuOptions;
using System.Text.RegularExpressions;
using Atm.Dal;
using Atm.Common;
using System.Globalization;

internal sealed class WithdrawCashMenuOption : IMenuOption
{
    public string Name { get; } = "Withdraw Cash";

    public WithdrawCashMenuOption() { }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        // get input
        var amount = Convert.ToInt32(inputGetter.GetInput((input) => IsValidInput(input, inputGetter), "Enter the withdrawal amount: "), new CultureInfo("en-US")) * -1;

        // remove from account
        int amountWithdrawn;
        try
        {
            _ = accountDAL.UpdateBalance(amount, accountId);
            Console.WriteLine("Cash Successfully Withdrawn");
            amountWithdrawn = Math.Abs(amount);
        }
        catch (InvalidBalanceUpdateException)
        {
            Console.WriteLine("ERROR: Invalid withdrawal amount");
            amountWithdrawn = 0;
        }

        // display updated account details
        Console.WriteLine($"Account #{accountId}");
        Console.WriteLine($"Date: {GetTodaysDateString()}");
        Console.WriteLine($"Withdrawn: {amountWithdrawn}");
        Console.WriteLine($"Balance: {accountDAL.GetBalance(accountId)}");
    }

    private static string GetTodaysDateString() => DateTime.Now.ToString("MM/dd/yyyy", new CultureInfo("en-US"));

    private static bool IsValidInput(string input, IInputGetter inputGetter)
    {
        if (!new Regex(inputGetter.RegexConstants.Balance).Match(input).Success)
        {
            return false;
        }

        return true;
    }
}
