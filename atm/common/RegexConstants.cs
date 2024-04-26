namespace Atm.Common;

/// <summary>
/// Regex Constants used to validate user input
/// </summary>
public class RegexConstants : IRegexConstants
{
    /// <summary>
    /// Expected login format
    /// </summary>
    public string Login { get; } = "^([a-z]|[A-Z]|[0-9])+$";

    /// <summary>
    /// Expected pin format
    /// </summary>
    public string Pin { get; } = "^[0-9]{5}$";

    /// <summary>
    /// Expected balance format
    /// </summary>
    public string Balance { get; } = "^[0-9]+$";

    /// <summary>
    /// Expected name format
    /// </summary>
    public string Name { get; } = "^([a-z]|[A-Z]| )+$";

    /// <summary>
    /// Expected accountId format
    /// </summary>
    public string AccountID { get; } = "^[0-9]+$";

    /// <summary>
    /// Expected menu option selection format
    /// </summary>
    public string MenuOptionSelection { get; } = "^[0-9]+$";

    /// <summary>
    /// Expected status format
    /// </summary>
    public string Status { get; } = "(active|disabled)";
}
