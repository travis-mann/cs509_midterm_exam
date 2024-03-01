using System.Text.RegularExpressions;

public class LoginMenu: ILoginMenu
{
    private readonly static int InvalidUserID = -1;
    private IInputGetter _InputGetter;

    public LoginMenu(IInputGetter InputGetter)
    {
        _InputGetter = InputGetter;
    }

    public int Login()
    {

        int user_id = InvalidUserID;
        while (user_id == InvalidUserID)
        {
            string login = _InputGetter.GetInput(input => new Regex("([a-z]|[A-Z]|[0-9])+").Match(input).Success, "Enter login: ");
            int pin = Convert.ToInt16(_InputGetter.GetInput(input => new Regex("[0-9]{5}").Match(input).Success, "Enter pin: "));
            user_id = GetUserID(login, pin);
            if (user_id == InvalidUserID) {
                Console.WriteLine("ERROR: invalid user credentials, please try again");
            }
        }
        Console.WriteLine("login success!");
        return user_id;
    }

    private static int GetUserID(string username, int pin) 
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
                return InvalidUserID;
            }
        }
    }
}
