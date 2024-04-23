public class UserRepository
{
    
    public UserRepository(string login, int pin, string name, int statusId, int roleId)
    {
        this.login = login;
        this.pin = pin;
        this.name = name;
        this.statusId = statusId;
        this.roleId = roleId;
    }

    public int id { get; set; }
    public string login { get; set;  }
    public int pin { get; set; }
    public string name { get; set; }
    public int statusId { get; set; }
    public int roleId { get; set; }
}
