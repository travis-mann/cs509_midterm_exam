namespace Atm.Common;
using Ninject.Modules;
using Atm.Dal;
using Atm.LoginMenu;
using Atm.UserMenu;

public class NinjectBindings : NinjectModule
{
    public override void Load()
    {
        this.Bind<ILoginMenu>().To<LoginMenu>();
        this.Bind<IMenu>().To<Menu>();
        this.Bind<IInputGetter>().To<InputGetter>();
        this.Bind<IAccountDAL>().To<AccountDAL>();
    }
}
