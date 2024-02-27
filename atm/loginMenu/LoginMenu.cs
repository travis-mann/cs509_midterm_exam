using System.Text.RegularExpressions;

public class LoginMenu: ILoginMenu
{
    private static int invalidUserID = -1;

    public int Login()
    {

        int user_id = invalidUserID;
        while (user_id == invalidUserID)
        {
            string login = getInput("([a-z]|[A-Z]|[0-9])+", "login");
            int pin = Convert.ToInt16(getInput("[0-9]{5}", "pin"));
            user_id = getUserID(login, pin);
            if (user_id == invalidUserID) {
                Console.WriteLine("ERROR: invalid user credentials, please try again");
            }
        }
        Console.WriteLine("login success!");
        return user_id;
    }

    private static string getInput(string formatRegex, string fieldName)
    {
        // initial prompt
        Console.Write($"Enter {fieldName}:");
        string input = "";

        bool inputValid = false;
        while (!inputValid)
        {
            // get input
            input = Console.ReadLine();

            // validate login format
            Regex regex = new Regex(formatRegex, RegexOptions.ECMAScript);
            Match match = regex.Match(input);

            if (match.Success)  // valid login string
            {
                inputValid = true;
            }
            else  // Invalid login string
            {
                Console.Write($"ERROR: Invalid {fieldName}, Try again:");
            }
        }
        
        return input;
    }

    private static int getUserID(string username, int pin) 
    {
        // search database for user id associated with given credentials
        using (var context = new Context())
        {
            IQueryable<int> query = from table in context.User
                                    where table.login == username
                                    & table.pin == pin
                                    select table.id;

            int[] user_ids = query.ToArray();
            if (user_ids.Length > 0)
            {
                return user_ids[0];
            }
            else
            {
                return invalidUserID;
            }
        }
    }
}
