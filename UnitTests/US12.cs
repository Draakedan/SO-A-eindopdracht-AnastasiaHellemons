using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Backlog;
using avansDevOps.Users;
using avansDevOps;

namespace UnitTests
{
    public class US12
    {
        private SubTask _subtask;
        private SubTask _subtask2;
        private SubTask _subtask3;
        private BacklogItem _backlog;
        private TestedState _state;
        private Sprint _sprint;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _backlog = new BacklogItem(1, "");
            _subtask = new SubTask("");
            _subtask2 = new SubTask("");
            _subtask3 = new SubTask("");
            _state = new TestedState();

            _backlog.State = new TestingState();
            _subtask.State = new TestingState();
            _subtask2.State = new TestingState();
            _subtask3.State = new TestingState();

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
        public void ATesterCanPlaceABacklogItemOrSubtasksInTested()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotPlaceABacklogItemOrSubtaskInTested()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void TheScrumMasterCanNotPlaceABacklogItemOrSubtaksInTested()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotPlaceABacklogItemOrSubtasksInTested()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaksCanNotBePlacedInTestedFromTodo()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new ToDoState());

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaksCanNotBePlacedInTestedFromDoing()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new DoingState());

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInTestedFromReadyForTesting()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new ReadyForTestingState());

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInTestedFromTesting()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new TestingState());

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInTestedFromDone()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new DoneState());

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void TestedCanHaveNoBacklogItemsOrSubtasks()
        {
            var result = _state.MinItems;
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TestedCanHaveOneBacklogItemOrSubtask()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestedCanHaveMultipleBacklogItemsAndSubtasks()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _subtask2.CanChangeState(user, new TestedState());
            var result = _subtask.CanChangeState(user, new TestedState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanBePlacedInTestedIfABacklogItemIsInTested()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new DoneState());
            _subtask3.ChangeState(new DoneState());

            _subtask2.CanChangeState(user, new TestedState());
            var result = _backlog.CanChangeState(user, new TestedState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanNotBePlacedInTestedIfABacklogItemIsInTestingOrLower()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);
            _subtask.ChangeState(new DoneState());
            _subtask3.ChangeState(new TestingState());

            _subtask2.CanChangeState(user, new TestedState());
            var result = _backlog.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABackligItemWithSubtasksCanNotBePlacedInTestedIfThereAreNoSubtasksInTested()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _backlog.CanChangeState(user, new TestedState());
            Assert.That(result, Is.False);
        }

    }
}
