using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class WebserverRepository : Repository<Webserver>, IWebserverRepository
    {
        public WebserverRepository(AppManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}