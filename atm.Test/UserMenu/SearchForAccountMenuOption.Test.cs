namespace Atm.Test.UserMenu;

using System.Globalization;
using Atm.Common;
using Atm.Dal;
using Atm.UserMenu;

public class SearchForAccountMenuOptionTest
{
    private readonly int accountId;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public SearchForAccountMenuOptionTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void RunShouldGetUserNameOnValidAccountId()
    {
        this.mockAccountDAL.Setup(a => a.IsValidAccount(It.IsAny<int>())).Returns(true);
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { this.accountId.ToString(new CultureInfo("en-US")) });

        new SearchForAccountMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.IsValidAccount(this.accountId), Times.Once);
        this.mockAccountDAL.Verify(a => a.GetUserName(this.accountId), Times.Once);
    }

    [Fact]
    public void RunShouldNotGetUserNameOnInvalidAccountId()
    {
        this.mockAccountDAL.Setup(a => a.IsValidAccount(It.IsAny<int>())).Returns(false);
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { this.accountId.ToString(new CultureInfo("en-US")) });

        new SearchForAccountMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.IsValidAccount(this.accountId), Times.Once);
        this.mockAccountDAL.Verify(a => a.GetUserName(this.accountId), Times.Never);
    }
}
