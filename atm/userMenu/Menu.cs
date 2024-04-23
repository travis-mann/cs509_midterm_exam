namespace Atm.UserMenu;

using System.Globalization;
using System.Text.RegularExpressions;
using Atm.Dal;
using Atm.Common;
using Atm.MenuOptions;

public class Menu : IMenu
{
    private readonly IInputGetter inputGetter;
    private readonly IAccountDAL accountDAL;

    public Menu(IInputGetter inputGetter, IAccountDAL accountDAL)
    {
        this.accountDAL = accountDAL;
        this.inputGetter = inputGetter;
    }

    public void Run(int accountId)
    {
        var menuOptions = this.GetMenuOptions(accountId);
        var exitIndex = menuOptions.Length + 1;
        var runMenu = true;
        while (runMenu)
        {
            DisplayOptions(exitIndex, menuOptions);
            int selection = Convert.ToInt16(this.inputGetter.GetInput((input) => IsValidSelection(input, exitIndex, this.inputGetter), "Enter selection: "), new CultureInfo("en-US"));
            if (selection == exitIndex)
            {
                Console.Clear();
                runMenu = false;
            }
            else
            {
                menuOptions[selection - 1].Run(accountId, this.inputGetter, this.accountDAL);
            }
        }
    }

    private IMenuOption[] GetMenuOptions(int accountId)
    {
        var role = this.accountDAL.GetRole(accountId);
        if (role == "admin")
        {
            return new IMenuOption[] {
                new CreateNewAccountMenuOption(),
                new DeleteExistingAccountMenuOption(),
                new UpdateAccountInformationMenuOption(),
                new SearchForAccountMenuOption()
            };
        }
        else
        {
            return new IMenuOption[] {
                new WithdrawCashMenuOption(),
                new DisplayBalanceMenuOption(),
                new DepositCashMenuOption()
            };
        }
    }

    private static bool IsValidSelection(string input, int exitIndex, IInputGetter inputGetter)
    {
        if (!new Regex(inputGetter.RegexConstants.MenuOptionSelection).Match(input).Success)
        {
            return false;
        }

        if (Convert.ToInt16(input, new CultureInfo("en-US")) > exitIndex)
        {
            return false;
        }

        if (Convert.ToInt16(input, new CultureInfo("en-US")) <= 0)
        {
            return false;
        }

        return true;
    }

    private static void DisplayOptions(int exitIndex, IMenuOption[] menuOptions)
    {
        var optionIndex = 1;
        foreach (var menuOption in menuOptions)
        {
            Console.WriteLine($"{optionIndex}....{menuOption.Name}");
            optionIndex++;
        }
        Console.WriteLine($"{exitIndex}....Exit");
    }
}
