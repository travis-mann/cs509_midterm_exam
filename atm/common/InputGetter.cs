namespace Atm.Common;

public class InputGetter : IInputGetter
{
    public IRegexConstants RegexConstants { get; } = new RegexConstants();


    public string GetInput(Func<string, bool> isValid, string fieldName, string? errorMessage = null)
    {
        Console.Write(fieldName);
        string? input = null;

        while (input is null)
        {
            input = GetInputSingleLoop(isValid, errorMessage);
        }

        return input;
    }

    internal static string? GetInputSingleLoop(Func<string, bool> isValid, string? errorMessage = "ERROR: Invalid value, Try again: ")
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
