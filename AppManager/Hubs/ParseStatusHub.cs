using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AppManager.Hubs
{
    [HubName("parseStatus")]
    public class ParseStatusHub : Hub
    {
    }
}