public interface IInputGetter
{
    public string GetInput(Func<string, bool> isValid, string fieldName, string? errorMessage = null);
}