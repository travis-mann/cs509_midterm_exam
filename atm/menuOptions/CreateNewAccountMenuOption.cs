internal class CreateNewAccountMenuOption : IMenuOption
{
    public string Name { get; } = "Create New Account";

    public CreateNewAccountMenuOption() { }

    public void Run()
    {
        Console.WriteLine("creating a new account wooooooo");
    }
}
