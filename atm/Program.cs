using System.Reflection;
using Ninject;  // dependancy injection

static void Run(ILoginMenu loginMenu, IMenuCreator menuCreator)
{
    Console.WriteLine("starting atm...");

    while (true)
    {
        var user_id = loginMenu.Login();
        var menu = menuCreator.GetMenu(user_id);
        menu.Run(user_id);
    }
}

var kernel = new StandardKernel();
kernel.Load(Assembly.GetExecutingAssembly());

var loginMenu = kernel.Get<ILoginMenu>();
var menuCreator = kernel.Get<IMenuCreator>();
Run(loginMenu, menuCreator);
