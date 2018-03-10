using System.Collections.Generic;
using AppManager.Data.Entity;

namespace AppManager.Core.Interfaces
{
    public interface IIISServerManagerService
    {
        ICollection<FoundIISWebSite> ListWebSites();
    }
}