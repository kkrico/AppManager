using System.Collections.Generic;
using AppManager.Data.Entity;

namespace AppManager.Core.Servico
{
    public interface ISiteService
    {
        ICollection<IISWebsite> ListAllSites();
        IISWebsite GetSite(int siteId);
        ICollection<string> ListarUrlsAppLog();
    }
}