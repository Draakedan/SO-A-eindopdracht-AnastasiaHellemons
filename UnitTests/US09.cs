using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US09
    {
        private SubTask _subtask;
        private SubTask _subtask2;
        private SubTask _subtask3;
        private BacklogItem _backlog;
        private DoingState _state;
        private Sprint _sprint;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _backlog = new BacklogItem(1, "");
            _subtask = new SubTask("");
            _subtask2 = new SubTask("");
            _subtask3 = new SubTask("");
            _state = new DoingState();

            _backlog.AddSubtask(_subtask);
            _backlog.AddSubtask(_subtask2);
            _backlog.AddSubtask(_subtask3);

            _sprint = new Sprint();
            _sprint.AddBacklogItem(_subtask);
            _sprint.AddBacklogItem(_subtask2);
            _sprint.AddBacklogItem(_subtask3);
            _sprint.AddBacklogItem(_backlog);
            var role = new Developer();
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
        public void ADeveloperWhoIsLinkedToASubtaskCanPlaceItInDoing()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.AddDeveloper(user);

            var result = _subtask.CanChangeState(user, new DoingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperWhoIsNotLinkedToABacklogItemCanNotPlaceItInDoing()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaskCanBePlacedInDoingFromTodo()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.AddDeveloper(user);

            var result = _subtask.CanChangeState(user, new DoingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ASubtaksCanNotBePlacedInDoingFromReadyForTesting()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.AddDeveloper(user);
            _subtask.ChangeState(new ReadyForTestingState());

            var result = _subtask.CanChangeState(user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaksCanNotBePlacedInDoingFromTesting()
        {
            _subtask.ChangeState(new TestingState());

            var result = _subtask.CanChangeState(_user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaksCanNotBePlacedInDoingFromTested()
        {
            _subtask.ChangeState(new TestedState());

            var result = _subtask.CanChangeState(_user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaksCanNotBePlacedInDoingFromDone()
        {
            _subtask.ChangeState(new DoneState());

            var result = _subtask.CanChangeState(_user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanHaveOneSubtaskOrBacklogItemInDoing()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.AddDeveloper(user);

            var result = _subtask.CanChangeState(user, new DoingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotHaveTwoSubtasksOrBacklogItesmInDoing()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.AddDeveloper(user);
            _subtask.ChangeState(new ReadyForTestingState());
            var subtask2 = new SubTask("");
            subtask2.AddDeveloper(user);

            _subtask.CanChangeState(user, new DoingState());
            var result = subtask2.CanChangeState(user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void DoingCanHaveNoSubtasksOrBacklogItems()
        {
            var result = _state.MinItems;

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void DoingCanHaveLessSubtasksOrBacklogItemsThanDevelopersInTheSprint()
        {

            var result = _subtask.CanChangeState(_user, new DoingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void DoingCanHaveTheSameAmountOfBacklogItemsOrSubtaksAsThereAreDevelopersInTheSprint()
        {
            _subtask.CanChangeState(_user, new DoingState());
            var result = _subtask2.CanChangeState(_user, new DoingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void DoingCanNotHaveMoreSubtasksOrBacklogItemsThanThereAreDevelopersInTheSprint()
        {
            _subtask.CanChangeState(_user, new DoingState());
            _subtask2.CanChangeState(_user, new DoingState());
            var result = _subtask3.CanChangeState(_user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void TheScrumMasterCanNotPlaceSubtasksOrBacklogItemsInDoing()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotPlaceSubtasksOrBacklogItemsInDoing()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotPlaceSubtasksOrBacklogItemsInDoing()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanNotBeInDoingIfThereAreNoSubtasksInDoing()
        {
            var result = _backlog.CanChangeState(_user, new DoingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemCanNotBeInDoingIfASubtasksIsInTodo()
        {
            _subtask.ChangeState(new DoingState());
            _subtask2.ChangeState(new ToDoState());

            var result = _backlog.CanChangeState(_user, new DoingState());
            Assert.That(result, Is.False);
        }

    }
}
