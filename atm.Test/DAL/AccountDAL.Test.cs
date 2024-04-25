namespace Atm.Test.Dal;
using Atm.Data;
using Atm.Dal;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class AccountDALTest
{
    [Fact]
    public void CreateAccountAddsANewAccountSavesChangesAndReturnsId()
    {
        var mockAccountRepositorySet = new Mock<DbSet<AccountRepository>>();
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        var login = new Fixture().Create<string>();
        var pin = new Fixture().Create<int>();
        var name = new Fixture().Create<string>();
        var role = new Fixture().Create<string>();
        var status = new Fixture().Create<string>();
        var balance = new Fixture().Create<int>();
        new AccountDAL(mockContextFactory.Object).CreateAccount(login, pin, name, role, status, balance).Should().Be(It.IsAny<int>());

        mockContextFactory.Verify(c => c.CreateContext(), Times.Once);
        mockAccountRepositorySet.Verify(a => a.Add(It.Is<AccountRepository>(a => a.Login == login && a.Pin == pin && a.Name == name && a.Role == role && a.Status == status && a.Balance == balance)), Times.Once);
        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void UpdateBalanceIncreasesAccountBalanceByAmountToAdd()
    {
        var balance = new Fixture().Create<int>();
        var amountToAdd = new Fixture().Create<int>();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, balance: balance);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).UpdateBalance(amountToAdd, accountId).Should().Be(balance + amountToAdd);
        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void UpdateBalanceThrowsErrorIfBalanceWouldBeNegative()
    {
        var balance = new Fixture().Create<int>();
        var amountToAdd = (balance + new Fixture().Create<int>()) * -1;
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, balance: balance);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        Action update = () => new AccountDAL(mockContextFactory.Object).UpdateBalance(amountToAdd, accountId);
        update.Should().Throw<InvalidBalanceUpdateException>();
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void GetBalanceReturnsAccountBalance()
    {
        var balance = new Fixture().Create<int>();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, balance: balance);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).GetBalance(accountId).Should().Be(balance);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void DeleteAccountRemovesAnAccount()
    {
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).DeleteAccount(accountId).Should().Be(accountId);
        mockContextFactory.Verify(c => c.CreateContext(), Times.Once);
        mockAccountRepositorySet.Verify(a => a.Remove(It.Is<AccountRepository>(a => a.Id == accountId)), Times.Once);
        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void IsValidLoginReturnsTrueWhenLoginIsInAccountRepositorySet()
    {
        var accountId = new Fixture().Create<int>();
        var login = new Fixture().Create<string>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, login: login);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).IsValidLogin(login).Should().Be(true);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void IsValidLoginReturnsFalseWhenLoginIsntInAccountRepositorySet()
    {
        var accountId = new Fixture().Create<int>();
        var login = new Fixture().Create<string>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).IsValidLogin(login).Should().Be(false);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void IsValidAccountReturnsTrueWhenAccountIdIsInAccountRepositorySet()
    {
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).IsValidAccount(accountId).Should().Be(true);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void IsValidAccountReturnsFalseWhenAccountIdIsntInAccountRepositorySet()
    {
        var accountIds = new Fixture().CreateMany<int>(2).ToArray();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountIds[0]);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).IsValidAccount(accountIds[1]).Should().Be(false);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void IsValidPinReturnsTrueWhenLoginPinComboIsInAccountRepositorySet()
    {
        var accountId = new Fixture().Create<int>();
        var login = new Fixture().Create<string>();
        var pin = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, login: login, pin: pin);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).IsValidPin(login, pin).Should().Be(true);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void IsValidPinReturnsFalseWhenLoginPinComboIsntInAccountRepositorySet()
    {
        var accountId = new Fixture().Create<int>();
        var login = new Fixture().Create<string>();
        var pin = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).IsValidPin(login, pin).Should().Be(false);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void IsAdminReturnsTrueWhenRoleIsAdmin()
    {
        var accountId = new Fixture().Create<int>();
        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, role: "admin");
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).IsAdmin(accountId).Should().BeTrue();
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void IsAdminReturnsFalseWhenRoleIsNotAdmin()
    {
        var accountId = new Fixture().Create<int>();
        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, role: "customer");
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).IsAdmin(accountId).Should().BeFalse();
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void GetUserLoginReturnsLogin()
    {
        var login = new Fixture().Create<string>();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, login: login);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).GetUserLogin(accountId).Should().Be(login);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void GetUserNameReturnsName()
    {
        var name = new Fixture().Create<string>();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, name: name);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).GetUserName(accountId).Should().Be(name);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void GetStatusReturnsStatus()
    {
        var status = new Fixture().Create<string>();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, status: status);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).GetStatus(accountId).Should().Be(status);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void GetRoleReturnsRole()
    {
        var role = new Fixture().Create<string>();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, role: role);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).GetRole(accountId).Should().Be(role);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void GetPinReturnsPin()
    {
        var pin = new Fixture().Create<int>();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, pin: pin);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).GetPin(accountId).Should().Be(pin);
        mockContext.Verify(c => c.SaveChanges(), Times.Never);
    }

    [Fact]
    public void UpdateUserNameUpdatesName()
    {
        var names = new Fixture().CreateMany<string>().ToArray();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, name: names[0]);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).UpdateUserName(accountId, names[1]).Should().Be(accountId);
        mockContext.Object.Account.Single(a => a.Id == accountId).Name.Should().Be(names[1]);
    }

    [Fact]
    public void UpdateUserStatusUpdatesStatus()
    {
        var statuses = new Fixture().CreateMany<string>().ToArray();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, status: statuses[0]);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).UpdateUserStatus(accountId, statuses[1]).Should().Be(accountId);
        mockContext.Object.Account.Single(a => a.Id == accountId).Status.Should().Be(statuses[1]);
    }

    [Fact]
    public void UpdateUserLoginUpdatesLogin()
    {
        var logins = new Fixture().CreateMany<string>().ToArray();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, login: logins[0]);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).UpdateUserLogin(accountId, logins[1]).Should().Be(accountId);
        mockContext.Object.Account.Single(a => a.Id == accountId).Login.Should().Be(logins[1]);
    }

    [Fact]
    public void UpdateUserPin()
    {
        var pins = new Fixture().CreateMany<int>().ToArray();
        var accountId = new Fixture().Create<int>();

        var mockAccountRepositorySet = MockHelper.CreateQueriableMockAccountRepositorySet(accountId, pin: pins[0]);
        var mockContext = MockHelper.CreateMockContext(mockAccountRepositorySet);
        var mockContextFactory = MockHelper.CreateMockIContextFactory(mockContext);

        new AccountDAL(mockContextFactory.Object).UpdateUserPin(accountId, pins[1]).Should().Be(accountId);
        mockContext.Object.Account.Single(a => a.Id == accountId).Pin.Should().Be(pins[1]);
    }
}
