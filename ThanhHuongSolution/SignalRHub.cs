using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ThanhHuongSolution
{
    public class SignalRHub : Hub
    {
        /*
        public void Send(string name, string message)
        {
            //Clients.All.broadcastMessage(name, message);
            Clients.All.receiveMessage(name, message);
        }*/

        public void Send()
        {
            Clients.All.broadcastMessage();
        }

        public void SendToUpdatePrice()
        {
            Clients.All.updatePrice();
        }
    }
}