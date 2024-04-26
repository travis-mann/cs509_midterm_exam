namespace Atm.Dal;

/// <summary>
/// Data Access Layer control for interacting with persistant data storage
/// </summary>
public interface IAccountDAL
{
    /// <summary>
    /// Creates a new account
    /// </summary>
    /// <param name="login">login for new account</param>
    /// <param name="pin">pin for new account</param>
    /// <param name="name">name for new account</param>
    /// <param name="role">role for new account</param>
    /// <param name="status">status for new account</param>
    /// <param name="balance">starting balance for new account</param>
    /// <returns></returns>
    public int CreateAccount(string login, int pin, string name, string role, string status, int balance);

    /// <summary>
    /// Deletes an existing account
    /// </summary>
    /// <param name="accountId">Id associated with account to delete</param>
    /// <returns></returns>
    public int DeleteAccount(int accountId);

    /// <summary>
    /// Checks if a given login exists
    /// </summary>
    /// <param name="login">login to check</param>
    /// <returns></returns>
    public bool IsValidLogin(string login);

    /// <summary>
    /// Checks if a given pin is correct for a given login
    /// </summary>
    /// <param name="login">login to check against</param>
    /// <param name="pin">pin to check</param>
    /// <returns></returns>
    public bool IsValidPin(string login, int pin);

    /// <summary>
    /// Checks if the given accountId exists
    /// </summary>
    /// <param name="accountId">account id to check</param>
    /// <returns></returns>
    public bool IsValidAccount(int accountId);

    /// <summary>
    /// Checks if the account associated with the given account id is an admin account
    /// </summary>
    /// <param name="accountId">account id to check</param>
    /// <returns></returns>
    public bool IsAdmin(int accountId);

    /// <summary>
    /// Returns the current balance
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve balance from</param>
    /// <returns></returns>
    public int GetBalance(int accountId);

    /// <summary>
    /// Get name associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve name from</param>
    /// <returns></returns>
    public string GetUserName(int accountId);

    /// <summary>
    /// Get login associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve login from</param>
    /// <returns></returns>
    public string GetUserLogin(int accountId);

    /// <summary>
    /// Get status associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve status from</param>
    /// <returns></returns>
    public string GetStatus(int accountId);

    /// <summary>
    /// Get role associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve role from</param>
    /// <returns></returns>
    public string GetRole(int accountId);

    /// <summary>
    /// Get pin associated with an account
    /// </summary>
    /// <param name="accountId">account id associated with account to retrieve pin from</param>
    /// <returns></returns>
    public int GetPin(int accountId);

    /// <summary>
    /// Get account id associated with given login
    /// </summary>
    /// <param name="login">login for account to get id from</param>
    /// <returns></returns>
    public int GetAccountIdFromLogin(string login);

    /// <summary>
    /// Update balance for a given account
    /// </summary>
    /// <param name="amountToAdd">amount to add to account balance</param>
    /// <param name="accountId">id associated with account to update</param>
    /// <returns></returns>
    public int UpdateBalance(int amountToAdd, int accountId);

    /// <summary>
    /// Update name for a given account
    /// </summary>
    /// <param name="accountId">id associated with account to update</param>
    /// <param name="name">new name</param>
    /// <returns></returns>
    public int UpdateUserName(int accountId, string name);

    /// <summary>
    /// Update status for a given account
    /// </summary>
    /// <param name="accountId">id associated with account to update</param>
    /// <param name="status">new status</param>
    /// <returns></returns>
    public int UpdateUserStatus(int accountId, string status);

    /// <summary>
    /// Update login for a given account
    /// </summary>
    /// <param name="accountId">id associated with account to update</param>
    /// <param name="login">new login</param>
    /// <returns></returns>
    public int UpdateUserLogin(int accountId, string login);

    /// <summary>
    /// Update pin for a given account
    /// </summary>
    /// <param name="accountId">id associated with account to update</param>
    /// <param name="pin">new pin</param>
    /// <returns></returns>
    public int UpdateUserPin(int accountId, int pin);
}
