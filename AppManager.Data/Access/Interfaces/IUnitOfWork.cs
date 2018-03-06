namespace AppManager.Data.Access.Interfaces
{
    public interface IUnitOfWork
    {
        AppManagerDbContext DbContext { get; }
        IApplicationRepository ApplicationRepository { get; }
        IIISApplicationRepository IISApplicationRepository { get; }
        IIISApplicationSoapServiceRepository IISApplicationSoapServiceRepository { get; }
        IIISWebSiteRepository IISWebSiteRepository { get; }
        ILogentryRepository LogentryRepository { get; }
        ISoapEndpointRepository SoapEndpointRepository { get; }
        ISoapServiceRepository SoapServiceRepository { get; }
        IWebserverRepository WebserverRepository { get; }
        void SaveChanges();
    }
}