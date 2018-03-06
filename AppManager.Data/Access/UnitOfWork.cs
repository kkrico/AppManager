using AppManager.Data.Access.Interfaces;

namespace AppManager.Data.Access
{
    public class UnitOfWork : IUnitOfWork
    {
        private IApplicationRepository _applicationRepository;
        private IIISApplicationRepository _iisApplicationRepository;
        private IIISApplicationSoapServiceRepository _iisApplicationSoapServiceRepository;
        private IIISWebSiteRepository _iisWebSiteRepository;
        private ILogentryRepository _logentryRepository;
        private ISoapEndpointRepository _soapEndpointRepository;
        private ISoapServiceRepository _soapServiceRepository;
        private IWebserverRepository _webserverRepository;


        public UnitOfWork(AppManagerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public AppManagerDbContext DbContext { get; }

        public IApplicationRepository ApplicationRepository =>
            _applicationRepository ?? (_applicationRepository = new ApplicationRepository(DbContext));

        public IIISApplicationRepository IISApplicationRepository =>
            _iisApplicationRepository ?? (_iisApplicationRepository = new IISApplicationRepository(DbContext));

        public IIISApplicationSoapServiceRepository IISApplicationSoapServiceRepository =>
            _iisApplicationSoapServiceRepository ?? (_iisApplicationSoapServiceRepository =
                new IISApplicationSoapServiceRepository(DbContext));

        public IIISWebSiteRepository IISWebSiteRepository =>
            _iisWebSiteRepository ?? (_iisWebSiteRepository = new IISWebSiteRepository(DbContext));

        public ILogentryRepository LogentryRepository =>
            _logentryRepository ?? (_logentryRepository = new LogentryRepository(DbContext));

        public ISoapEndpointRepository SoapEndpointRepository =>
            _soapEndpointRepository ?? (_soapEndpointRepository = new SoapEndpointRepository(DbContext));

        public ISoapServiceRepository SoapServiceRepository =>
            _soapServiceRepository ?? (_soapServiceRepository = new SoapServiceRepository(DbContext));

        public IWebserverRepository WebserverRepository =>
            _webserverRepository ?? (_webserverRepository = new WebserverRepository(DbContext));

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}