using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AppManager.Hubs
{
    [HubName("log")]
    public class LogHub : Hub
    {
        private static ConcurrentDictionary<string, List<int>> _mapping;

        public LogHub()
        {
            _mapping = new ConcurrentDictionary<string, List<int>>();
        }
        public override Task OnConnected()
        {
            _mapping.GetOrAdd(Context.ConnectionId, new List<int>());
            Clients.All.newConnection(Context.ConnectionId);
            return base.OnConnected();
        }
    }
}