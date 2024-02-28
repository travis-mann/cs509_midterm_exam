internal class DisplayBalanceMenuOption : IMenuOption
{
    public string Name { get; } = "Display Balance";

    public DisplayBalanceMenuOption() { }

    public void Run()
    {
        Console.WriteLine("Displaying Balance");
    }
}
