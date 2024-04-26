namespace Atm.Test.UserMenu;

using System;
using System.Globalization;
using Atm.Common;
using Atm.Dal;
using Atm.UserMenu;

public class UpdateAccountInformationMenuOptionTest
{
    private readonly int accountId;
    private readonly int custAccountId;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public UpdateAccountInformationMenuOptionTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.custAccountId = new Fixture().Create<int>();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void RunShouldUpdateAccountDetailsOnValidAccountIdAndNonZeroInputs()
    {
        var name = FixtureHelper.CreateNameInput();
        var status = FixtureHelper.CreateStatusInput();
        var login = FixtureHelper.CreateLoginInput();
        var pin = FixtureHelper.CreatePinInput();

        this.mockAccountDAL.Setup(a => a.IsValidAccount(this.custAccountId)).Returns(true);
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { this.custAccountId.ToString(new CultureInfo("en-US")), name, status, login, pin });

        new UpdateAccountInformationMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.GetUserName(this.custAccountId), Times.Once);
        this.mockAccountDAL.Verify(a => a.UpdateUserName(this.custAccountId, name), Times.Once);
        this.mockAccountDAL.Verify(a => a.GetStatus(this.custAccountId), Times.Once);
        this.mockAccountDAL.Verify(a => a.UpdateUserStatus(this.custAccountId, status), Times.Once);
        this.mockAccountDAL.Verify(a => a.GetUserLogin(this.custAccountId), Times.Once);
        this.mockAccountDAL.Verify(a => a.UpdateUserLogin(this.custAccountId, login), Times.Once);
        this.mockAccountDAL.Verify(a => a.GetPin(this.custAccountId), Times.Never);
        this.mockAccountDAL.Verify(a => a.UpdateUserPin(this.custAccountId, Convert.ToInt32(pin, new CultureInfo("en-US"))), Times.Once);
    }

    [Fact]
    public void RunShouldNotUpdateAccountDetailsOnBlankInputs()
    {
        this.mockAccountDAL.Setup(a => a.IsValidAccount(this.custAccountId)).Returns(true);
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { this.custAccountId.ToString(new CultureInfo("en-US")), "", "", "", "" });

        new UpdateAccountInformationMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.GetUserName(this.custAccountId), Times.Once);
        this.mockAccountDAL.Verify(a => a.UpdateUserName(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.GetStatus(this.custAccountId), Times.Once);
        this.mockAccountDAL.Verify(a => a.UpdateUserStatus(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.GetUserLogin(this.custAccountId), Times.Once);
        this.mockAccountDAL.Verify(a => a.UpdateUserLogin(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.GetPin(this.custAccountId), Times.Never);
        this.mockAccountDAL.Verify(a => a.UpdateUserPin(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void RunShouldNotGetOrUpdateAccountDetailsOnInvalidAccountId()
    {
        this.mockAccountDAL.Setup(a => a.IsValidAccount(this.custAccountId)).Returns(false);
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { this.custAccountId.ToString(new CultureInfo("en-US")) });

        new UpdateAccountInformationMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.GetUserName(It.IsAny<int>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.UpdateUserName(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.GetStatus(It.IsAny<int>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.UpdateUserStatus(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.GetUserLogin(It.IsAny<int>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.UpdateUserLogin(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.GetPin(It.IsAny<int>()), Times.Never);
        this.mockAccountDAL.Verify(a => a.UpdateUserPin(this.accountId, It.IsAny<int>()), Times.Never);
    }
}
