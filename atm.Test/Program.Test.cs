namespace Atm.Test;
using Atm;
using Atm.UserMenu;

public class ProgramTest
{
    [Fact]
    public void SingleATMLoopPassesAccountIdFromLoginToMenuForAdmin()
    {
        var loginMenu = new Mock<ILoginMenu>();
        var accountNum = new Fixture().Create<int>();
        loginMenu.Setup(x => x.Login()).Returns((accountNum, true));
        var menu = new Mock<IMenu>();
        ATM.SingleATMLoop(loginMenu.Object, menu.Object);
        menu.Verify(d => d.Run(accountNum, It.IsAny<IMenuOption[]>(), It.IsAny<bool>()), Times.Once);
    }

    [Fact]
    public void SingleATMLoopPassesAccountIdFromLoginToMenuForCustomer()
    {
        var loginMenu = new Mock<ILoginMenu>();
        var accountNum = new Fixture().Create<int>();
        loginMenu.Setup(x => x.Login()).Returns((accountNum, false));
        var menu = new Mock<IMenu>();
        ATM.SingleATMLoop(loginMenu.Object, menu.Object);
        menu.Verify(d => d.Run(accountNum, It.IsAny<IMenuOption[]>(), It.IsAny<bool>()), Times.Once);
    }
}
