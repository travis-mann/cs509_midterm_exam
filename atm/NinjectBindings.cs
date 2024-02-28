using Ninject.Modules;

public class NinjectBindings : NinjectModule
{
    public override void Load()
    {
        Bind<IATMSystem>().To<ATMSystem>();
        Bind<ILoginMenu>().To<LoginMenu>();
        Bind<IMenuCreator>().To<MenuCreator>();
        Bind<IMenu>().To<Menu>();
        Bind<IInputGetter>().To<InputGetter>();
    }
}
