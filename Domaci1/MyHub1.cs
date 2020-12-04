using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Domaci1
{
    public class MyHub1 : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void SendMessage(string user, string message)
        {
           Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}