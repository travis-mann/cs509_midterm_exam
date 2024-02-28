internal class CreateNewAccountMenuOption : ICreateNewAccountMenuOption
{
    public string Name { get; } = "Create New Account";

    public CreateNewAccountMenuOption() { }

    public void Run(int user_id)
    {
        Console.WriteLine("creating a new account wooooooo");
    }
}
