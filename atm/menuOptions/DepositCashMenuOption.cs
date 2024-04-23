namespace Atm.MenuOptions;
using System.Text.RegularExpressions;
using Atm.Dal;
using Atm.Common;
using System.Globalization;

internal sealed class DepositCashMenuOption : IMenuOption
{
    public string Name { get; } = "Deposit Cash";

    public DepositCashMenuOption() { }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        // get input
        var amount = Convert.ToInt32(inputGetter.GetInput((input) => IsValidInput(input, inputGetter), "Enter the cash amount to deposit:"), new CultureInfo("en-US"));

        // add to account
        var balance = accountDAL.UpdateBalance(amount, accountId);
        Console.WriteLine("Cash Deposited Successfully.");

        // display updated account details
        Console.WriteLine($"Account #{accountId}");
        Console.WriteLine($"Date: {GetTodaysDateString()}");
        Console.WriteLine($"Deposited: {amount}");
        Console.WriteLine($"Balance: {balance}");
    }

    private static string GetTodaysDateString() => DateTime.Now.ToString("MM/dd/yyyy", new CultureInfo("en-US"));

    private static bool IsValidInput(string input, IInputGetter inputGetter) => new Regex(inputGetter.RegexConstants.Balance).Match(input).Success;
}
