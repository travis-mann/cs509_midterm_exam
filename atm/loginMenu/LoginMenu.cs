namespace Atm.LoginMenu;
using Atm.Dal;
using Atm.Common;
using System.Text.RegularExpressions;
using System.Globalization;

public class LoginMenu : ILoginMenu
{
    private readonly IInputGetter inputGetter;
    private readonly IAccountDAL accountDAL;

    public LoginMenu(IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        this.inputGetter = inputGetter;
        this.accountDAL = accountDAL;
    }

    public int Login()
    {
        int? accountId = null;
        while (accountId == null)
        {
            accountId = LoginAttemptHandler(this.inputGetter, this.accountDAL);
        }
        Console.WriteLine("login success!");
        return (int)accountId;
    }

    private static int? LoginAttemptHandler(IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        var login = inputGetter.GetInput(input => new Regex(inputGetter.RegexConstants.Login).Match(input).Success, "Enter login: ");
        if (!accountDAL.IsValidLogin(login))
        {
            Console.WriteLine($"ERROR: Username \"{login}\" not found, try again.");
            return null;
        }

        int pin = Convert.ToInt16(inputGetter.GetInput(input => new Regex(inputGetter.RegexConstants.Pin).Match(input).Success, "Enter pin: "), new CultureInfo("en-US"));
        if (!accountDAL.IsValidPin(login, pin))
        {
            Console.WriteLine($"ERROR: Invalid pin");
            return null;
        }

        return accountDAL.GetAccountIdFromLogin(login);
    }
}
