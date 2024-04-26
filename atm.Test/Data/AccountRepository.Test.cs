namespace Atm.Test.Data;

using System.Globalization;
using Atm.Data;

public class AccountRepositoryTest
{
    [Fact]
    public void AccountRepositoryConstructorCreatesNewAccountRepository() => new AccountRepository(
        FixtureHelper.CreateLoginInput(),
        Convert.ToInt32(FixtureHelper.CreatePinInput(), new CultureInfo("en-US")),
        FixtureHelper.CreateNameInput(),
        FixtureHelper.CreateRoleInput(),
        FixtureHelper.CreateStatusInput(),
        new Fixture().Create<int>()
        ).Should().BeOfType<AccountRepository>();
}
