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
    public class US16
    {
        private SubTask _subtask;
        private SubTask _subtask2;
        private SubTask _subtask3;
        private BacklogItem _backlog;
        private ToDoState _state;
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
            _state = new ToDoState(_stateCount);

            _backlog.State = new TestingState(_stateCount);
            _subtask.State = new TestingState(_stateCount);
            _subtask2.State = new TestingState(_stateCount);
            _subtask3.State = new TestingState(_stateCount);

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
        public void ADeveloperCanPlaceABacklogItemOrSubtaskInTodo()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanPlaceABacklogItemOrSubtaskInTodo()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void TheScrumMasterCanPlaceABacklogItemOrSubtaksInTodo()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void AProductOwnerCanNotPlaceBacklogItemOrSubtaskInTodo()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaksCanBePlacedInTodoFromDoing()
        {

            _subtask.ChangeState(new DoingState(_stateCount));
            var result = _subtask.CanChangeState(_user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInTodoFromReadyForTesting()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _subtask.ChangeState(new ReadyForTestingState(_stateCount));
            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInTodoFromTesting()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _subtask.ChangeState(new TestingState(_stateCount));
            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInTodoFromTested()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _subtask.ChangeState(new TestedState(_stateCount));
            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInTodoFromDone()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _subtask.ChangeState(new DoneState(_stateCount));
            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void TodoCanHaveNoBacklogItemsOrSubtasks()
        {
            var result = _state.MinItems;
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TodoCanHaveOneBacklogItemOrSubtask()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void TodoCanHaveMultipleBacklogItemsOrSubtasks()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _subtask2.ChangeState(new ToDoState(_stateCount));
            var result = _subtask.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithSubtaskCanBeInTodoIfASubtaskIsInTodo()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _subtask.ChangeState(new ToDoState(_stateCount));
            var result = _backlog.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanNotbeInTodoIfNoSubtasksAreInTodo()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _subtask.ChangeState(new DoingState(_stateCount));
            var result = _backlog.CanChangeState(user, new ToDoState(_stateCount));
            Assert.That(result, Is.False);
        }

    }
}
