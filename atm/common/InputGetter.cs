namespace Atm.Common;

/// <summary>
/// Manages gathering and validating user input
/// </summary>
public class InputGetter : IInputGetter
{
    /// <summary>
    /// Member class which holds regex strings used to validate user input
    /// </summary>
    public IRegexConstants RegexConstants { get; } = new RegexConstants();

    /// <summary>
    /// Gather and validate user input
    /// </summary>
    /// <param name="isValid">function to determine if a given input is valid</param>
    /// <param name="fieldName">user prompt</param>
    /// <param name="errorMessage">message displayed for invalid input</param>
    /// <returns></returns>
    public string GetInput(Func<string, bool> isValid, string fieldName, string errorMessage)
    {
        Console.Write(fieldName);
        string? input = null;

        while (input is null)
        {
            input = GetInputSingleLoop(isValid, errorMessage);
        }

        return input;
    }

    internal static string? GetInputSingleLoop(Func<string, bool> isValid, string errorMessage)
    {
        var input = Console.ReadLine() ?? "";
        if (isValid(input))
        {
            return input;
        }
        else
        {
            Console.Write(errorMessage);
        }

        return null;
    }
}
