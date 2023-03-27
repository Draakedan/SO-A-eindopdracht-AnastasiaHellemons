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
        private StateCount _stateCount;
        [SetUp]
        public void Setup()
        {
            _stateCount = new StateCount();
            _backlog = new BacklogItem(1, "", _stateCount);
            _subtask = new SubTask("", _stateCount);
            _subtask2 = new SubTask("", _stateCount);
            _subtask3 = new SubTask("", _stateCount);
            _state = new TestingState(_stateCount);

            _backlog.State = new ReadyForTestingState(_stateCount);
            _subtask.State = new ReadyForTestingState(_stateCount);
            _subtask2.State = new ReadyForTestingState(_stateCount);
            _subtask3.State = new ReadyForTestingState(_stateCount);

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

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotPlaceSubtasksOrBacklogItemsInTesting()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void TheScrumMasterCanNotPlaceSubtasksOrBacklogItemsInTesting()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotPlaceSubtasksOrBacklogItemsInTesting()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtasksOrBacklogItemCanNotBePlacedInTestingFromTodo()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new ToDoState(_stateCount));

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaskOrBacklogItemCanNotBePlacedInTestingFromDoing()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new DoingState(_stateCount));

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaskOrBaclogItemCanBePlacedInTestingFromReadyForTesting()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new ReadyForTestingState(_stateCount));

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ASubtaskOrBacklogItemCanNotBePlacedInTestingFromTested()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new TestedState(_stateCount));

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaskOrBacklogItemCanNotBePlacedInTestingFromDone()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new DoneState(_stateCount));

            var result = _subtask.CanChangeState(user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanHaveOneBacklogItemOrSubtaskInTesting()
        {
            var result = _subtask.CanChangeState(_user, new TestingState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotHaveMutipleBacklogItemsOrSubtasksInTesting()
        {
            _subtask.CanChangeState(_user, new TestingState(_stateCount));
            var result = _subtask.CanChangeState(_user, new TestingState(_stateCount));
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
            var result = _subtask.CanChangeState(_user, new TestingState(_stateCount));
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

            _subtask2.CanChangeState(user, new TestingState(_stateCount));
            var result = _subtask.CanChangeState(_user, new TestingState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestingCanNotHaveMoreSubtasksOrBacklogItemsInThanThereAreTestersInTheSprint()
        {
            _subtask3.ChangeState( new TestingState(_stateCount));
            _subtask2.ChangeState(new TestingState(_stateCount));
            var result = _subtask.CanChangeState(_user, new TestingState(_stateCount, 2));
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

            _subtask2.ChangeState(new TestingState(_stateCount));
            _subtask3.ChangeState(new TestingState(_stateCount));
            _subtask.ChangeState(new TestingState(_stateCount));
            var result = _backlog.CanChangeState(_user, new TestingState(_stateCount));
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

            _subtask2.ChangeState(new ReadyForTestingState(_stateCount));
            _subtask3.ChangeState(new TestedState(_stateCount));


            _subtask.CanChangeState(user, new TestingState(_stateCount));
            var result = _backlog.CanChangeState(_user, new TestingState(_stateCount));
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

            _subtask.ChangeState(new ToDoState(_stateCount));
            _subtask2.ChangeState(new ToDoState(_stateCount));
            _subtask3.ChangeState(new ToDoState(_stateCount));

            var result = _backlog.CanChangeState(_user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }
    }
}
