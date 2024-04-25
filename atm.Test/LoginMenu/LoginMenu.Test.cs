namespace Atm.Test.LoginMenu;
using Atm.LoginMenu;
using Atm.Common;
using Atm.Dal;
using System.Globalization;

public class LoginMenuTest
{
    [Fact]
    public void LoginAttemptHandlerReturnsNullOnInvalidLogin()
    {
        var login = new Fixture().Create<string>();
        var mockInputGetter = new Mock<IInputGetter>();
        mockInputGetter.Setup(i => i.GetInput(It.IsAny<Func<string, bool>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(login);

        var mockAccountDAL = new Mock<IAccountDAL>();
        mockAccountDAL.Setup(a => a.IsValidLogin(It.IsAny<string>())).Returns(false);
        LoginMenu.LoginAttemptHandler(mockInputGetter.Object, mockAccountDAL.Object).Should().BeNull();
    }

    [Fact]
    public void LoginAttemptHandlerReturnsNullOnInvalidPin()
    {
        var login = new Fixture().Create<string>();
        var pin = (new Fixture().Create<int>() % (99999 - 10000 + 1) + 10000).ToString(new CultureInfo("en-US"));
        var mockInputGetter = new Mock<IInputGetter>();
        mockInputGetter.SetupSequence(i => i.GetInput(It.IsAny<Func<string, bool>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(login).Returns(pin);

        var mockAccountDAL = new Mock<IAccountDAL>();
        mockAccountDAL.Setup(a => a.IsValidLogin(It.IsAny<string>())).Returns(true);
        mockAccountDAL.Setup(a => a.IsValidPin(It.IsAny<string>(), It.IsAny<int>())).Returns(false);
        LoginMenu.LoginAttemptHandler(mockInputGetter.Object, mockAccountDAL.Object).Should().BeNull();
    }

    [Fact]
    public void LoginAttemptHandlerReturnsGetAccountIdFromLoginResult()
    {
        var login = new Fixture().Create<string>();
        var pin = FixtureHelper.CreatePinInput();
        var accountId = new Fixture().Create<int>();
        var mockInputGetter = new Mock<IInputGetter>();
        mockInputGetter.SetupSequence(i => i.GetInput(It.IsAny<Func<string, bool>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(login).Returns(pin);

        var mockAccountDAL = new Mock<IAccountDAL>();
        mockAccountDAL.Setup(a => a.IsValidLogin(It.IsAny<string>())).Returns(true);
        mockAccountDAL.Setup(a => a.IsValidPin(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
        mockAccountDAL.Setup(a => a.GetAccountIdFromLogin(login)).Returns(accountId);
        LoginMenu.LoginAttemptHandler(mockInputGetter.Object, mockAccountDAL.Object).Should().Be(accountId);
    }
}
