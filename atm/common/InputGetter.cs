namespace Atm.Common;

public class InputGetter : IInputGetter
{
    public IRegexConstants RegexConstants { get; } = new RegexConstants();

    public string GetInput(Func<string, bool> isValid, string fieldName, string? errorMessage = null)
    {
        // initial prompt
        Console.Write(fieldName);
        var input = "";

        var inputValid = false;
        while (!inputValid)
        {
            // get input
            input = Console.ReadLine() ?? "";

            if (isValid(input))  // valid login string
            {
                inputValid = true;
            }
            // Invalid login string
            else if (errorMessage != null)
            {
                Console.Write(errorMessage);
            }
            else
            {
                Console.Write($"ERROR: Invalid value, Try again: ");
            }
        }

        return input;
    }
}
