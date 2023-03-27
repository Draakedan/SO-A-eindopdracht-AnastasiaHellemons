using avansDevOps.Backlog.TasklStates__State_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_.NotificationSender__Decorator_
{
    public class Message : IMessage
    {
        public string MessageToSend { get; private set; }
        public Message(string message) 
        {
            this.MessageToSend = message;
        }
        public string Send() 
        {
            return MessageToSend; 
        }
    }
}
