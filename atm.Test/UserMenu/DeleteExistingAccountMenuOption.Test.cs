namespace Atm.Test.UserMenu;

using System.Globalization;
using Atm.Test;
using Atm.UserMenu;
using Atm.Common;
using Atm.Dal;

public class DeleteExistingAccountMenuOptionTest
{
    private readonly int accountId;
    private readonly int[] custAccountIds;
    private readonly Mock<IInputGetter> mockInputGetter;
    private readonly Mock<IAccountDAL> mockAccountDAL;

    public DeleteExistingAccountMenuOptionTest()
    {
        this.accountId = new Fixture().Create<int>();
        this.custAccountIds = new Fixture().CreateMany<int>(2).ToArray();
        this.mockInputGetter = new Mock<IInputGetter>();
        this.mockAccountDAL = new Mock<IAccountDAL>();
    }

    [Fact]
    public void RunShouldNotDeleteAnInvalidAccount()
    {
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] { this.custAccountIds[0].ToString(new CultureInfo("en-US")) });
        this.mockAccountDAL.Setup(a => a.IsValidAccount(this.custAccountIds[0])).Returns(false);
        new DeleteExistingAccountMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.DeleteAccount(It.IsAny<int>()), Times.Never);

    }

    [Fact]
    public void RunShouldNotDeleteAccountIfIdsDontMatch()
    {
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] {
            this.custAccountIds[0].ToString(new CultureInfo("en-US")),
            this.custAccountIds[1].ToString(new CultureInfo("en-US"))
        });
        this.mockAccountDAL.Setup(a => a.IsValidAccount(this.custAccountIds[0])).Returns(true);

        new DeleteExistingAccountMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.DeleteAccount(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void RunShouldDeleteAccountWithValidMatchingIds()
    {
        MockHelper.SetupInputSequence(this.mockInputGetter, new string[] {
            this.custAccountIds[0].ToString(new CultureInfo("en-US")),
            this.custAccountIds[0].ToString(new CultureInfo("en-US"))
        });
        this.mockAccountDAL.Setup(a => a.IsValidAccount(this.custAccountIds[0])).Returns(true);

        new DeleteExistingAccountMenuOption().Run(this.accountId, this.mockInputGetter.Object, this.mockAccountDAL.Object);
        this.mockAccountDAL.Verify(a => a.DeleteAccount(this.custAccountIds[0]), Times.Once);
    }
}
