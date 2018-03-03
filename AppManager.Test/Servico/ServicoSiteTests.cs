using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppManager.Core.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppManager.Data.Access;
using AppManager.Data.Entity;
using Moq;

namespace AppManager.Core.Servico.Tests
{
    [TestClass]
    public class ServicoSiteTests
    {
        [TestMethod]
        public void Listar_Sites_Retorna_Lista_Sites()
        {
            var sitesEncontrados = new List<IISWebsite>()
            {
                new IISWebsite {Namewebsite= "A"},
                new IISWebsite {Namewebsite= "B"}
            };
            var uow = new Mock<UnitOfWork>(string.Empty);
            var serverManager = new Mock<IServerManager>();
            serverManager.Setup(s => s.ListAllSites()).Returns(() => sitesEncontrados);

            var servicoSite = new SiteService(uow.Object, serverManager.Object);


            var sites = servicoSite.ListAllSites();

            Assert.AreEqual(sitesEncontrados.Count, sites.Count);
            Assert.IsInstanceOfType(sites, typeof(List<IISWebsite>));
        }


        [TestMethod]
        public void Listar_Sites_NaoRetorna_Nulo()
        {
            ICollection<IISWebsite> sitesEncontrados = null;
            var uow = new Mock<IUnitOfWork>();
            var serverManager = new Mock<IServerManager>();
            serverManager.Setup(s => s.ListAllSites()).Returns(() => sitesEncontrados);

            var servicoSite = new SiteService(uow.Object, serverManager.Object);

            var sites = servicoSite.ListAllSites();

            Assert.IsNotNull(sites);
            Assert.IsInstanceOfType(sites, typeof(List<IISWebsite>));
        }
    }
}