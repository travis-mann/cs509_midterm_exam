namespace Atm.Common;

public interface IInputGetter
{
    IRegexConstants RegexConstants { get; }

    public string GetInput(Func<string, bool> isValid, string fieldName, string? errorMessage = null);
}
