internal class UpdateAccountInformationMenuOption : IUpdateAccountInformationMenuOption
{
    public string Name { get; } = "Update Account Information";

    public UpdateAccountInformationMenuOption() { }

    public void Run(int user_id)
    {
        Console.WriteLine("Updating Account Information");
    }
}
