using System.Reflection;
using Ninject;  // dependancy injection

class Program
{
    private static void Main(string[] args)
    {
        var kernel = new StandardKernel();
        kernel.Load(Assembly.GetExecutingAssembly());
        var atm = kernel.Get<IATMSystem>();
        atm.Run();
    }
}