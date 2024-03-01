using Org.BouncyCastle.Asn1.X509;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {

    }

    public UserNotFoundException(string login)
        : base($"ERROR: User \"{login}\" not found")
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
    public UserRepository GetUser(string login)
    {
        using (Context context = new())
        {
            UserRepository? user = context.User.SingleOrDefault(a => a.login == login);
            if (user == null)
            {
                throw new UserNotFoundException(login);
            }
            else
            {
                return (UserRepository)user;
            }
        }
    }

    public int CreateUser(string login, int pin, string name, string status, string role)
    {
        using (Context context = new Context())
        {
            // check if user already exists
            try
            {
                UserRepository duplicateUser = GetUser(login);
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

    public int DeleteUser(string login)
    {
        using (Context context = new Context())
        {
            UserRepository user;
            try
            {
                user = context.User.Single(u => u.login == login);
            }
            catch (InvalidOperationException)
            {
                throw new UserNotFoundException(login);
            }
            context.User.Remove(user);
            context.SaveChanges();
            return user.id;
        }
    }
}

