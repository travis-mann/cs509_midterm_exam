namespace Atm.UserMenu;

public interface IMenu
{
    public void Run(int accountId, IMenuOption[] menuOptions, bool clearConsole = true);
}
