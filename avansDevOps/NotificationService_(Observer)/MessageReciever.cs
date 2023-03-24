using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_
{
    public class MessageReciever : IMessageSender
    {
        public string mesage { get; private set; }
        public void SendMessage(string message, string type) { }
    }
}
