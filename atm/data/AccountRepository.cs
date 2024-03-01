public class AccountRepository
{
    public AccountRepository(int user_id, int status_id, int balance)
    {
        this.user_id = user_id;
        this.status_id = status_id;
        this.balance = balance;
    }

    public int id { get; set; }
    public int user_id { get; set; }
    public int status_id { get; set; }
    public int balance { get; set; }
}

