namespace Atm.MenuOptions;
using Atm.Dal;
using Atm.Common;

public interface IMenuOption
{
    public string Name { get; }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL);
}

