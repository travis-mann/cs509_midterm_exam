namespace Atm.Test.UserMenu;

using System.Globalization;
using Atm.Common;
using Atm.Dal;
using Atm.UserMenu;

public class CreateNewAccountMenuOptionTest
{
    private readonly int accountId;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public CreateNewAccountMenuOptionTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void RunShouldCreateAccountOnNewLogin()
    {
        var login = new Fixture().Create<string>();
        var pin = FixtureHelper.CreatePinInput();
        var name = new Fixture().Create<string>();
        var balance = new Fixture().Create<int>().ToString(new CultureInfo("en-US"));
        var status = new Fixture().Create<string>();

        this.mockAccountDAL.Setup(a => a.IsValidLogin(It.IsAny<string>())).Returns(false);
        this.mockAccountDAL.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(this.accountId);
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { login, pin, name, balance, status });

        new CreateNewAccountMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.CreateAccount(login, Convert.ToInt32(pin, new CultureInfo("en-US")), name, "customer", status, Convert.ToInt32(balance, new CultureInfo("en-US"))), Times.Once);

    }

    [Fact]
    public void RunShouldNotCreateAccountIfLoginAlreadyExists()
    {
        this.mockAccountDAL.Setup(a => a.IsValidLogin(It.IsAny<string>())).Returns(true);
        new CreateNewAccountMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }
}
