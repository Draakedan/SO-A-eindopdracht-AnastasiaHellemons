using avansDevOps.Backlog;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US04
    {
        public SubTask _subtask;
        [SetUp]
        public void Setup()
        {
            _subtask = new("task1", new StateCount());
        }

        [Test]
        public void ADeveloperCanBeAttachedToSubTasks()
        {
            var before = _subtask.Developer;
            var developer = new Developer();
            var user = new User("", "", "");
            user.AddRole(developer);

            _subtask.AddDeveloper(user);
            var after = _subtask.Developer;

            Assert.That(before, Is.Not.EqualTo(after));
        }

        [Test]
        public void ASubtasksDoesCanNotHaveMultipleDevelopers()
        {
            var developer = new Developer();
            var user1 = new User("", "t", "");
            var user2 = new User("", "q", "");
            user1.AddRole(developer);
            user2.AddRole(developer);

            _subtask.AddDeveloper(user1);
            var result = _subtask.AddDeveloper(user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanBeDetachedFromASubtask()
        {
            var developer = new Developer();
            var user = new User("", "", "");
            user.AddRole(developer);

            _subtask.AddDeveloper(user);
            var before = _subtask.Developer;

            _subtask.RemoveDeveloper();

            var after = _subtask.Developer;

            Assert.That(before, Is.Not.EqualTo(after));
        }

        [Test]
        public void ADeveloperCanAttachHimselfToASubtaks()
        {
            var developer = new Developer();
            var user = new User("", "", "");
            user.AddRole(developer);

            var result = _subtask.CanAddUser(user, user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanAttachOtherDevelopersToASubtask()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(developer);

            var result = _subtask.CanAddUser(user2, user1);

            Assert.That(result, Is.True);
        }

        [Test]
        public void AScrumMasterCanAttachADeveloperToASubtask()
        {
            var developer = new Developer();
            var scrumMaster = new ScrumMaster();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(scrumMaster);

            var result = _subtask.CanAddUser(user2, user1);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotAttachADeveloperToASubtask()
        {
            var developer = new Developer();
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(tester);

            var result = _subtask.CanAddUser(user2, user1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotAttachADeveloperToASubtask()
        {
            var developer = new Developer();
            var productOwner = new ProductOwner();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(productOwner);

            var result = _subtask.CanAddUser(user2, user1);

            Assert.That(result, Is.False);
        }

    }
}
