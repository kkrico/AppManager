using System.Collections.Generic;
using AppManager.Data.Entity;

namespace AppManager.Core.Interfaces
{
    public interface IIISWebSiteService
    {
        ICollection<IISWebSite> ListAllSites();
    }
}