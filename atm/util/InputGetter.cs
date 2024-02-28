using System.Text.RegularExpressions;

public class InputGetter: IInputGetter
{
    
    public InputGetter() { }
    
    public string GetInput(Func<string, bool> isValid, string fieldName)
    {
        // initial prompt
        Console.Write($"Enter {fieldName}: ");
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
            else  // Invalid login string
            {
                Console.Write($"ERROR: Invalid {fieldName}, Try again: ");
            }
        }

        return input;
    }
}
