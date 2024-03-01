public interface IUserDAL
{
    public UserRepository GetUser(string login);
    public int CreateUser(string login, int pin, string name, string status, string role);
    public int DeleteUser(string login);
}
