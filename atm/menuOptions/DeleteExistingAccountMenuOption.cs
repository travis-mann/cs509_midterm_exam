internal class DeleteExistingAccountMenuOption : IDeleteExistingAccountMenuOption
{
    public string Name { get; } = "Delete Existing Account";

    public DeleteExistingAccountMenuOption() { }

    public void Run(int user_id)
    {
        Console.WriteLine("deleting existing account");
    }
}
