using System.Collections.Generic;
using AppManager.Data.Entity;

namespace AppManager.Core.Servico
{
    public interface IServerManager
    {
        ICollection<IISWebsite> ListAllSites();
    }
}