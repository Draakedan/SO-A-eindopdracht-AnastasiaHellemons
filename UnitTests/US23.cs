using avansDevOps.Backlog.DiscussuionForum;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US23
    {
        private Response _response;
        private DiscussionThread _thread;
        private BackLogDiscussionForum _forum;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _user = new User("", "", "");
            _user.AddRole(new Developer());
            _forum = new BackLogDiscussionForum(new TestedState());
            _response = new Response(new TestedState(), "", _user, false);
            _thread = new DiscussionThread(new TestedState(), "testTopic", _user, _response);
        }

        [Test]
        public void ThePosterOfAReplyCanEditTheReply()
        {
            var result = _response.CanEdit(_user, "");
            Assert.That(result, Is.True);
        }

        [Test]
        public void AnotherUserOtherThanThePosterOfAReplyCanNotEditTheReply()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _response.CanEdit(user, "");
            Assert.That(result, Is.False);
        }

        [Test]
        public void ThePosterOfAReplyCanDeleteTheReply()
        {
            var result = _thread.CanDeleteResponse(_user, _response);
            Assert.That(result, Is.True);
        }

        [Test]
        public void AnotherUserOtherThanThePosterOfAReplyCanNotDeleteTheReply()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _thread.CanDeleteResponse(user, _response);
            Assert.That(result, Is.False);
        }


    }
}
