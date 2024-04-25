namespace Atm.Test.UserMenu;

using System.Globalization;
using Atm.UserMenu;
using Atm.Common;
using Atm.Dal;

public class DepositCashMenuOptionTest
{

    private readonly int accountId;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public DepositCashMenuOptionTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void RunShouldCallUpdateBalanceWithValidInput()
    {
        var amountToAdd = new Fixture().Create<int>();
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { amountToAdd.ToString(new CultureInfo("en-US")) });
        new DepositCashMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.UpdateBalance(amountToAdd, this.accountId), Times.Once);
    }

    [Fact]
    public void IsValidInputShouldReturnTrueWhenInputMatchesBalanceRegex()
    {
        var input = new Fixture().Create<string>();
        this.mockInputGetter.Setup(i => i.RegexConstants.Balance).Returns(input);
        DepositCashMenuOption.IsValidInput(input, this.mockInputGetter.Object).Should().BeTrue();
    }

    [Fact]
    public void IsValidInputShouldReturnFalseWhenInputDoesNotMatchBalanceRegex()
    {
        var input = new Fixture().Create<string>();
        this.mockInputGetter.Setup(i => i.RegexConstants.Balance).Returns("a^");
        DepositCashMenuOption.IsValidInput(input, this.mockInputGetter.Object).Should().BeFalse();
    }
}
