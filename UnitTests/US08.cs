using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US08
    {
        private Sprint _sprint;

        [SetUp]
        public void Setup()
        {
            _sprint = new();
            _sprint.EditSprint(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3), "");
        }

        [Test]
        public void TheScrumMasterCanNotEditASprintAferTheStartDateHasPassed()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _sprint.CanEditSprint(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotEditASprintAfterTheStartDateHasPassed()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _sprint.CanEditSprint(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotEditASprintAfterTheStartDateHasPassed()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _sprint.CanEditSprint(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotEditASprintAfterTheStartDateHasPassed()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _sprint.CanEditSprint(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AfterTheStartOfASprintBacklogItemsCanNotBeAdded()
        {
            var backlogItem = new BacklogItem(2, "");
            var result = _sprint.AddBacklogItem(backlogItem);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AfterTheStartOfASprintSubtasksCanNotBeAdded()
        {
            var backlogItem = new SubTask("");
            var result = _sprint.AddBacklogItem(backlogItem);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotBeAddedToTheSprintAfterTheStartDateHasPassed()
        {
            var role1 = new ScrumMaster();
            var role2 = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(role1);
            user2.AddRole(role2);

            var result = _sprint.CanAddDeveloper(user1, user2);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotBeAddedToTheSprintAfterTheStartDateHasPassed()
        {
            var role1 = new ScrumMaster();
            var role2 = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(role1);
            user2.AddRole(role2);

            var result = _sprint.CanAddTester(user1, user2);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotBeRemovedFromASprintAfterTheStartDateHasPassed()
        {
            var role1 = new ScrumMaster();
            var role2 = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(role1);
            user2.AddRole(role2);

            var result = _sprint.CanRemoveDeveloper(user1, user2);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotBeRemovedFromASprintAfterTheStartDateHasPassed()
        {
            var role1 = new ScrumMaster();
            var role2 = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(role1);
            user2.AddRole(role2);

            var result = _sprint.CanRemoveTester(user1, user2);
            Assert.That(result, Is.False);
        }
    }
}
