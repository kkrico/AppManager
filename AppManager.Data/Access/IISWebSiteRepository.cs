using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class IISWebSiteRepository : Repository<IISWebSite>, IIISWebSiteRepository
    {
        public IISWebSiteRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}