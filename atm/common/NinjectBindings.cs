namespace Atm.Common;
using Ninject.Modules;
using Atm.Dal;
using Atm.Data;
using Atm.LoginMenu;
using Atm.UserMenu;

public class NinjectBindings : NinjectModule
{
    public override void Load()
    {
        _ = this.Bind<ILoginMenu>().To<LoginMenu>();
        _ = this.Bind<IMenu>().To<Menu>();
        _ = this.Bind<IInputGetter>().To<InputGetter>();
        _ = this.Bind<IAccountDAL>().To<AccountDAL>();
        _ = this.Bind<IContextFactory>().To<ContextFactory>();
    }
}
