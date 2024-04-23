namespace Atm.Common;

public class RegexConstants : IRegexConstants
{
    public string Login { get; } = "^([a-z]|[A-Z]|[0-9])+$";
    public string Pin { get; } = "^[0-9]{5}$";
    public string Balance { get; } = "^[0-9]+$";
    public string Name { get; } = "^([a-z]|[A-Z]| )+$";
    public string AccountID { get; } = "^[0-9]+$";
    public string MenuOptionSelection { get; } = "^[0-9]+$";
    public string Status { get; } = "(active|disabled)";
}
