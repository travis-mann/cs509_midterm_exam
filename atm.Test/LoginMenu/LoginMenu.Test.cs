namespace Atm.Test.LoginMenu;
using Atm.LoginMenu;
using Atm.Common;
using Atm.Dal;
using System.Globalization;

public class LoginMenuTest
{
    private readonly int accountId;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public LoginMenuTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void LoginReturnsAccountIdAndIsAdminOnValidLogin()
    {
        var login = FixtureHelper.CreateLoginInput();
        var pin = FixtureHelper.CreatePinInput();
        var isAdmin = new Fixture().Create<bool>();
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { login, pin });
        this.mockAccountDAL.Setup(a => a.IsValidLogin(login)).Returns(true);
        this.mockAccountDAL.Setup(a => a.IsValidPin(login, Convert.ToInt32(pin))).Returns(true);
        this.mockAccountDAL.Setup(a => a.GetAccountIdFromLogin(login)).Returns(this.accountId);
        this.mockAccountDAL.Setup(a => a.IsAdmin(this.accountId)).Returns(isAdmin);

        new LoginMenu(this.mockInputGetter.Object, this.mockAccountDAL.Object).Login().Should().Be((this.accountId, isAdmin));
    }

    [Fact]
    public void LoginAttemptHandlerReturnsNullOnInvalidLogin()
    {
        var login = new Fixture().Create<string>();
        this.mockInputGetter.Setup(i => i.GetInput(It.IsAny<Func<string, bool>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(login);
        this.mockAccountDAL.Setup(a => a.IsValidLogin(It.IsAny<string>())).Returns(false);
        LoginMenu.LoginAttemptHandler(this.mockInputGetter.Object, this.mockAccountDAL.Object).Should().BeNull();
    }

    [Fact]
    public void LoginAttemptHandlerReturnsNullOnInvalidPin()
    {
        var login = new Fixture().Create<string>();
        var pin = (new Fixture().Create<int>() % (99999 - 10000 + 1) + 10000).ToString(new CultureInfo("en-US"));

        this.mockInputGetter.SetupSequence(i => i.GetInput(It.IsAny<Func<string, bool>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(login).Returns(pin);
        this.mockAccountDAL.Setup(a => a.IsValidLogin(It.IsAny<string>())).Returns(true);
        this.mockAccountDAL.Setup(a => a.IsValidPin(It.IsAny<string>(), It.IsAny<int>())).Returns(false);
        LoginMenu.LoginAttemptHandler(this.mockInputGetter.Object, this.mockAccountDAL.Object).Should().BeNull();
    }

    [Fact]
    public void LoginAttemptHandlerReturnsGetAccountIdFromLoginResult()
    {
        var login = new Fixture().Create<string>();
        var pin = FixtureHelper.CreatePinInput();
        var accountId = new Fixture().Create<int>();

        this.mockInputGetter.SetupSequence(i => i.GetInput(It.IsAny<Func<string, bool>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(login).Returns(pin);
        this.mockAccountDAL.Setup(a => a.IsValidLogin(It.IsAny<string>())).Returns(true);
        this.mockAccountDAL.Setup(a => a.IsValidPin(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
        this.mockAccountDAL.Setup(a => a.GetAccountIdFromLogin(login)).Returns(accountId);

        LoginMenu.LoginAttemptHandler(mockInputGetter.Object, mockAccountDAL.Object).Should().Be(accountId);
    }
}
