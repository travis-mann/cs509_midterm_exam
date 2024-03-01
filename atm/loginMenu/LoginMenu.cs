using System.Text.RegularExpressions;

public class LoginMenu: ILoginMenu
{
    private IInputGetter _InputGetter;
    private IUserDAL _UserDAL;
    private IRegexConstants _RegexConstants;

    public LoginMenu(IInputGetter InputGetter, IUserDAL UserDAL, IRegexConstants RegexConstants)
    {
        _InputGetter = InputGetter;
        _UserDAL = UserDAL;
        _RegexConstants = RegexConstants;
    }

    public int Login()
    {
        int? user_id = null;
        while (user_id == null)
        {
            user_id = LoginAttemptHandler();
        }
        Console.WriteLine("login success!");
        return (int)user_id;
    }

    private int? LoginAttemptHandler()
    {

        string login = _InputGetter.GetInput(input => new Regex(_RegexConstants.login).Match(input).Success, "Enter login: ");
        if (!_UserDAL.IsValidLogin(login))
        {
            Console.WriteLine($"ERROR: Username \"{login}\" not found, try again.");
            return null;
        }

        int pin = Convert.ToInt16(_InputGetter.GetInput(input => new Regex(_RegexConstants.pin).Match(input).Success, "Enter pin: "));
        if (!_UserDAL.IsValidPin(login, pin))
        {
            Console.WriteLine($"ERROR: Invalid pin");
            return null;
        }

        return _UserDAL.GetUserID(login);
    }
}
