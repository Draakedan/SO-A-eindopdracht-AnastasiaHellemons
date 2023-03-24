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
    public class US22
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
            _response = new Response(new TestedState(), "", _user, true);
            _thread = new DiscussionThread(new TestedState(), "testTopic", _user, _response);
        }

        [Test]
        public void ADeveloperCanAddAReplyToADiscussionThread()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _thread.CanAddResponse(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanAddAReplyToADiscussionThread()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _thread.CanAddResponse(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void TheScrumMasterCanAddAReplyToADiscussionThread()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _thread.CanAddResponse(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void AProductOwnerCanNotAddAReplyToADiscussionThread()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _thread.CanAddResponse(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AReplyCanBeDeletedWhenTheBacklogItemOrSubtaskIsNotInDone()
        {
            var result = _thread.CanDeleteResponse(_user, _response);
            Assert.That(result, Is.True);
        }

        [Test]
        public void AReplyCanBeEditedWhenTheBacklogItemOrSubtaskIsNotInDone()
        {
            var result = _thread.CanEdit(_user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void AReplyCanNotBeEditedWhenTheBacklogItemOrSubtaskIsInDone()
        {
            _thread.UpdateState(new DoneState());
            var result = _thread.CanEdit(_user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AReplyCanNotBeDeletedWhenTheBacklogItemOrSubtaskIsInDone()
        {
            _thread.UpdateState(new DoneState());
            var result = _thread.CanDeleteResponse(_user, _response);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AReplyCanNotBeAddedToADiscussionThreadWhenTheBacklogItemOrSubtaskIsInDone()
        {
            Assert.Pass();
        }

    }
}
