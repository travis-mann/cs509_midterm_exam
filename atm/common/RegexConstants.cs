public class RegexConstants: IRegexConstants
{
    public string login { get; } = "([a-z]|[A-Z]|[0-9])+";
    public string pin { get; } = "[0-9]{5}";
    public string balance { get; } = "[0-9]+";
    public string name { get; } = "([a-z]|[A-Z]| )+";

}
