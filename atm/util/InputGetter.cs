using System.Text.RegularExpressions;

public class InputGetter: IInputGetter
{
    
    public InputGetter() { }
    
    public string GetInput(Func<string, bool> isValid, string prompt, string? errorMessage = null)
    {
        // initial prompt
        Console.Write(prompt);
        string input = "";

        bool inputValid = false;
        while (!inputValid)
        {
            // get input
            input = Console.ReadLine();

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
