using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class SoapendpointRepository : Repository<Soapendpoint>
    {
        public SoapendpointRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}