public class ATMSystem: IATMSystem
{
    private ILoginMenu _LoginMenu;
    private IMenuCreator _MenuCreator;

    public ATMSystem (ILoginMenu LoginMenu, IMenuCreator MenuCreator) 
    {
        _LoginMenu = LoginMenu;
        _MenuCreator = MenuCreator;
    }
    
    public void Run()
    {
        Console.WriteLine("starting atm...");

        while (true)
        {
            int user_id = _LoginMenu.Login();
            IMenu menu = _MenuCreator.GetMenu(user_id);
            menu.Run();
        }
    }
}
