namespace Atm;

using System.Reflection;
using Atm.UserMenu;
using Ninject;  // dependancy injection

internal class ATM
{
    /// <summary>
    /// Entry point to load menus and start ATM Loop
    /// </summary>
    internal static void Main()
    {
        var kernel = new StandardKernel();
        kernel.Load(Assembly.GetExecutingAssembly());

        var loginMenu = kernel.Get<ILoginMenu>();
        var menu = kernel.Get<IMenu>();
        StartATMLoop(loginMenu, menu);

    }

    internal static void StartATMLoop(ILoginMenu loginMenu, IMenu menu)
    {
        Console.WriteLine("starting atm...");
        while (true)
        {
            SingleATMLoop(loginMenu, menu);
        }
    }

    internal static void SingleATMLoop(ILoginMenu loginMenu, IMenu menu)
    {
        var (accountId, isAdmin) = loginMenu.Login();
        menu.Run(accountId, isAdmin ? GetAdminMenu() : GetCustomerMenu());
    }

    private static IMenuOption[] GetAdminMenu() => new IMenuOption[] {
                new CreateNewAccountMenuOption(),
                new DeleteExistingAccountMenuOption(),
                new UpdateAccountInformationMenuOption(),
                new SearchForAccountMenuOption()
            };

    private static IMenuOption[] GetCustomerMenu() => new IMenuOption[] {
                new CreateNewAccountMenuOption(),
                new DeleteExistingAccountMenuOption(),
                new UpdateAccountInformationMenuOption(),
                new SearchForAccountMenuOption()
            };
}
