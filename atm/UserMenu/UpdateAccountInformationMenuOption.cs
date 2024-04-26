namespace Atm.UserMenu;
using System.Text.RegularExpressions;
using Atm.Dal;
using Atm.Common;
using System.Globalization;

internal sealed class UpdateAccountInformationMenuOption : IMenuOption
{
    public string Name { get; } = "Update Account Information";

    public UpdateAccountInformationMenuOption() { }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        // get account number and customer to update
        var custAccountId = GetAccountID("Enter the account number: ", inputGetter);
        if (!accountDAL.IsValidAccount(custAccountId))
        {
            Console.WriteLine($"ERROR: Account {custAccountId} does not exist. Update operation canceled.");
            return;
        }

        Console.WriteLine($"Starting update on account {custAccountId}. Press enter with no entry to skip a field.");
        UpdateName(custAccountId, inputGetter, accountDAL);
        UpdateStatus(custAccountId, inputGetter, accountDAL);
        UpdateLogin(custAccountId, inputGetter, accountDAL);
        UpdatePin(custAccountId, inputGetter, accountDAL);
    }

    private static int GetAccountID(string prompt, IInputGetter inputGetter) => Convert.ToInt32(inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.AccountID).Match(input).Success,
            prompt
            ), new CultureInfo("en-US"));

    private static void UpdateName(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        var name = inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Name).Match(input).Success || input.Length == 0,
            $"Holder ({accountDAL.GetUserName(accountId)}): "
            );

        if (name.Length > 0)
        {
            _ = accountDAL.UpdateUserName(accountId, name);
            Console.WriteLine($"Holder updated to \"{name}\"");
            return;
        }
        Console.WriteLine("Holder skipped");
    }

    private static void UpdateStatus(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        var status = inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Status).Match(input).Success || input.Length == 0,
            $"Status ({accountDAL.GetStatus(accountId)}): "
            );

        if (status.Length > 0)
        {
            _ = accountDAL.UpdateUserStatus(accountId, status);
            Console.WriteLine($"Status updated to \"{status}\"");
            return;
        }
        Console.WriteLine("Status skipped");
    }

    private static void UpdateLogin(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        var login = inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Login).Match(input).Success || input.Length == 0,
            $"Login ({accountDAL.GetUserLogin(accountId)}): "
            );

        if (login.Length > 0)
        {
            _ = accountDAL.UpdateUserLogin(accountId, login);
            Console.WriteLine($"Login updated to \"{login}\"");
            return;
        }
        Console.WriteLine("Login skipped");
    }

    private static void UpdatePin(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        var pinStr = inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Pin).Match(input).Success || input.Length == 0,
            // not shown to keep private from admin
            $"Pin Code: "
            );

        if (pinStr.Length > 0)
        {
            _ = accountDAL.UpdateUserPin(accountId, Convert.ToInt32(pinStr, new CultureInfo("en-US")));
            Console.WriteLine($"Pin updated to \"{pinStr}\"");
            return;
        }
        Console.WriteLine("Pin skipped");
    }
}
