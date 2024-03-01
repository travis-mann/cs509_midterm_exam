using System.Text.RegularExpressions;

internal class CreateNewAccountMenuOption : ICreateNewAccountMenuOption
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
        int pin = GetPin();
        string name = GetName();
        int balance = GetBalance();
        string status = GetStatus();

        try
        {
            int newUserID = _UserDAL.CreateUser(login, pin, name, status, "customer");
            int newAccountID = _AccountDAL.CreateAccount(newUserID, (int)_StatusDAL.getStatusID(status), balance);
            Console.WriteLine($"Account Successfully Created – the account number assigned is: {newAccountID}");
        }
        catch (DuplicateUserException)
        {
            Console.WriteLine($"ERROR: User {login} already exists.");
        }
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
            input => _StatusDAL.getStatusID(input) != null,
            "Status: "
            );
    }
}
