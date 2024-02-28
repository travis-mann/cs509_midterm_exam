internal class DeleteExistingAccountMenuOption : IMenuOption
{
    public string Name { get; } = "Delete Existing Account";

    public DeleteExistingAccountMenuOption() { }

    public void Run()
    {
        Console.WriteLine("deleting existing account");
    }
}
