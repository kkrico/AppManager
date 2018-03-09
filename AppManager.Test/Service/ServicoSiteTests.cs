using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using AppManager.Core.Interfaces;
using AppManager.Core.Service;
using AppManager.Data.Access;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;
using Moq;

namespace AppManager.Core.Service.Tests
{
    [TestClass]
    public class ServicoSiteTests
    {
        private Mock<AppManagerDbContext> _ctx;
        private Mock<IUnitOfWork> _uow;
        private Mock<IIISServerManagerService> _serverManager;
        private IISWebSiteService _siteService;

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
                new IISWebSite {Namewebsite= "A"},
                new IISWebSite {Namewebsite= "B"}
            };

            var mockSiteRepostory = new Mock<IIISWebSiteRepository>();
            mockSiteRepostory.Setup(s => s.GetAll()).Returns(sitesEncontrados.AsQueryable);
            _uow.Setup(s => s.IISWebSiteRepository).Returns(mockSiteRepostory.Object);

            var sites = _siteService.ListAllSites();
            Assert.AreEqual(sitesEncontrados.Count, sites.Count);
            Assert.IsInstanceOfType(sites, typeof(List<IISWebSite>));
        }


        [TestMethod]
        public void Listar_Sites_NaoRetorna_Nulo()
        {
            IQueryable<IISWebSite> sitesEncontrados = null;

            var mockSiteRepostory = new Mock<IIISWebSiteRepository>();
            mockSiteRepostory.Setup(s => s.GetAll()).Returns(sitesEncontrados);
            _uow.Setup(s => s.IISWebSiteRepository).Returns(mockSiteRepostory.Object);

            var servicoSite = new IISWebSiteService(_uow.Object, _serverManager.Object);

            var sites = servicoSite.ListAllSites();

            Assert.IsNotNull(sites);
            Assert.IsInstanceOfType(sites, typeof(List<IISWebSite>));
        }
    }
}