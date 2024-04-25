namespace Atm.Test.UserMenu;

using Atm.UserMenu;
using Atm.Common;
using Atm.Dal;

public class DisplayBalanceMenuOptionTest
{
    private readonly int accountId;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public DisplayBalanceMenuOptionTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void RunShouldGetBalance()
    {
        new DisplayBalanceMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.GetBalance(this.accountId), Times.Once());
    }
}
