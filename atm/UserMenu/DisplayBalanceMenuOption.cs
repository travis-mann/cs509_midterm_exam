namespace Atm.UserMenu;
using Atm.Dal;
using Atm.Common;
using System.Globalization;

internal sealed class DisplayBalanceMenuOption : IMenuOption
{
    public string Name { get; } = "Display Balance";

    public DisplayBalanceMenuOption() { }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        // display account details
        Console.WriteLine($"Account #{accountId}");
        Console.WriteLine($"Date: {GetTodaysDateString()}");
        Console.WriteLine($"Balance: {accountDAL.GetBalance(accountId)}");
    }

    private static string GetTodaysDateString() => DateTime.Now.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
}
