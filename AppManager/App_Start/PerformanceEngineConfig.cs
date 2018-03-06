using System.Threading.Tasks;
using AppManager.Engine;

namespace AppManager
{
    public class PerformanceEngineConfig
    {
        public static void RegisterPerformanceMonitor()
        {
            Task.Factory.StartNew(() => PerformanceEngine.Instance.IniciarMonitoramento());
        }
    }
}