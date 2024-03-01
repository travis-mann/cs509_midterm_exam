using Org.BouncyCastle.Asn1.X509;
using System.Linq;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {

    }

    public UserNotFoundException(string login)
        : base($"ERROR: User \"{login}\" not found")
    {

    }

    public UserNotFoundException(int id)
        : base($"ERROR: User id {id} not found")
    {

    }
}

public class DuplicateUserException : Exception
{
    public DuplicateUserException()
    {

    }

    public DuplicateUserException(string login)
        : base($"ERROR: User \"{login}\" already exists")
    {

    }
}


public class UserDAL: IUserDAL
{
    public string GetUserLogin(int UserID)
    {
        using (Context context = new())
        {
            return GetUser(UserID, context).login;
        }
    }

    public string GetUserName(int UserID)
    {
        using (Context context = new())
        {
            return GetUser(UserID, context).name;
        }
    }

    public int CreateUser(string login, int pin, string name, string status, string role)
    {
        using (Context context = new Context())
        {
            // check if user already exists
            try
            {
                int duplicateUserID = GetUserID(login);
                throw new DuplicateUserException();
            }
            catch (UserNotFoundException)
            {
                StatusRepository statusObj = context.Status.Single(s => s.name == status);
                RoleRepository roleObj = context.Role.Single(r => r.name == role);
                UserRepository newUser = new UserRepository(login, pin, name, statusObj.id, roleObj.id);
                context.User.Add(newUser);
                context.SaveChanges();
                return newUser.id;
            }
        }
    }

    public int DeleteUser(int UserID)
    {
        using (Context context = new())
        {
            UserRepository user = GetUser(UserID, context);
            context.User.Remove(user);
            context.SaveChanges();
            return user.id;
        }
    }

    public bool IsValidPin(string login, int pin)
    {
        using (Context context = new())
        {
            int? userID = context.User.Where(u => u.login == login && u.pin == pin).Select(u => u.id).SingleOrDefault();
            if (userID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public bool IsValidLogin(string login)
    {
        try
        {
            GetUserID(login);
            return true;
        }
        catch (UserNotFoundException)
        {
            return false;
        }
    }

    public int GetUserID(string login)
    {
        using (Context context = new())
        {
            int? userID = context.User.Where(u => u.login == login).Select(u => u.id).SingleOrDefault();
            if (userID == 0)
            {
                throw new UserNotFoundException(login);
            }
            else
            {
                return (int)userID;
            }
        }
    }

    private UserRepository GetUser(int userID, Context context)
    {
        UserRepository? user = context.User.SingleOrDefault(u => u.id == userID);
        if (user == null)
        {
            throw new UserNotFoundException(userID);
        }
        else
        {
            return user;
        }
    }
}

