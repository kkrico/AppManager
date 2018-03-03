using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class WebserverRepository : Repository<Webserver>
    {
        public WebserverRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}