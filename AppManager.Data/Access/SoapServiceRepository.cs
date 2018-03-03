using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class SoapServiceRepository : Repository<Soapservice>
    {
        public SoapServiceRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}