using avansDevOps.NotificationService__Observer_.NotificationSender__Decorator_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.NotificationService__Observer_
{
    public class MessageReciever : IMessageSender
    {
        public string Message { get; private set; }

        public MessageReciever(string message)
        {
            this.Message = message;
        }

        public void SendMessage(string message, string type)
        {
            Message message1 = new Message(message);
            this.Message = type switch
            {
                "email" => new EmailServiceDecorator(message1).Send(),
                "slack" => new SlackServiceDecorator(message1).Send(),
                "email slack" => new EmailServiceDecorator(message1).Send() +
                                        "\n" + new SlackServiceDecorator(message1).Send(),
                "slack email" => new SlackServiceDecorator(message1).Send() +
                                        "\n" + new EmailServiceDecorator(message1).Send(),
                _ => "",
            };
        }
    }
}
