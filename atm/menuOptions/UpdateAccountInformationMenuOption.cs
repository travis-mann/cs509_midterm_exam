using System.Text.RegularExpressions;

internal sealed class UpdateAccountInformationMenuOption : IUpdateAccountInformationMenuOption
{
    public string Name { get; } = "Update Account Information";
    private IInputGetter _InputGetter;
    private IUserDALWithUpdate _UserDAL;
    private IAccountDAL _AccountDAL;
    private IStatusDAL _StatusDAL;
    private IRegexConstants _RegexConstants;

    public UpdateAccountInformationMenuOption(IInputGetter InputGetter, IAccountDAL AccountDAL, IUserDALWithUpdate UserDAL, IStatusDAL StatusDAL, IRegexConstants RegexConstants)
    {
        _InputGetter = InputGetter;
        _UserDAL = UserDAL;
        _AccountDAL = AccountDAL;
        _StatusDAL = StatusDAL;
        _RegexConstants = RegexConstants;
    }

    public void Run(int user_id)
    {
        // get account number and customer to update
        int accountID = GetAccountID("Enter the account number: ");
        int custUserID;
        try
        {
            custUserID = _AccountDAL.GetUserID(accountID);
        }
        catch (AccountNotFoundException)
        {
            Console.WriteLine($"ERROR: Account {accountID} does not exist. Update operation canceled.");
            return;
        }

        Console.WriteLine($"Starting update on account {accountID}. Press enter with no entry to skip a field.");
        UpdateName(custUserID);
        UpdateStatus(custUserID);
        UpdateLogin(custUserID);
        UpdatePin(custUserID);
    }

    private int GetAccountID(string prompt)
    {
        return Convert.ToInt32(_InputGetter.GetInput(
            input => new Regex(_RegexConstants.accountID).Match(input).Success,
            prompt
            ));
    }

    private void UpdateName(int userID)
    {
        string name = _InputGetter.GetInput(
            input => new Regex(_RegexConstants.name).Match(input).Success || input.Length == 0,
            $"Holder ({_UserDAL.GetUserName(userID)}): "
            );

        if (name.Length > 0 )
        {
            _UserDAL.UpdateUserName(userID, name);
            Console.WriteLine($"Holder updated to \"{name}\"");
            return;
        }
        Console.WriteLine("Holder skipped");
    }

    private void UpdateStatus(int userID)
    {
        string status = _InputGetter.GetInput(
            input => _StatusDAL.GetStatusID(input) != 0 || input.Length == 0,
            $"Status: "
            );

        if (status.Length > 0)
        {
            _UserDAL.UpdateUserStatus(userID, (int)_StatusDAL.GetStatusID(status));
            Console.WriteLine($"Status updated to \"{status}\"");
            return;
        }
        Console.WriteLine("Status skipped");
    }

    private void UpdateLogin(int userID)
    {
        string login = _InputGetter.GetInput(
            input => new Regex(_RegexConstants.login).Match(input).Success || input.Length == 0,
            $"Login ({_UserDAL.GetUserLogin(userID)}): "
            );

        if (login.Length > 0)
        {
            _UserDAL.UpdateUserLogin(userID, login);
            Console.WriteLine($"Login updated to \"{login}\"");
            return;
        }
        Console.WriteLine("Login skipped");
    }

    private void UpdatePin(int userID)
    {
        string pinStr = _InputGetter.GetInput(
            input => new Regex(_RegexConstants.pin).Match(input).Success || input.Length == 0,
            // not shown to keep private from admin
            $"Pin Code: "
            );

        if (pinStr.Length > 0)
        {
            _UserDAL.UpdateUserPin(userID, Convert.ToInt32(pinStr));
            Console.WriteLine($"Pin updated to \"{pinStr}\"");
            return;
        }
        Console.WriteLine("Pin skipped");
    }
}
