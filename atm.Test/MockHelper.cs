namespace Atm.Test;

using Atm.Common;
using Atm.Data;
using Microsoft.EntityFrameworkCore;

internal class MockHelper
{
    public static Mock<DbSet<AccountRepository>> CreateQueriableMockAccountRepositorySet(
        int accountId,
        string? login = null,
        int? pin = null,
        string? name = null,
        string? role = null,
        string? status = null,
        int? balance = null
    )
    {
        var accountRepository = new AccountRepository(
                login ?? new Fixture().Create<string>(),
                pin ?? new Fixture().Create<int>(),
                name ?? new Fixture().Create<string>(),
                role ?? new Fixture().Create<string>(),
                status ?? new Fixture().Create<string>(),
                balance ?? new Fixture().Create<int>()
                )
        {
            Id = accountId
        };

        var data = new List<AccountRepository>
        {
            accountRepository
        }.AsQueryable();

        var accountRepositorySet = new Mock<DbSet<AccountRepository>>();
        accountRepositorySet.As<IQueryable<AccountRepository>>().Setup(m => m.Provider).Returns(data.Provider);
        accountRepositorySet.As<IQueryable<AccountRepository>>().Setup(m => m.Expression).Returns(data.Expression);
        accountRepositorySet.As<IQueryable<AccountRepository>>().Setup(m => m.ElementType).Returns(data.ElementType);
        accountRepositorySet.As<IQueryable<AccountRepository>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
        return accountRepositorySet;
    }

    public static Mock<DbSet<AccountRepository>> CreateQueriableMockAccountRepositorySet(List<Mock<AccountRepository>> accountRepositoryList)
    {
        var data = accountRepositoryList.AsQueryable();

        var accountRepositorySet = new Mock<DbSet<AccountRepository>>();
        accountRepositorySet.As<IQueryable<AccountRepository>>().Setup(m => m.Provider).Returns(data.Provider);
        accountRepositorySet.As<IQueryable<AccountRepository>>().Setup(m => m.Expression).Returns(data.Expression);
        accountRepositorySet.As<IQueryable<AccountRepository>>().Setup(m => m.ElementType).Returns(data.ElementType);
        accountRepositorySet.As<IQueryable<AccountRepository>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
        return accountRepositorySet;
    }

    public static Mock<Context> CreateMockContext(Mock<DbSet<AccountRepository>> accountRepositorySet)
    {
        var context = new Mock<Context>();
        var accountRepository = new Mock<DbSet<AccountRepository>>();
        context.Setup(c => c.Account).Returns(accountRepositorySet.Object);
        return context;

    }

    public static Mock<IContextFactory> CreateMockIContextFactory(Mock<Context> mockContext)
    {
        var contextFactory = new Mock<IContextFactory>();
        contextFactory.Setup(c => c.CreateContext()).Returns(mockContext.Object);
        return contextFactory;
    }

    public static void SetupInputSequence(Mock<IInputGetter> mockInputGetter, string[] inputs)
    {
        var mockInputGetterSequence = mockInputGetter.SetupSequence(i => i.GetInput(It.IsAny<Func<string, bool>>(), It.IsAny<string>(), It.IsAny<string>()));
        foreach (var input in inputs)
        {
            mockInputGetterSequence.Returns(input);
        }
    }
}
