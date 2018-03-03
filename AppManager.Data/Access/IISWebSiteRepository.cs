using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class IISWebSiteRepository : Repository<IISWebsite>
    {
        public IISWebSiteRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}