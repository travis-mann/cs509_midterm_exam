using System.Reflection;
using Atm.UserMenu;
using Ninject;  // dependancy injection

static void Run(ILoginMenu loginMenu, IMenu menu)
{
    Console.WriteLine("starting atm...");

    while (true)
    {
        var accountId = loginMenu.Login();
        menu.Run(accountId);
    }
}

var kernel = new StandardKernel();
kernel.Load(Assembly.GetExecutingAssembly());

var loginMenu = kernel.Get<ILoginMenu>();
var menu = kernel.Get<IMenu>();
Run(loginMenu, menu);
