using System.Text.RegularExpressions;

internal class SearchForAccountMenuOption : ISearchForAccountMenuOption
{
    public string Name { get; } = "Search for Account";
    private IInputGetter _InputGetter;
    private IUserDAL _UserDAL;
    private IAccountDAL _AccountDAL;
    private IStatusDAL _StatusDAL;
    private IRegexConstants _RegexConstants;

    public SearchForAccountMenuOption(IInputGetter InputGetter, IAccountDAL AccountDAL, IUserDAL UserDAL, IStatusDAL StatusDAL, IRegexConstants RegexConstants)
    {
        _InputGetter = InputGetter;
        _UserDAL = UserDAL;
        _AccountDAL = AccountDAL;
        _StatusDAL = StatusDAL;
        _RegexConstants = RegexConstants;
    }

    public void Run(int user_id)
    {
        // get account number and customer to display
        int accountID = GetAccountID("Enter account number: ");
        int custUserID;
        try
        {
            custUserID = _AccountDAL.GetUserID(accountID);
        }
        catch (AccountNotFoundException)
        {
            Console.WriteLine($"ERROR: Account {accountID} does not exist. Delete operation canceled.");
            return;
        }

        // display info
        DisplayAccountInfo(accountID, custUserID);
    }

    private int GetAccountID(string prompt)
    {
        return Convert.ToInt32(_InputGetter.GetInput(
            input => new Regex(_RegexConstants.accountID).Match(input).Success,
            prompt
            ));
    }

    private void DisplayAccountInfo(int accountID, int custUserID)
    {
        Console.WriteLine($"Account # {accountID}");
        Console.WriteLine($"Holder: {_UserDAL.GetUserName(custUserID)}");
        Console.WriteLine($"Balance: {_AccountDAL.GetBalance(accountID)}");
        Console.WriteLine($"Status: {_StatusDAL.GetStatusFromID(_UserDAL.GetUserStatus(custUserID))}");
        Console.WriteLine($"Login: {_UserDAL.GetUserLogin(custUserID)}");
        Console.WriteLine($"Pin Code: {_UserDAL.GetUserPin(custUserID)}");
    }
}
