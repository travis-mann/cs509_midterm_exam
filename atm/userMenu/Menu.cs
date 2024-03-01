using System.Text.RegularExpressions;

public class Menu: IMenu
{
    private IMenuOption[] _MenuOptions;
    private IInputGetter _InputGetter;

    private int ExitIndex;
    
    public Menu(IMenuOption[] menuOptions, IInputGetter InputGetter)
    {
        _MenuOptions = menuOptions;
        _InputGetter = InputGetter;
        ExitIndex = menuOptions.Length + 1;
    }

    public void Run(int user_id)
    {
        bool runMenu = true;
        while (runMenu) 
        {
            DisplayOptions();
            int selection = Convert.ToInt16(_InputGetter.GetInput(isValidSelection, "Enter selection: "));
            if (selection == ExitIndex)
            {
                runMenu = false;
            }
            else
            {
                _MenuOptions[selection - 1].Run(user_id);
            }
        }
    }

    private bool isValidSelection(string input)
    {
        if (!new Regex("[0-9]+").Match(input).Success)
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
