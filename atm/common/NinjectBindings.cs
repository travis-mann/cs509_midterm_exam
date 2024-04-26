namespace Atm.Common;
using Atm.Dal;
using Atm.Data;
using Atm.LoginMenu;
using Atm.UserMenu;
using Ninject.Modules;

/// <summary>
/// Stores bindings between interfaces and associated implementations for DI
/// </summary>
public class NinjectBindings : NinjectModule
{
    /// <summary>
    /// Initilizes and definites DI relationships
    /// </summary>
    public override void Load()
    {
        _ = this.Bind<ILoginMenu>().To<LoginMenu>();
        _ = this.Bind<IMenu>().To<Menu>();
        _ = this.Bind<IInputGetter>().To<InputGetter>();
        _ = this.Bind<IAccountDAL>().To<AccountDAL>();
        _ = this.Bind<IContextFactory>().To<ContextFactory>();
    }
}
