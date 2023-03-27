using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_.NotificationSender__Decorator_
{
    public interface IMessage
    {
        string MessageToSend { get; }
        string Send();
    }
}
