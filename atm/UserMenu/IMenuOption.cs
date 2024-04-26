namespace Atm.UserMenu;
using Atm.Common;
using Atm.Dal;

public interface IMenuOption
{
    public string Name { get; }

    public void Run(int accountId, IInputGetter inputGetter, IAccountDAL accountDAL);
}

