public interface IMenuOption
{
    public string Name { get; }

    public void Run(int userId);
}

