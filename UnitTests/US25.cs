using avansDevOps.NotificationService__Observer_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US25
    {
        private MessagePublisher _publisher;
        private MessageReciever _reciever;
        [SetUp]
        public void Setup()
        {
            _publisher = new MessagePublisher();
            _reciever = new MessageReciever("test");
            _publisher.Subscribe(_reciever);
        }

        [Test]
        public void AUserCanGetAMessageThrougSlack()
        {
            _publisher.NotifySubscriber("test", "slack");
            var result = _reciever.Message;
            Assert.That(result, Is.EqualTo("slack: test"));
        }

        [Test]
        public void AUserCanGetAMessageThrougEmail()
        {
            _publisher.NotifySubscriber("test", "email");
            var result = _reciever.Message;
            Assert.That(result, Is.EqualTo("email: test"));
        }

        [Test]
        public void AUserCanGetAMessageThroughBothEmailAndSlack()
        {
            _publisher.NotifySubscriber("test", "slack email");
            var result = _reciever.Message;
            Assert.That(result, Is.EqualTo("slack: test\nemail: test"));
        }

    }
}
