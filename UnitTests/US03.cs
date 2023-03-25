using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;

namespace UnitTests
{
    public class US03
    {
        public BacklogItem _backlogItem;
        public StateCount _stateCount;
        [SetUp]
        public void Setup()
        {
            _stateCount = new StateCount();
            _backlogItem = new(1, "test1", _stateCount);
        }

        [Test]
        public void ABacklogItemCanHaveSubtasks()
        {
            var subtasks = new SubTask("task1", _stateCount);
            _backlogItem.AddSubtask(subtasks);
            var result = _backlogItem.GetSubtasks();
            Assert.That(result, Is.Not.Null);
            Assert.Equals(result, "task1");
        }

        [Test]
        public void ADeveloperCanAddSubtaksToABacklogItem()
        {
            var developer = new Developer();
            var user = new User("", "", "");
            user.AddRole(developer);

            var result = _backlogItem.CanAddSubtasks(user, new SubTask("", _stateCount));

            Assert.That(result, Is.True);
        }

        [Test]
        public void AScrumMasterCanAddSubtasksToABacklogItem()
        {
            var scrumMaster = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(scrumMaster);

            var result = _backlogItem.CanAddSubtasks(user, new SubTask("", _stateCount));

            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotAddSubtasksToABacklogItem()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);

            var result = _backlogItem.CanAddSubtasks(user, new SubTask("", _stateCount));

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotAddSubtasksToABacklogItem()
        {
            var productOwner = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(productOwner);

            var result = _backlogItem.CanAddSubtasks(user, new SubTask("", _stateCount));

            Assert.That(result, Is.False);
        }

        [Test]
        public void ASubtaskHasAName()
        {
            var subtask = new SubTask("task1", _stateCount);
            var result = subtask.Name;

            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.EqualTo("task1"));
        }

        [Test]
        public void ABacklogItemHasAListOfSubtasks()
        {
            var result = _backlogItem.GetSubtasks();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("No subtasks"));
        }
    }
}
