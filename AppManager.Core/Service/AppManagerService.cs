using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using AppManager.Core.Interfaces;
using AppManager.Data.Access.Interfaces;

namespace AppManager.Core.Service
{
    public class AppManagerService : IAppManagerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IIISServerManagerService _iiisServerManagerService;

        public AppManagerService(IUnitOfWork uow, IIISServerManagerService iiisServerManagerService)
        {
            _uow = uow;
            _iiisServerManagerService = iiisServerManagerService;
        }

        public void Parse()
        {
            var foundWebSites = _iiisServerManagerService.ListWebSites();
            if (foundWebSites == null)
                throw new InvalidOperationException();

            var ctx = _uow.DbContext;

            ctx.IISWebSite.AddOrUpdate(u => new{u.Idiiswebsite});
        }
    }

    public interface IAppManagerService
    {
        void Parse();
    }
}
