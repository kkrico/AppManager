using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class SoapServiceRepository : Repository<SoapService>, ISoapServiceRepository
    {
        public SoapServiceRepository(AppManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}