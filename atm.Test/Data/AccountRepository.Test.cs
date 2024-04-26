namespace Atm.Test.Data;
using Atm.Data;

public class AccountRepositoryTest
{
    [Fact]
    public void AccountRepositoryConstructorCreatesNewAccountRepository() => new AccountRepository(
        FixtureHelper.CreateLoginInput(),
        Convert.ToInt32(FixtureHelper.CreatePinInput()),
        FixtureHelper.CreateNameInput(),
        FixtureHelper.CreateRoleInput(),
        FixtureHelper.CreateStatusInput(),
        new Fixture().Create<int>()
        ).Should().BeOfType<AccountRepository>();
}
