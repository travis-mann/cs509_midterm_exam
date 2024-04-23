namespace Atm.MenuOptions;
using System.Text.RegularExpressions;
using Atm.Dal;
using Atm.Common;
using System.Globalization;

internal sealed class CreateNewAccountMenuOption : IMenuOption
{
    public string Name { get; } = "Create New Account";

    public CreateNewAccountMenuOption() { }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        var login = GetLogin(inputGetter);
        // check if user already exists
        if (accountDAL.IsValidLogin(login))
        {
            Console.WriteLine($"ERROR: User {login} already exists. Canceling create operation.");
            return;
        }

        // get remaining user details
        var pin = GetPin(inputGetter);
        var name = GetName(inputGetter);
        var balance = GetBalance(inputGetter);
        var status = GetStatus(inputGetter);

        // create new account
        var newAccountID = accountDAL.CreateAccount(login, pin, name, "customer", status, balance);
        Console.WriteLine($"Account Successfully Created – the account number assigned is: {newAccountID}");
    }

    private static string GetLogin(IInputGetter inputGetter) => inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Login).Match(input).Success,
            "Login: "
            );

    private static int GetPin(IInputGetter inputGetter) => Convert.ToInt32(inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Pin).Match(input).Success,
            "Pin Code: "
            ), new CultureInfo("en-US"));

    private static string GetName(IInputGetter inputGetter) => inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Name).Match(input).Success,
            "Holders Name: "
            );

    private static int GetBalance(IInputGetter inputGetter) => Convert.ToInt32(inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Balance).Match(input).Success,
            "Starting Balance: "
            ), new CultureInfo("en-US"));

    private static string GetStatus(IInputGetter inputGetter) => inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.Status).Match(input).Success,
            "Status: "
            );
}
