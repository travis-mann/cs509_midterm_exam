namespace Atm.UserMenu;
using System.Text.RegularExpressions;
using Atm.Dal;
using Atm.Common;
using System.Globalization;

internal sealed class SearchForAccountMenuOption : IMenuOption
{
    public string Name { get; } = "Search for Account";

    public SearchForAccountMenuOption() { }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        // get account number and customer to display
        var custAccountId = GetAccountID("Enter account number: ", inputGetter);
        if (!accountDAL.IsValidAccount(custAccountId))
        {
            Console.WriteLine($"ERROR: Account {custAccountId} does not exist. Delete operation canceled.");
            return;
        }

        // display info
        DisplayAccountInfo(custAccountId, accountDAL);
    }

    internal static int GetAccountID(string prompt, IInputGetter inputGetter) => Convert.ToInt32(inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.AccountID).Match(input).Success,
            prompt
            ), new CultureInfo("en-US"));

    private static void DisplayAccountInfo(int accountID, IAccountDAL accountDAL)
    {
        Console.WriteLine($"Account # {accountID}");
        Console.WriteLine($"Holder: {accountDAL.GetUserName(accountID)}");
        Console.WriteLine($"Balance: {accountDAL.GetBalance(accountID)}");
        Console.WriteLine($"Status: {accountDAL.GetStatus(accountID)}");
        Console.WriteLine($"Login: {accountDAL.GetUserLogin(accountID)}");
        Console.WriteLine($"Pin Code: {accountDAL.GetPin(accountID)}");
    }
}
