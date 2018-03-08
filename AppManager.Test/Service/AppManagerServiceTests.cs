using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppManager.Core.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using AppManager.Core.Interfaces;
using AppManager.Data.Access;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;
using AppManager.Test;
using AppManager.Test.Fake;
using Moq;

namespace AppManager.Core.Service.Tests
{
    [TestClass]
    public class AppManagerServiceTests
    {
        private Mock<AppManagerService> _appManagerService;
        private Mock<IUnitOfWork> _uow;
        private Mock<IIISServerManagerService> _iisServerManagerService;
        private Mock<AppManagerDbContext> _ctx;

        [TestInitialize]
        public void Setup()
        {
            _ctx = new Mock<AppManagerDbContext>("ConnectionString");
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(s => s.DbContext).Returns(_ctx.Object);
            _iisServerManagerService = new Mock<IIISServerManagerService>();
            _appManagerService = new Mock<AppManagerService>(_uow.Object, _iisServerManagerService.Object);
        }

        //[TestMethod]
        //public void Parse_NaoDeve_InserirWebsite_JaCadastrado()
        //{
        //    var dados = new List<IISWebSite>
        //    {
        //        new IISWebSite {IISWebSiteId = 1}
        //    };

        //    var fakedbSet = new FakeDbSet<IISWebSite>(dados);
        //    _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

        //    var foundIisWebsites = new List<FoundIISWebSite>
        //    {
        //        new FoundIISWebSite {IISId = 1, Apppollname = "AppPoolName1", IISLogPath = "Path"},
        //    };
        //    _iisServerManagerService.Setup(s => s.ListWebSites()).Returns(foundIisWebsites);
        //    _appManagerService.Object.Parse();

        //    _ctx.Verify(s => s.SaveChanges(), Times.Never());
        //}

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
            _iisServerManagerService.Setup(s => s.ListWebSites()).Returns((List<FoundIISWebSite>)null);
            var dados = new List<IISWebSite>
            {
                new IISWebSite {IISWebSiteId = 1}
            };

            var fakedbSet = new FakeDbSet<IISWebSite>(dados);
            _ctx.Setup(s => s.IISWebSite).Returns(fakedbSet);

            _appManagerService.Object.Parse();
            _ctx.Verify(s => s.SaveChanges(), Times.Never());
        }

        //[TestMethod()]
        //public void ParseTest()
        //{
        //    Assert.Fail();
        //}
    }
}