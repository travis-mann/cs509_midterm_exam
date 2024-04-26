namespace Atm.UserMenu;

/// <summary>
/// Public interface for menu class
/// </summary>
public interface IMenu
{
    /// <summary>
    /// Runs menu with given menu options
    /// </summary>
    /// <param name="accountId">user accountId</param>
    /// <param name="menuOptions">options used to populate the menu</param>
    /// <param name="clearConsole">optional flag to clear the console on exit selection</param>
    public void Run(int accountId, IMenuOption[] menuOptions, bool clearConsole = true);
}
