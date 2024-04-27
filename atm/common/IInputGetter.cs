namespace Atm.Common;

/// <summary>
/// Public interface for InputGetter class
/// </summary>
public interface IInputGetter
{
    /// <summary>
    /// Member class to contain regex strings for validating different types of input
    /// </summary>
    IRegexConstants RegexConstants { get; }

    /// <summary>
    /// Gather and validate user input
    /// </summary>
    /// <param name="isValid">function to determine if a given input is valid</param>
    /// <param name="fieldName">user prompt</param>
    /// <param name="errorMessage">message displayed for invalid input</param>
    /// <returns></returns>
    public string GetInput(Func<string, bool> isValid, string fieldName, string errorMessage = "ERROR: Invalid value, Try again: ");
}
