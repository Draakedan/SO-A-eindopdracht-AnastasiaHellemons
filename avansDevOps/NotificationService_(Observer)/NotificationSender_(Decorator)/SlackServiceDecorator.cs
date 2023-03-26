using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_.NotificationSender__Decorator_
{
    public class SlackServiceDecorator : MessageServiceDecorator
    {
        private Message Wrappee { get; set; } 
        public SlackServiceDecorator(Message wrappee) : base(wrappee)
        {
            this.Wrappee = wrappee;
        }
        public string Send() 
        {
            return "slack: " + this.Wrappee.Send(); 
        }
    }
}
