using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class IISApplicationSoapServiceRepository : Repository<IISApplicationSoapService>,
        IIISApplicationSoapServiceRepository
    {
        public IISApplicationSoapServiceRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}