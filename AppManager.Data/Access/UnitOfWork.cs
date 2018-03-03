using System;

namespace AppManager.Data.Access
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IISWebSiteRepository> _iisWebSiteRepository;
        private readonly Lazy<ApplicationRepository> _applicationRepository;
        private readonly Lazy<LogEntryRepository> _logEntryRepository;
        private readonly Lazy<SoapendpointRepository> _soapendpointRepository;
        private readonly Lazy<SoapServiceRepository> _soapServiceRepository;
        private readonly Lazy<WebserverRepository> _webserverRepository;

        public AppManagerDbContext DbContext { get; }

        public UnitOfWork(AppManagerDbContext dbContext)
        {
            DbContext = dbContext;

            _iisWebSiteRepository = new Lazy<IISWebSiteRepository>();
            _applicationRepository = new Lazy<ApplicationRepository>();
            _logEntryRepository = new Lazy<LogEntryRepository>();
            _soapendpointRepository = new Lazy<SoapendpointRepository>();
            _soapServiceRepository = new Lazy<SoapServiceRepository>();
            _webserverRepository = new Lazy<WebserverRepository>();
        }

        public IISWebSiteRepository IISWebSiteRepository => _iisWebSiteRepository.Value;
        public ApplicationRepository ApplicationRepository => _applicationRepository.Value;
        public LogEntryRepository LogEntryRepository => _logEntryRepository.Value;
        public SoapendpointRepository SoapendpointRepository => _soapendpointRepository.Value;
        public SoapServiceRepository SoapServiceRepository => _soapServiceRepository.Value;
        public WebserverRepository WebserverRepository => _webserverRepository.Value;


        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}