using Ninject.Modules;

public class NinjectBindings : NinjectModule
{
    public override void Load()
    {
        Bind<IATMSystem>().To<ATMSystem>();
        Bind<ILoginMenu>().To<LoginMenu>();
    }
}

