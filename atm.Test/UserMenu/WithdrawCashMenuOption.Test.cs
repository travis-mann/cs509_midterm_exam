namespace Atm.Test.UserMenu;

using System.Globalization;
using System.Xml.Linq;
using Atm.Common;
using Atm.Dal;
using Atm.UserMenu;

public class WithdrawCashMenuOptionTest
{
    private readonly int accountId;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public WithdrawCashMenuOptionTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void RunShouldUpdateBalanceWhenAmountIsLessThanBalance()
    {
        var balance = new Fixture().Create<int>();
        var amountToWithdraw = FixtureHelper.CreateIntInRange(0, balance);

        this.mockAccountDAL.Setup(a => a.GetBalance(this.accountId)).Returns(balance);
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { amountToWithdraw.ToString() });

        new WithdrawCashMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.GetBalance(this.accountId), Times.Exactly(2));
        this.mockAccountDAL.Verify(a => a.UpdateBalance(-1 * amountToWithdraw, this.accountId), Times.Once);
    }

    [Fact]
    public void RunShouldNotUpdateBalanceWhenAmountIsMoreThanBalance()
    {
        var balance = new Fixture().Create<int>();
        var amountToWithdraw = balance + new Fixture().Create<int>();

        this.mockAccountDAL.Setup(a => a.GetBalance(this.accountId)).Returns(balance);
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { amountToWithdraw.ToString() });

        new WithdrawCashMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.GetBalance(this.accountId), Times.Exactly(2));
        this.mockAccountDAL.Verify(a => a.UpdateBalance(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }
}
