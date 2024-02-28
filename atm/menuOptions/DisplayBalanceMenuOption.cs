internal class DisplayBalanceMenuOption : IDisplayBalanceMenuOption
{
    public string Name { get; } = "Display Balance";

    public DisplayBalanceMenuOption() { }

    public void Run(int user_id)
    {
        Console.WriteLine("Displaying Balance");
    }
}
