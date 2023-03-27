using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System.ComponentModel.DataAnnotations;

namespace UnitTests
{
    public class US02
    {
        public BacklogItem _backlogItem;
        public StateCount _stateCount;
        [SetUp]
        public void Setup()
        {
            _stateCount = new StateCount();
            _backlogItem = new(1, "DemoItem", _stateCount);
        }

        [Test]
        public void ADeveloperCanBeLinkedWithABackLogItem()
        {
            var before = _backlogItem.Developer;
            var developer = new Developer();
            var user = new User("", "t", "");
            user.AddRole(developer);

            _backlogItem.AddDeveloper(user);
            var after = _backlogItem.Developer;
            Assert.That(after, Is.Not.Null);
        }

        [Test]
        public void ABackLogItemCanOnlyHaveOneDeveloperLinked()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(developer);

            _backlogItem.AddDeveloper(user1);
            var result = _backlogItem.AddDeveloper(user2);
            Assert.That(result, Is.Not.True);
        }

        [Test]
        public void ADeveloperCanBeDetatchedFromABackLogItem()
        {
            var developer = new Developer();
            var user = new User("", "", "");
            user.AddRole(developer);

            _backlogItem.AddDeveloper(user);
            var before = _backlogItem.Developer;

            _backlogItem.RemoveDeveloper();
            var after = _backlogItem.Developer;

            Assert.That(after, Is.Not.EqualTo(before));
            Assert.That(after, Is.Null);
        }

        [Test]
        public void ADeveloperCanAttachHimselfToABacklogItem()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            user1.AddRole(developer);

            var result = _backlogItem.CanAddUser(user1, user1);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanAttatchOtherDevelopersToABacklogItem()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(developer);

            var result = _backlogItem.CanAddUser(user1, user2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void AScrumMasterCanNotBeAttatchedToABacklogItem()
        {
            var scrumMaster = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(scrumMaster);

            var result = _backlogItem.AddDeveloper(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotBeAttachedToABacklogItem()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);

            var result = _backlogItem.AddDeveloper(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProjectOwnerCanNotBeAttachedToABacklogItem()
        {
            var productOwner = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(productOwner);

            var result = _backlogItem.AddDeveloper(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AScrumMasterCanAttachDevelopersToABacklogItem()
        {
            var developer = new Developer();
            var scrumMaster = new ScrumMaster();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(scrumMaster);

            var result = _backlogItem.CanAddUser(user2, user1);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotAttachDevelopersToABacklogItem()
        {
            var developer = new Developer();
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(tester);

            var result = _backlogItem.CanAddUser(user2, user1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotAttachDevelopersToABacklogItem()
        {
            var developer = new Developer();
            var productOwner = new ProductOwner();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(productOwner);

            var result = _backlogItem.CanAddUser(user2, user1);

            Assert.That(result, Is.False);
        }
    }
}
