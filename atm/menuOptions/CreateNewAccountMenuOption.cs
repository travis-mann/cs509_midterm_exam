using System.Text.RegularExpressions;

internal sealed class CreateNewAccountMenuOption : ICreateNewAccountMenuOption
{
    public string Name { get; } = "Create New Account";
    private IInputGetter _InputGetter;
    private IUserDAL _UserDAL;
    private IAccountDAL _AccountDAL;
    private IStatusDAL _StatusDAL;
    private IRegexConstants _RegexConstants;

    public CreateNewAccountMenuOption(IInputGetter InputGetter, IAccountDAL AccountDAL, IUserDAL UserDAL, IStatusDAL StatusDAL, IRegexConstants RegexConstants)
    {
        _InputGetter = InputGetter;
        _UserDAL = UserDAL;
        _AccountDAL = AccountDAL;
        _StatusDAL = StatusDAL;
        _RegexConstants = RegexConstants;
    }

    public void Run(int userID)
    {
        string login = GetLogin();
        // check if user already exists
        if (_UserDAL.IsValidLogin(login))
        {
            Console.WriteLine($"ERROR: User {login} already exists. Canceling create operation.");
            return;
        }

        // get remaining user details
        int pin = GetPin();
        string name = GetName();
        int balance = GetBalance();
        string status = GetStatus();

        // create new account
        int newUserID = _UserDAL.CreateUser(login, pin, name, status, "customer");
        int newAccountID = _AccountDAL.CreateAccount(newUserID, (int)_StatusDAL.GetStatusID(status), balance);
        Console.WriteLine($"Account Successfully Created – the account number assigned is: {newAccountID}");
    }

    private string GetLogin()
    {
        return _InputGetter.GetInput(
            input => new Regex(_RegexConstants.login).Match(input).Success,
            "Login: "
            );
    }

    private int GetPin()
    {
        return Convert.ToInt32(_InputGetter.GetInput(
            input => new Regex(_RegexConstants.pin).Match(input).Success,
            "Pin Code: "
            ));
    }

    private string GetName()
    {
        return _InputGetter.GetInput(
            input => new Regex(_RegexConstants.name).Match(input).Success,
            "Holders Name: "
            );
    }

    private int GetBalance()
    {
        return Convert.ToInt32(_InputGetter.GetInput(
            input => new Regex(_RegexConstants.balance).Match(input).Success,
            "Starting Balance: "
            ));
    }

    private string GetStatus()
    {
        return _InputGetter.GetInput(
            input => _StatusDAL.GetStatusID(input) != 0,
            "Status: "
            );
    }
}
