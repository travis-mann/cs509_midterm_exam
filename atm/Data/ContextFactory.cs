namespace Atm.Data;

/// <summary>
/// Manages context creation for interaction with persistant data storage
/// </summary>
public class ContextFactory : IContextFactory
{
    /// <summary>
    /// Returns a new context for interacting with the persistant data storage
    /// </summary>
    /// <returns></returns>
    public Context CreateContext() => new();
}
