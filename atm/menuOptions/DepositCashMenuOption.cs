internal class DepositCashMenuOption : IMenuOption
{
    public string Name { get; } = "Deposit Cash";

    public DepositCashMenuOption() { }

    public void Run()
    {
        Console.WriteLine("Depositing Cash");
    }
}
