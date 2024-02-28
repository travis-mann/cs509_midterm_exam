internal class SearchForAccountMenuOption : IMenuOption
{
    public string Name { get; } = "Search for Account";

    public SearchForAccountMenuOption() { }

    public void Run()
    {
        Console.WriteLine("Searching for Account");
    }
}
