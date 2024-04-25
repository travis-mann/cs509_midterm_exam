namespace Atm.Test.UserMenu;
using Atm.Common;

using Atm.Dal;
using Atm.UserMenu;

public class MenuTest
{
    private readonly int accountId;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public MenuTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockInputGetter.Setup(i => i.RegexConstants.MenuOptionSelection).Returns("[0-9]+");
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void RunRunsMenuOptionWhenSelectedAndExits()
    {
        var mockMenuOption = new Mock<IMenuOption>();
        var mockMenuOptions = new[] { mockMenuOption.Object };
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { "1", "2" });
        new Menu(this.mockInputGetter.Object, this.mockAccountDAL.Object).Run(this.accountId, mockMenuOptions, false);
        mockMenuOption.Verify(m => m.Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object), Times.Once);
    }

    [Fact]
    public void IsValidSelectionReturnsFalseIfInputDoesntMatchRegex()
    {
        var exitIndex = new Fixture().Create<int>();
        var selection = "ABC";
        Menu.IsValidSelection(selection, exitIndex, this.mockInputGetter.Object).Should().BeFalse();
    }

    [Fact]
    public void IsValidSelectionReturnsFalseIfInputGreaterThanExitIndex()
    {
        var exitIndex = new Fixture().Create<int>();
        var selection = (exitIndex + new Fixture().Create<int>()).ToString();
        Menu.IsValidSelection(selection, exitIndex, this.mockInputGetter.Object).Should().BeFalse();
    }

    [Fact]
    public void IsValidSelectionReturnsFalseIfInputLessThan0()
    {
        var exitIndex = new Fixture().Create<int>();
        var selection = (new Fixture().Create<int>() * -1).ToString();
        Menu.IsValidSelection(selection, exitIndex, this.mockInputGetter.Object).Should().BeFalse();
    }

    [Fact]
    public void IsValidSelectionReturnsTrueIfInputGreaterThan0LessThanExit()
    {
        var exitIndex = FixtureHelper.CreateIntInRange(2, 100);
        var selection = FixtureHelper.CreateIntInRange(1, exitIndex).ToString();
        Menu.IsValidSelection(selection, exitIndex, this.mockInputGetter.Object).Should().BeTrue();
    }
}
