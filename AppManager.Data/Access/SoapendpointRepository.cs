using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class SoapEndpointRepository : Repository<SoapEndpoint>, ISoapEndpointRepository
    {
        public SoapEndpointRepository(AppManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}