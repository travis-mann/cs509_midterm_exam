public class UserRepository
{
    
    public UserRepository(string login, int pin, string name, int status_id, int role_id)
    {
        this.login = login;
        this.pin = pin;
        this.name = name;
        this.status_id = status_id;
        this.role_id = role_id;
    }

    public int id { get; set; }
    public string login { get; set;  }
    public int pin { get; set; }
    public string name { get; set; }
    public int status_id { get; set; }
    public int role_id { get; set; }
}
