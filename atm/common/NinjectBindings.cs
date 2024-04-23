using Ninject.Modules;
using Atm.Dal;

public class NinjectBindings : NinjectModule
{
    public override void Load()
    {
        Bind<ILoginMenu>().To<LoginMenu>();
        Bind<IMenuCreator>().To<MenuCreator>();
        Bind<IMenu>().To<Menu>();
        Bind<IInputGetter>().To<InputGetter>();

        Bind<IAccountDAL>().To<AccountDAL>();
        Bind<IUserDAL>().To<UserDAL>();
        Bind<IUserDALWithUpdate>().To<UserDALWithUpdate>();
        Bind<IStatusDAL>().To<StatusDAL>();

        Bind<IWithdrawCashMenuOption>().To<WithdrawCashMenuOption>();
        Bind<IDepositCashMenuOption>().To<DepositCashMenuOption>();
        Bind<IDisplayBalanceMenuOption>().To<DisplayBalanceMenuOption>();
        Bind<ICreateNewAccountMenuOption>().To<CreateNewAccountMenuOption>();
        Bind<IDeleteExistingAccountMenuOption>().To<DeleteExistingAccountMenuOption>();
        Bind<IUpdateAccountInformationMenuOption>().To<UpdateAccountInformationMenuOption>();
        Bind<ISearchForAccountMenuOption>().To<SearchForAccountMenuOption>();

        Bind<IRegexConstants>().To<RegexConstants>();
    }
}
