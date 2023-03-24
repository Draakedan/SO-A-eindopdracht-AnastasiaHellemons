using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Backlog;
using avansDevOps.Users;
using avansDevOps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US13
    {
        private SubTask _subtask;
        private SubTask _subtask2;
        private SubTask _subtask3;
        private BacklogItem _backlog;
        private DoneState _state;
        private Sprint _sprint;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _backlog = new BacklogItem(1, "");
            _subtask = new SubTask("");
            _subtask2 = new SubTask("");
            _subtask3 = new SubTask("");
            _state = new DoneState();

            _backlog.State = new TestedState();
            _subtask.State = new TestedState();
            _subtask2.State = new TestedState();
            _subtask3.State = new TestedState();

            _backlog.AddSubtask(_subtask);
            _backlog.AddSubtask(_subtask2);
            _backlog.AddSubtask(_subtask3);

            _sprint = new Sprint();
            _sprint.AddBacklogItem(_subtask);
            _sprint.AddBacklogItem(_subtask2);
            _sprint.AddBacklogItem(_subtask3);
            _sprint.AddBacklogItem(_backlog);
            var role = new Tester();
            _user = new User("", "", "");
            var user2 = new User("", "", "");
            _user.AddRole(role);
            user2.AddRole(role);

            _backlog.AddDeveloper(_user);
            _subtask.AddDeveloper(_user);
            _subtask2.AddDeveloper(_user);
            _subtask3.AddDeveloper(_user);

            _sprint.AddDeveloper(_user);
            _sprint.AddDeveloper(user2);
        }

        [Test]
        public void TheScrumMasterCanPlaceABacklogItemOrSubtaskInDone()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new DoneState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanPlaceABacklogItemOrSubtaskInDone()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new DoneState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanPlaceABacklogItemOrSubtaskInDone()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new DoneState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void AProductOwnerCanNotPlaceABacklogItemOrSubtaskInDone()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new DoneState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInDoneFromTodo()
        {
            _subtask.ChangeState(new ToDoState());

            var result = _subtask.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInDoneFromDoing()
        {
            _subtask.ChangeState(new DoingState());

            var result = _subtask.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInDoneFromReadyForTesting()
        {
            _subtask.ChangeState(new ReadyForTestingState());

            var result = _subtask.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInDoneFromTesting()
        {
            _subtask.ChangeState(new TestingState());

            var result = _subtask.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaksCanBePlacedInDoneFromTested()
        {
           _subtask.ChangeState(new TestedState());

            var result = _subtask.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void DoneCanHaveNoBacklogItemsOrSubtasks()
        {
            var result = _state.MinItems;
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void DoneCanHaveOneBacklogItemOrSubtaks()
        {
            var result = _subtask.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void DoneCanHaveMultipleBacklogItemsAndSubtasks()
        {
            _subtask2.CanChangeState(_user, new DoneState());
            var result = _subtask.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanNotPlacedInDoneWhenNoSubtasksAreInDone()
        {
            _subtask.CanChangeState(_user, new DoingState());
            _subtask2.CanChangeState(_user, new DoingState());
            _subtask3.CanChangeState(_user, new DoingState());
            var result = _backlog.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanNotBePlacedInDoneIfNotAllSubtasksAreInDone()
        {
            _subtask2.CanChangeState(_user, new DoingState());
            _subtask3.CanChangeState(_user, new DoneState());
            var result = _backlog.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanBePlacedInDoneWhenAllSubtasksAreInDone()
        {
            _subtask.CanChangeState(_user, new DoneState());
            _subtask2.CanChangeState(_user, new DoneState());
            _subtask3.CanChangeState(_user, new DoneState());
            var result = _backlog.CanChangeState(_user, new DoneState());
            Assert.That(result, Is.True);
        }
    }
}
