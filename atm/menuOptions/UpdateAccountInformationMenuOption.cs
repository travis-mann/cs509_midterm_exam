internal class UpdateAccountInformationMenuOption : IMenuOption
{
    public string Name { get; } = "Update Account Information";

    public UpdateAccountInformationMenuOption() { }

    public void Run()
    {
        Console.WriteLine("Updating Account Information");
    }
}
