public interface IUserDALWithUpdate: IUserDAL
{
    public int UpdateUserName(int UserID, string name);
    public int UpdateUserStatus(int UserID, int statusID);
    public int UpdateUserLogin(int UserID, string login);
    public int UpdateUserPin(int UserID, int pin);
}

