namespace Atm.Data;

public class ContextFactory : IContextFactory
{
    public Context CreateContext() => new Context();
}
