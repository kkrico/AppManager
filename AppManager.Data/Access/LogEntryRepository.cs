using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class LogentryRepository : Repository<Logentry>, ILogentryRepository
    {
        public LogentryRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}