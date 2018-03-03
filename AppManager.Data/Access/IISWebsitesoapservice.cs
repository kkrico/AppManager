namespace AppManager.Data.Access
{
    public class IISWebsitesoapservice : Repository<IISWebsitesoapservice>
    {
        public IISWebsitesoapservice(AppManagerDbContext context) : base(context)
        {
        }
    }
}