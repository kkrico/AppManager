using System;
using System.Collections.Generic;
using System.Linq;
using AppManager.Core.Interfaces;
using AppManager.Data.Access;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;
using AppManager.Test.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppManager.Core.Service.Tests
{
    [TestClass]
    public class AppManagerServiceTests
    {
        private Mock<AppManagerService> _appManagerService;
        private Mock<AppManagerDbContext> _ctx;
        private Mock<IIISServerManagerService> _iisServerManagerService;
        private Mock<IUnitOfWork> _uow;

        [TestInitialize]
        public void Setup()
        {
            _ctx = new Mock<AppManagerDbContext>("ConnectionString");
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(s => s.DbContext).Returns(_ctx.Object);
            _iisServerManagerService = new Mock<IIISServerManagerService>();
            _appManagerService = new Mock<AppManagerService>(_uow.Object, _iisServerManagerService.Object);
        }

        [TestMethod]
        public void Parse_NaoDeve_InserirWebsite_JaCadastrado_ComMesmoId_ComMesmoNome_ComMesmoAppPool()
        {
            var dados = new List<IISWebSite>
            {
                new IISWebSite {IISWebSiteId = 1, Apppollname = "AppPoolName1", Iislogpath = "Path"}
            };

            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

            var foundIisWebsites = new List<FoundIISWebSite>
            {
                new FoundIISWebSite {IISId = 1, Apppollname = "AppPoolName1", IISLogPath = "Path"}
            };
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
            _appManagerService.Object.Parse();

            Assert.AreEqual(_ctx.Object.IISWebSite.Count(), dados.Count);
        }

        [TestMethod]
        public void Parse_Insere_Se_AppPoolIISWebSite_For_Diferente()
        {
            var dados = new List<IISWebSite>
            {
                new IISWebSite {IISWebSiteId = 1, Apppollname = "WebSiteNomeAppPool", Iislogpath = "Path"}
            };

            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

            var foundIisWebsites = new List<FoundIISWebSite>
            {
                new FoundIISWebSite {IISId = 1, Apppollname = "AppPoolName1", IISLogPath = "Path"}
            };
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
            _appManagerService.Object.Parse();

            Assert.AreEqual(_ctx.Object.IISWebSite.First().Apppollname, foundIisWebsites.First().Apppollname);
        }

        [TestMethod]
        public void Parse_Insere_Se_NomeIISWebSite_For_Diferente()
        {
            var dados = new List<IISWebSite>
            {
                new IISWebSite {IISWebSiteId = 1, Namewebsite = "IISWebSiteNome", Iislogpath = "Path"}
            };

            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

            var foundIisWebsites = new List<FoundIISWebSite>
            {
                new FoundIISWebSite {IISId = 1, Apppollname = "AppPoolName1", IISLogPath = "Path"}
            };
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
            _appManagerService.Object.Parse();

            Assert.AreEqual(_ctx.Object.IISWebSite.First().Namewebsite, foundIisWebsites.First().Namewebsite);
        }

        [TestMethod]
        public void Parse_Fecha_Vigencia_Se_Nao_Encontrar()
        {
            var dados = new List<IISWebSite>
            {
                new IISWebSite {IISWebSiteId = 1, Namewebsite = "IISWebSiteNome", Iislogpath = "Path"}
            };

            _uow.Setup(s => s.IISApplicationRepository).Returns(new Mock<IIISApplicationRepository>().Object);
            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

            var foundIisWebsites = new List<FoundIISWebSite>
            {
                new FoundIISWebSite {IISId = 2, Apppollname = "AppPool2"}
            };
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
            _appManagerService.Object.Parse();

            Assert.IsNotNull(_ctx.Object.IISWebSite.First().Enddate);
        }

        [TestMethod]
        public void Parse_Fecha_Vigencia_Somente_Vigente()
        {
            var dados = new List<IISWebSite>
            {
                new IISWebSite
                {
                    IISWebSiteId = 1,
                    Namewebsite = "IISWebSiteNome",
                    Iislogpath = "Path",
                    Enddate = DateTime.Now.Date.AddDays(-99)
                }
            };

            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

            var foundIisWebsites = new List<FoundIISWebSite>
            {
                new FoundIISWebSite {IISId = 2, Apppollname = "AppPool2"}
            };
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
            _appManagerService.Object.Parse();

            Assert.AreEqual(_ctx.Object.IISWebSite.First().Enddate, DateTime.Now.Date.AddDays(-99));
        }

        [TestMethod]
        public void Parse_Insere_Novos_IISWebSites()
        {
            var dados = new List<IISWebSite>();

            var fakeDbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakeDbSet);


            var foundIisWebsites = new List<FoundIISWebSite>
            {
                new FoundIISWebSite {IISId = 2, Apppollname = "AppPool2"},
                new FoundIISWebSite {IISId = 1, Apppollname = "AppPoolName1", IISLogPath = "Path"}
            };
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
            _appManagerService.Object.Parse();

            Assert.IsTrue(_ctx.Object.IISWebSite.Any());
            Assert.AreEqual(_ctx.Object.IISWebSite.Count(), foundIisWebsites.Count);
            Assert.AreEqual(_ctx.Object.IISWebSite.First().Namewebsite, foundIisWebsites.First().Namewebsite);
            Assert.IsNull(_ctx.Object.IISWebSite.First().Enddate);
        }

        [TestMethod]
        public void Parse_NaoSalva_SeNaoAchar_Site()
        {
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(new List<FoundIISWebSite>());
            var dados = new List<IISWebSite>
            {
                new IISWebSite {IISWebSiteId = 1}
            };

            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

            _appManagerService.Object.Parse();
            _ctx.Verify(s => s.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void Parse_NaoSalva_SeNaoAchar_Site_RetornoNulo()
        {
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns((List<FoundIISWebSite>) null);
            var dados = new List<IISWebSite>
            {
                new IISWebSite {IISWebSiteId = 1}
            };

            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

            _appManagerService.Object.Parse();
            _ctx.Verify(s => s.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void Parse_Notifica_QueFazParseDaEntitidade_IISWEbSIte()
        {
            var dados = new List<IISWebSite>();

            var fakeDbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakeDbSet);


            var foundIisWebsites = new List<FoundIISWebSite>
            {
                new FoundIISWebSite {IISId = 2, Apppollname = "AppPool2"},
                new FoundIISWebSite {IISId = 1, Apppollname = "AppPoolName1", IISLogPath = "Path"}
            };

            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
            var isCalled = false;
            _appManagerService.Object.OnEntityParsed += (name, type) => isCalled = name == nameof(IISWebSite);
            _appManagerService.Object.Parse();

            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public void Parse_Fecha_Vigencia_Application_Se_SiteFecharVigencia()
        {
            var dados = new List<IISWebSite>
            {
                new IISWebSite {IISWebSiteId = 1, Namewebsite = "IISWebSiteNome", Iislogpath = "Path"}
            };

            var applications = new List<IISApplication>
            {
                new IISApplication
                {
                    Creationdate = DateTime.Now,
                    Enddate = null,
                    Idiiswebsite = dados.First().IISWebSiteId
                }
            };
            var mockApplicationRepository = new Mock<IIISApplicationRepository>();
            mockApplicationRepository.Setup(s => s.List()).Returns(applications.AsQueryable());
            _uow.Setup(s => s.IISApplicationRepository).Returns(mockApplicationRepository.Object);

            var fakeDbSetApplications = new FakeDbSet<IISApplication>(applications);
  
               
            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);
            _ctx.Setup(s => s.IISApplication).Returns(fakeDbSetApplications);
            var foundIisWebsites = new List<FoundIISWebSite>
            {
                new FoundIISWebSite {IISId = 2, Apppollname = "AppPool2"}
            };
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
            _appManagerService.Object.Parse();

            Assert.IsNotNull(_ctx.Object.IISWebSite.First().Enddate);
            Assert.IsNotNull(_ctx.Object.IISApplication.First().Enddate);
        }
    }
}