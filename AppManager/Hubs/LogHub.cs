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
       
    }
}