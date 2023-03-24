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
    public class US11
    {
        private SubTask _subtask;
        private SubTask _subtask2;
        private SubTask _subtask3;
        private BacklogItem _backlog;
        private TestingState _state;
        private Sprint _sprint;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _backlog = new BacklogItem(1, "");
            _subtask = new SubTask("");
            _subtask2 = new SubTask("");
            _subtask3 = new SubTask("");
            _state = new TestingState();

            _backlog.State = new ReadyForTestingState();
            _subtask.State = new ReadyForTestingState();
            _subtask2.State = new ReadyForTestingState();
            _subtask3.State = new ReadyForTestingState();

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
        public void ATesterCanPlaceSubtasksOrBacklogItemsInTesting()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotPlaceSubtasksOrBacklogItemsInTesting()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void TheScrumMasterCanNotPlaceSubtasksOrBacklogItemsInTesting()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotPlaceSubtasksOrBacklogItemsInTesting()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtasksOrBacklogItemCanNotBePlacedInTestingFromTodo()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new ToDoState());

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaskOrBacklogItemCanNotBePlacedInTestingFromDoing()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new DoingState());

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaskOrBaclogItemCanBePlacedInTestingFromReadyForTesting()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new ReadyForTestingState());

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ASubtaskOrBacklogItemCanNotBePlacedInTestingFromTested()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new TestedState());

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaskOrBacklogItemCanNotBePlacedInTestingFromDone()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new DoneState());

            var result = _subtask.CanChangeState(user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanHaveOneBacklogItemOrSubtaskInTesting()
        {
            var result = _subtask.CanChangeState(_user, new TestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotHaveMutipleBacklogItemsOrSubtasksInTesting()
        {
            _subtask.CanChangeState(_user, new TestingState());
            var result = _subtask.CanChangeState(_user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void TestingCanHaveNoSubtasksOrBacklogItems()
        {
            var result = _state.MinItems;
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TestingCanHaveLessSubtasksOrBacklogItemsThanThereAreTestersInTheSprint()
        {
            var result = _subtask.CanChangeState(_user, new TestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestingCanHaveTheSameAmountOfSubtasksOrBacklogItemsAsThereAreTestersInTheSprint()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);
            _subtask2.RemoveDeveloper();
            _subtask2.AddDeveloper(user);

            _subtask2.CanChangeState(user, new TestingState());
            var result = _subtask.CanChangeState(_user, new TestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestingCanNotHaveMoreSubtasksOrBacklogItemsInThanThereAreTestersInTheSprint()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            var user2 = new User("", "", "");
            user.AddRole(tester);
            user2.AddRole(tester);
            _subtask2.RemoveDeveloper();
            _subtask2.AddDeveloper(user);
            _subtask3.RemoveDeveloper();
            _subtask3.AddDeveloper(user2);

            _subtask3.CanChangeState(user2, new TestingState());
            _subtask2.CanChangeState(user, new TestingState());
            var result = _subtask.CanChangeState(_user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanBePlacedInTestingWhenABacklogItemIsInTesting()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);
            _backlog.RemoveDeveloper();
            _backlog.AddDeveloper(user);

            _subtask2.ChangeState(new TestedState());
            _subtask3.ChangeState(new TestedState());


            _subtask.CanChangeState(user, new TestingState());
            var result = _backlog.CanChangeState(_user, new TestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithSubtasksIsNotPlacedInTestedWhenASubtaskIsInReadyForTestingOrLower()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);
            _backlog.RemoveDeveloper();
            _backlog.AddDeveloper(user);

            _subtask2.ChangeState(new ReadyForTestingState());
            _subtask3.ChangeState(new TestedState());


            _subtask.CanChangeState(user, new TestingState());
            var result = _backlog.CanChangeState(_user, new TestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanNotBePlacedInTestingWhenNoBacklogItemsAreInTesting()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);
            _backlog.RemoveDeveloper();
            _backlog.AddDeveloper(user);

            _subtask.ChangeState(new ToDoState());
            _subtask2.ChangeState(new ToDoState());
            _subtask3.ChangeState(new ToDoState());

            var result = _backlog.CanChangeState(_user, new TestingState());
            Assert.That(result, Is.False);
        }
    }
}
