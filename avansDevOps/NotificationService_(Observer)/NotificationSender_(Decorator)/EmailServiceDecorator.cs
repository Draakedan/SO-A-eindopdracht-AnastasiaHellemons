using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_.NotificationSender__Decorator_
{
    public class EmailServiceDecorator : MessageServiceDecorator
    {
        private Message Wrappee { get; set; }
        public EmailServiceDecorator(Message wrappee) : base(wrappee)
        {
            this.Wrappee = wrappee;
        }

        public string Send() 
        {
            return "email: " + this.Wrappee.Send(); 
        }
    }
}
