namespace Atm.UserMenu;
using Atm.Common;
using Atm.Dal;

/// <summary>
/// Public interface for generic menu option
/// </summary>
public interface IMenuOption
{
    /// <summary>
    /// Menu option name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Method to run the core functionality of the menu option
    /// </summary>
    /// <param name="accountId">user accountId</param>
    /// <param name="inputGetter">class to manage gathering and validating user input</param>
    /// <param name="accountDAL">manages interactions with persistant data storage</param>
    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL);
}

