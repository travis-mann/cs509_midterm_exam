using System.Text.RegularExpressions;

public class Menu: IMenu
{
    private IMenuOption[] _MenuOptions;
    private IInputGetter _InputGetter;
    private IRegexConstants _RegexConstants;

    private int ExitIndex;
    
    public Menu(IMenuOption[] menuOptions, IInputGetter InputGetter, IRegexConstants RegexConstants)
    {
        _MenuOptions = menuOptions;
        _InputGetter = InputGetter;
        _RegexConstants = RegexConstants;
        ExitIndex = menuOptions.Length + 1;
    }

    public void Run(int userId)
    {
        bool runMenu = true;
        while (runMenu)
        {
            DisplayOptions();
            int selection = Convert.ToInt16(_InputGetter.GetInput(isValidSelection, "Enter selection: "));
            if (selection == ExitIndex)
            {
                Console.Clear();
                runMenu = false;
            }
            else
            {
                _MenuOptions[selection - 1].Run(userId);
            }
        }
    }

    private bool isValidSelection(string input)
    {
        if (!new Regex(_RegexConstants.menuOptionSelection).Match(input).Success)
            return false;

        if (Convert.ToInt16(input) > ExitIndex)
            return false;

        if (Convert.ToInt16(input) <= 0)
            return false;

        return true;
    }

    private void DisplayOptions()
    {
        int optionIndex = 1;
        foreach (IMenuOption menuOption in _MenuOptions)
        {
            Console.WriteLine($"{optionIndex}....{menuOption.Name}");
            optionIndex++;
        }
        Console.WriteLine($"{ExitIndex}....Exit");
    }
}
