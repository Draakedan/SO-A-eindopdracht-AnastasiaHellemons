using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_
{
    public class MessagePublisher
    {
        public void subscribe(IMessageSender sender) { }
        public void Unsubscribe(IMessageSender sender) { }
        public void NotifySubscriber(string message, string type) { }
    }
}
