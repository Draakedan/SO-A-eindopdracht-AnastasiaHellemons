using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_
{
    public class MessagePublisher
    {
        private List<IMessageSender> subscribers;
        public MessagePublisher() 
        {
            subscribers = new();
        }
        public void Subscribe(IMessageSender sender) 
        {
            subscribers.Add(sender);
        }

        public void Unsubscribe(IMessageSender sender) 
        {
            subscribers.Remove(sender);
        }

        public void NotifySubscriber(string message, string type) 
        {
            foreach (IMessageSender subscriber in subscribers)
            {
                subscriber.SendMessage(message, type);
            }
        }
    }
}
