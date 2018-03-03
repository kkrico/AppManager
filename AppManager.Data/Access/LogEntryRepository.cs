using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class LogEntryRepository : Repository<Logentry>
    {
        public LogEntryRepository(AppManagerDbContext context) : base(context)
        {
        }
    }
}