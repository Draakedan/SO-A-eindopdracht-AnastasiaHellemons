using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_.NotificationSender__Decorator_
{
    public class MessageServiceDecorator : IMessage
    {
        private Message wrappee { get; set; }
        public string MessageToSend { get; set; }

        public MessageServiceDecorator(Message wrappee)
        {
            this.MessageToSend = wrappee.MessageToSend;
            this.wrappee = wrappee;
        }
        public string Send()
        {
            return "decorated" + wrappee.Send();
        }
    }
}
