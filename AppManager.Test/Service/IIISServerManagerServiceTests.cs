using System.Collections.Generic;
using System.Linq;
using AppManager.Core.Interfaces;
using AppManager.Data.Access;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppManager.Core.Service.Tests
{
    [TestClass]
    public class IIISServerManagerServiceTests
    {
        private Mock<AppManagerDbContext> _ctx;
        private Mock<IIISServerManagerService> _serverManager;
        private IISWebSiteService _siteService;
        private Mock<IUnitOfWork> _uow;

        [TestInitialize]
        public void Setup()
        {
            _ctx = new Mock<AppManagerDbContext>("connectionString");
            _uow = new Mock<IUnitOfWork>();
            _serverManager = new Mock<IIISServerManagerService>();
            _siteService = new IISWebSiteService(_uow.Object, _serverManager.Object);
        }

        [TestMethod]
        public void Listar_Sites_Retorna_Lista_Sites()
        {
            var sitesEncontrados = new List<IISWebSite>
            {
                new IISWebSite {Namewebsite = "A"},
                new IISWebSite {Namewebsite = "B"}
            };

            var mockSiteRepostory = new Mock<IIISWebSiteRepository>();
            mockSiteRepostory.Setup(s => s.List()).Returns(sitesEncontrados.AsQueryable);
            _uow.Setup(s => s.IISWebSiteRepository).Returns(mockSiteRepostory.Object);

            ICollection<IISWebSite> sites = _siteService.ListAllSites();
            Assert.AreEqual(sitesEncontrados.Count, sites.Count);
            Assert.IsInstanceOfType(sites, typeof(List<IISWebSite>));
        }


        [TestMethod]
        public void Listar_Sites_NaoRetorna_Nulo()
        {
            IQueryable<IISWebSite> sitesEncontrados = null;

            var mockSiteRepostory = new Mock<IIISWebSiteRepository>();
            mockSiteRepostory.Setup(s => s.List()).Returns(sitesEncontrados);
            _uow.Setup(s => s.IISWebSiteRepository).Returns(mockSiteRepostory.Object);

            var servicoSite = new IISWebSiteService(_uow.Object, _serverManager.Object);

            ICollection<IISWebSite> sites = servicoSite.ListAllSites();

            Assert.IsNotNull(sites);
            Assert.IsInstanceOfType(sites, typeof(List<IISWebSite>));
        }
    }
}