
public class NoRoleFoundException: Exception
{

}

internal class MenuCreator : IMenuCreator
{
    // build pre-configured menus
    private Dictionary<string, IMenuOption[]> menuItemMap = new(){
        { "customer", 
            new IMenuOption[] { 
                new WithdrawCashMenuOption(),
                new DisplayBalanceMenuOption(),
                new DepositCashMenuOption()
            }
        },
        { "admin",
            new IMenuOption[] {
                new CreateNewAccountMenuOption(),
                new DeleteExistingAccountMenuOption(),
                new UpdateAccountInformationMenuOption(),
                new SearchForAccountMenuOption()
            }
        },
    };

    private IInputGetter _InputGetter;

    public MenuCreator(IInputGetter InputGetter)
    {
        _InputGetter = InputGetter;
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
