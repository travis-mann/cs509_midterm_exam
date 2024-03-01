public interface IUserDAL
{
    public string? GetUserLogin(int UserID);
    public string? GetUserName(int UserID);
    public int GetUserID(string login);
    public int CreateUser(string login, int pin, string name, string status, string role);
    public int DeleteUser(int UserID);
    public bool IsValidPin(string login, int pin);
    public bool IsValidLogin(string login);
}
