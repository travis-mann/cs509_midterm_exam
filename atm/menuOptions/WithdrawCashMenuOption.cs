internal class WithdrawCashMenuOption : IMenuOption
{
    public string Name { get; } = "Withdraw Cash";

    public WithdrawCashMenuOption() {}

    public void Run()
    {
        Console.WriteLine("withdrawing cash");
    }
}
