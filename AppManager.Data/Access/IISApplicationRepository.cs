using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class IISApplicationRepository : Repository<IISApplication>, IIISApplicationRepository
    {
        public IISApplicationRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}