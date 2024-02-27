public class ATMSystem: IATMSystem
{
    private ILoginMenu _LoginMenu;

    public ATMSystem (ILoginMenu LoginMenu) 
    {
        _LoginMenu = LoginMenu;
    }
    
    public void Run()
    {
        Console.WriteLine("starting atm...");

        while (true)
        {
            int user_id = _LoginMenu.Login();
        }
    }
}
