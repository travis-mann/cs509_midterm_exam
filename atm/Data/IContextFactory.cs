namespace Atm.Data;

/// <summary>
/// Public interface for ContextFactory class
/// </summary>
public interface IContextFactory
{
    /// <summary>
    /// Returns a new context for interacting with the persistant data storage
    /// </summary>
    /// <returns></returns>
    public Context CreateContext();
}
