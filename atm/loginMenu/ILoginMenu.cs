namespace Atm.LoginMenu;

/// <summary>
/// Interface object for managing ATM login process
/// </summary>
public interface ILoginMenu
{
    /// <summary>
    /// Runs the login process for a user
    /// </summary>
    /// <returns>Account id and isAdmin flag</returns>
    public (int, bool) Login();
}
