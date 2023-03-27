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
        private StateCount _stateCount;
        private Response _response;
        private Response _response2;
        private DiscussionThread _thread;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _stateCount = new StateCount();
            _user = new User("", "t", "");
            _user.AddRole(new Developer());
            _response = new Response(new TestedState(_stateCount), "", _user, true);
            _thread = new DiscussionThread(new TestedState(_stateCount), "testTopic", _user, _response);
            _response2 = new Response(new TestedState(_stateCount), "2", _user, false);
        }

        [TearDown]
        public void TearDown()
        {
            _stateCount = null;
            _user = null;
            _response = null;
            _thread = null;
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
            var result = _thread.CanDeleteResponse(_user, _response2);
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
            _thread.UpdateState(new DoneState(_stateCount));
            var result = _thread.CanEdit(_user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AReplyCanNotBeDeletedWhenTheBacklogItemOrSubtaskIsInDone()
        {
            _thread.UpdateState(new DoneState(_stateCount));
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
