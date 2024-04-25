namespace Atm.UserMenu;
using System.Text.RegularExpressions;
using Atm.Dal;
using Atm.Common;
using System.Globalization;

internal sealed class DeleteExistingAccountMenuOption : IMenuOption
{
    public string Name { get; } = "Delete Existing Account";

    public DeleteExistingAccountMenuOption() { }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        // get account number and customer to delete
        var custAccountId = GetAccountID("Enter the account number to which you want to delete: ", inputGetter);
        if (!accountDAL.IsValidAccount(custAccountId))
        {
            Console.WriteLine($"ERROR: Account {custAccountId} does not exist. Delete operation canceled.");
            return;
        }

        // confirm deletion
        var confirmAccountID = GetAccountID($"You wish to delete the account held by {accountDAL.GetUserName(custAccountId)}. If this information is correct, please re-enter the account number: ", inputGetter);

        if (custAccountId == confirmAccountID)
        {
            accountDAL.DeleteAccount(custAccountId);
            Console.WriteLine("Account Deleted Successfully");
        }
        else
        {
            Console.WriteLine("ERROR: First and second account IDs did not match. Delete operation canceled.");
        }
    }

    private static int GetAccountID(string prompt, IInputGetter inputGetter) => Convert.ToInt32(inputGetter.GetInput(
            input => new Regex(inputGetter.RegexConstants.AccountID).Match(input).Success,
            prompt
            ), new CultureInfo("en-US"));
}
