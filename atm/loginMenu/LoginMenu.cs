namespace Atm.LoginMenu;
using System.Globalization;
using System.Text.RegularExpressions;
using Atm.Common;
using Atm.Dal;

/// <summary>
/// Manages user login for the ATM
/// </summary>
public class LoginMenu : ILoginMenu
{
    private readonly IInputGetter inputGetter;
    private readonly IAccountDAL accountDAL;

    /// <summary>
    /// Constructor for LoginMenu class
    /// </summary>
    /// <param name="inputGetter">Class to manage getting user input</param>
    /// <param name="accountDAL">Class to manage accessing data from persistant storge layer</param>
    public LoginMenu(IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        this.inputGetter = inputGetter;
        this.accountDAL = accountDAL;
    }

    /// <summary>
    /// Runs login process for the ATM
    /// </summary>
    /// <returns>accountId and isAdmin flag</returns>
    public (int, bool) Login()
    {
        int? accountId = null;
        while (accountId == null)
        {
            accountId = LoginAttemptHandler(this.inputGetter, this.accountDAL);
        }
        Console.WriteLine("login success!");
        return ((int)accountId, this.accountDAL.IsAdmin((int)accountId));
    }

    internal static int? LoginAttemptHandler(IInputGetter inputGetter, IAccountDAL accountDAL)
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
