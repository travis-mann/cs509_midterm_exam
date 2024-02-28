
public class NoRoleFoundException: Exception
{

}

internal class MenuCreator : IMenuCreator
{
    private IInputGetter _InputGetter;
    private Dictionary<string, IMenuOption[]> menuItemMap;

    public MenuCreator(
        IInputGetter InputGetter, 
        IWithdrawCashMenuOption WithdrawCashMenuOption,
        IDisplayBalanceMenuOption DisplayBalanceMenuOption,
        IDepositCashMenuOption DepositCashMenuOption,
        ICreateNewAccountMenuOption CreateNewAccountMenuOption,
        IDeleteExistingAccountMenuOption DeleteExistingAccountMenuOption,
        IUpdateAccountInformationMenuOption UpdateAccountInformationMenuOption,
        ISearchForAccountMenuOption SearchForAccountMenuOption
    )
    {
        _InputGetter = InputGetter;

        menuItemMap = new(){
        { "customer",
            new IMenuOption[] {
                WithdrawCashMenuOption,
                DisplayBalanceMenuOption,
                DepositCashMenuOption
            }
        },
        { "admin",
            new IMenuOption[] {
                CreateNewAccountMenuOption,
                DeleteExistingAccountMenuOption,
                UpdateAccountInformationMenuOption,
                SearchForAccountMenuOption
            }
        },
    };
    }

    public IMenu GetMenu(int user_id)
    {
        string role = GetUserRole(user_id);
        return new Menu(menuItemMap[role], _InputGetter);
    }

    private string GetUserRole(int user_id)
    {
        // get user role from db
        string[] roles;
        using (var context = new Context())
        {
            var user = context.User;
            var role = context.Role;

            IQueryable<string> query = context.User
            .Join(context.Role,
                u => u.role_id,
                r => r.id,
                (u, r) => new { User = u, Role = r })
            .Where(x => x.User.id == user_id)
            .Select(x => x.Role.name);
            roles = query.ToArray();
        }

        // check if role found
        if (roles.Length > 0)
        {
            return roles[0];
        }
        else
        {
            throw new NoRoleFoundException();
        }
    }
}
