internal class DepositCashMenuOption : IDepositCashMenuOption
{
    public string Name { get; } = "Deposit Cash";

    public DepositCashMenuOption() { }

    public void Run(int user_id)
    {
        Console.WriteLine("Depositing Cash");
    }
}
