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
    public class US21
    {
        private StateCount _stateCount;
        private Response _response;
        private DiscussionThread _thread;
        private BackLogDiscussionForum _forum;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _stateCount = new StateCount();
            _user = new User("", "", "");
            _user.AddRole(new Developer());
            _forum = new BackLogDiscussionForum(new TestedState(_stateCount));
            _response = new Response(new TestedState(_stateCount), "", _user, true);
            _thread = new DiscussionThread(new TestedState(_stateCount), "testTopic", _user, _response);
        }

        [Test]
        public void ADeveloperCanStartADiscussionThread()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _forum.CanAddDiscussionThread(user, _thread);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanStartADiscussionThread()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _forum.CanAddDiscussionThread(user, _thread);
            Assert.That(result, Is.True);
        }

        [Test]
        public void TheScrumMasterCanStartADiscussionThread()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _forum.CanAddDiscussionThread(user, _thread);
            Assert.That(result, Is.True);
        }

        [Test]
        public void AProductOwnerCanNotStartADiscussionThread()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _forum.CanAddDiscussionThread(user, _thread);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanHaveOneDiscussionThread()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _forum.CanAddDiscussionThread(user, _thread);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanHaveMultipleDiscussionThreads()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            _forum.CanAddDiscussionThread(user, _thread);
            var result = _forum.CanAddDiscussionThread(user, _thread);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADiscussionThreadHasASubject()
        {
            var result = _thread.Topic;
            Assert.That(result, Is.EqualTo("testTopic"));
        }

        [Test]
        public void TheSubjectOfADiscussionThreadCanBeEditedAfterCreatingTheDiscussionThread()
        {
            var result = _thread.CanEdit(_user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADiscussionThreadCanNotBeMadeIfABacklogItemOrSubtasksIsInDone()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            _forum.UpdateState(new DoneState(_stateCount));
            var result = _forum.CanAddDiscussionThread(user, _thread);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ADiscussionThreadCanHaveMultiplePosts()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            _forum.CanAddDiscussionThread(user, _thread);
            var result = _forum.CanAddDiscussionThread(user, _thread);
            Assert.That(result, Is.True);
        }

        [Test]
        public void TheFirstPostOfADiscussionThreadCanBeEdited()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _response.CanEdit(user, "");
            Assert.That(result, Is.True);
        }

        [Test]
        public void TheFirstPostOfADiscussionThreadCanNotBeDeleted()
        {
            var result = _thread.CanDeleteResponse(_user, _response);
            Assert.That(result, Is.False);
        }

        [Test]
        public void TheContentOfTheFirstPostCanNotBeEditedWhenABacklogItemOrSubtaksIsInDone()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            _response.ChangeState(new DoneState(_stateCount));
            var result = _response.CanEdit(user, "");
            Assert.That(result, Is.True);
        }

    }
}
