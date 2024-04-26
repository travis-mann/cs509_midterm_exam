namespace Atm.Common;

/// <summary>
/// Public interface for regex expressions to validate user input
/// </summary>
public interface IRegexConstants
{
    /// <summary>
    /// Required login format
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Required pin format
    /// </summary>
    public string Pin { get; }

    /// <summary>
    /// Required balance format
    /// </summary>
    public string Balance { get; }

    /// <summary>
    /// Required name format
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Required accountId format
    /// </summary>
    public string AccountID { get; }

    /// <summary>
    /// Required menu option selection format
    /// </summary>
    public string MenuOptionSelection { get; }
    /// <summary>
    /// Required status format
    /// </summary>
    public string Status { get; }
}
