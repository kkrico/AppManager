using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AppManager.Hubs
{
    [HubName("log")]
    public class LogHub : Hub
    {
    }
}