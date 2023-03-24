using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_.NotificationSender__Decorator_
{
    public class EmailServiceDecorator : MessageServiceDecorator
    {
        public EmailServiceDecorator(Message wrappee) : base(wrappee)
        {
        }

        public string Send(string message) { return string.Empty; }
    }
}
