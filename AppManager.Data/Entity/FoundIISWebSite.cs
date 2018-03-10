using System.Collections.Generic;

namespace AppManager.Data.Entity
{
    public class FoundIISWebSite
    {
        public string Namewebsite { get; set; }
        public string Apppollname { get; set; }
        public long IISId { get; set; }
        public string IISLogPath { get; set; }
        public ICollection<FoundIISApplication> IISApplications { get; set; }
        public string PhysicalPath { get; set; }
    }

    public class FoundIISApplication
    {
        public int IISWebSiteId { get; set; }
        public string ApplicationName { get; set; }
        public string PhysicalPath { get; set; }
        public string AppPoolName { get; set; }
        public string IISLogicalPath { get; set; }
    }
}