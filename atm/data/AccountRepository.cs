public class AccountRepository
{
    public AccountRepository(int userId, int statusId, int balance)
    {
        this.userId = userId;
        this.statusId = statusId;
        this.balance = balance;
    }

    public int id { get; set; }
    public int userId { get; set; }
    public int statusId { get; set; }
    public int balance { get; set; }
}

