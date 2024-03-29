using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;

namespace UnitTests
{
    [TestFixture]
    public class US01
    {
        private Sprint? _sprint;
        private StateCount _stateCount;

        [SetUp]
        public void Setup()
        {
            _stateCount = new StateCount();
            _sprint = new Sprint();
        }

        [TearDown]
        public void Teardown() 
        {
            _sprint = null;
        }

        [Test]
        public void AListForBacklogItemsExist()
        {
            var result = _sprint!.ViewBacklog();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void BackLogItemsAreInPriorityOrder()
        {
            var backlogItem1 = new BacklogItem(1, "test1", _stateCount);
            var backlogItem2 = new BacklogItem(2, "test2", _stateCount);

            _sprint!.AddBacklogItem(backlogItem2);
            _sprint.AddBacklogItem(backlogItem1);

            var result = _sprint.ViewBacklog();
            Assert.That(result, Is.EqualTo("test1, test2, "));
        }

        [Test]
        public void ABacklogItemWithNameCanBeAddedToTheList()
        {
            var namedBacklogItem = new BacklogItem(1, "name", _stateCount);

            var result = _sprint!.AddBacklogItem(namedBacklogItem);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithoutNameCanNotBeAddedToTheList()
        {
            var namelessBacklogItem = new BacklogItem(1, string.Empty, _stateCount);

            var result = _sprint!.AddBacklogItem(namelessBacklogItem);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemListCanNotHaveNoItems()
        {
            var user = new User("", "", "");
            var developer = new Developer();
            user.AddRole(developer);
            var user2 = new User("", "", "");
            var tester = new Tester();
            user2.AddRole(tester);

            _sprint!.AddTester(user2);
            _sprint.AddDeveloper(user);

            var result = _sprint.CanSprintStart();
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemListCanHaveOneItem()
        {
            var backlogItem1 = new BacklogItem(1, "test1", _stateCount);
            var user = new User("", "", "");
            var developer = new Developer();
            user.AddRole(developer);
            var user2 = new User("", "", "");
            var tester = new Tester();
            user2.AddRole(tester);

            _sprint!.AddTester(user2);
            _sprint.AddDeveloper(user);

            _sprint!.AddBacklogItem(backlogItem1);


            var result = _sprint.CanSprintStart();
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemListCanHaveMultipleItems()
        {
            var backlogItem1 = new BacklogItem(1, "test1", _stateCount);
            var backlogItem2 = new BacklogItem(2, "test2", _stateCount);

            _sprint!.AddBacklogItem(backlogItem2);
            _sprint.AddBacklogItem(backlogItem1);

            var user = new User("", "", "");
            var developer = new Developer();
            user.AddRole(developer);
            var user2 = new User("", "", "");
            var tester = new Tester();
            user2.AddRole(tester);

            _sprint!.AddTester(user2);
            _sprint.AddDeveloper(user);


            var result = _sprint.CanSprintStart();
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanCreateBackLogItems()
        {
            var user = new User("", "", "");
            var developer = new Developer();
            user.AddRole(developer);

            var result = _sprint!.CanEditSprint(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void AScrumMasterCanCreateBackLogItems()
        {
            var user = new User("", "", "");
            var scrumMaster = new ScrumMaster();
            user.AddRole(scrumMaster);

            var result = _sprint!.CanEditSprint(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanCreateBackLogItems()
        {
            var user = new User("", "", "");
            var tester = new Tester();
            user.AddRole(tester);

            var result = _sprint!.CanEditSprint(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void AProductOwnerCanNotCreateBackLogItems()
        {
            var user = new User("", "", "");
            var productOwner = new ProductOwner();
            user.AddRole(productOwner);

            var result = _sprint.CanEditSprint(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemHasAName()
        {
            var backlogItem1 = new BacklogItem(1, "test1", _stateCount);
            var result = backlogItem1.Name;
            Assert.That(result, Is.Not.Empty);
        }

    }
}