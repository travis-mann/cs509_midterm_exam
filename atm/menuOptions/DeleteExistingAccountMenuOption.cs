using System.Text.RegularExpressions;
using Atm.Dal;

internal sealed class DeleteExistingAccountMenuOption : IDeleteExistingAccountMenuOption
{
    public string Name { get; } = "Delete Existing Account";
    private IInputGetter _InputGetter;
    private IUserDAL _UserDAL;
    private IAccountDAL _AccountDAL;
    private IRegexConstants _RegexConstants;

    public DeleteExistingAccountMenuOption(IInputGetter InputGetter, IAccountDAL AccountDAL, IUserDAL UserDAL, IStatusDAL StatusDAL, IRegexConstants RegexConstants) 
    {
        _InputGetter = InputGetter;
        _UserDAL = UserDAL;
        _AccountDAL = AccountDAL;
        _RegexConstants = RegexConstants;
    }

    public void Run(int userID)
    {
        // get account number and customer to delete
        int accountID = GetAccountID("Enter the account number to which you want to delete: ");
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

        // confirm deletion
        int confirmAccountID = GetAccountID($"You wish to delete the account held by {_UserDAL.GetUserName(custUserID)}. If this information is correct, please re-enter the account number: ");

        if (accountID == confirmAccountID)
        {
            _AccountDAL.DeleteAccount(accountID);
            _UserDAL.DeleteUser(custUserID);
            Console.WriteLine("Account Deleted Successfully");
        }
        else
        {
            Console.WriteLine("ERROR: First and second account IDs did not match. Delete operation canceled.");
        }
    }

    private int GetAccountID(string prompt)
    {
        return Convert.ToInt32(_InputGetter.GetInput(
            input => new Regex(_RegexConstants.accountID).Match(input).Success,
            prompt
            ));
    }
}
