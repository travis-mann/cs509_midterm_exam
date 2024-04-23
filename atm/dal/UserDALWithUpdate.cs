public class UserDALWithUpdate: UserDAL, IUserDALWithUpdate
{
    public int UpdateUserName(int UserID, string name)
    {
        using (Context context = new())
        {
            UserRepository user = GetUser(UserID, context);
            user.name = name;
            context.SaveChanges();
            return user.id;
        }
    }

    public int UpdateUserStatus(int UserID, int statusID)
    {
        using (Context context = new())
        {
            UserRepository user = GetUser(UserID, context);
            user.statusId = statusID;
            context.SaveChanges();
            return user.id;
        }
    }

    public int UpdateUserLogin(int UserID, string login)
    {
        using (Context context = new())
        {
            UserRepository user = GetUser(UserID, context);
            user.login = login;
            context.SaveChanges();
            return user.id;
        }
    }

    public int UpdateUserPin(int UserID, int pin)
    {
        using (Context context = new())
        {
            UserRepository user = GetUser(UserID, context);
            user.pin = pin;
            context.SaveChanges();
            return user.id;
        }
    }
}
