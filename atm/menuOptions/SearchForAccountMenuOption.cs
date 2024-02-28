internal class SearchForAccountMenuOption : ISearchForAccountMenuOption
{
    public string Name { get; } = "Search for Account";

    public SearchForAccountMenuOption() { }

    public void Run(int user_id)
    {
        Console.WriteLine("Searching for Account");
    }
}
