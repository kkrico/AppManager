using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class ApplicationRepository : Repository<Application>
    {
        public ApplicationRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}