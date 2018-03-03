namespace AppManager.Data.Access
{
    public interface IUnitOfWork
    {
        AppManagerDbContext DbContext { get; }

        IISWebSiteRepository IISWebSiteRepository { get; }
        ApplicationRepository ApplicationRepository { get; }
        LogEntryRepository LogEntryRepository { get; }
        SoapendpointRepository SoapendpointRepository { get; }
        SoapServiceRepository SoapServiceRepository { get; }
        WebserverRepository WebserverRepository { get; }

        void SaveChanges();
    }
}