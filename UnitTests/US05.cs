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
    public class US05
    {
        public Sprint _sprint;
        [SetUp]
        public void Setup()
        {
            _sprint = new();
        }

        [Test]
        public void AScrumMasterCanAddABacklogItemToASprint()
        {
            var scrumMaster = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(scrumMaster);

            var result = _sprint.CanEditSprint(user);
            Assert.Equals(true, result);
        }

        [Test]
        public void ADeveloperCanAddABacklogItemToASprint()
        {
            var developer = new Developer();
            var user = new User("", "", "");
            user.AddRole(developer);

            var result = _sprint.CanEditSprint(user);
            Assert.Equals(false, result);
        }

        [Test]
        public void ATesterCanNotAddABacklogItemToASprint()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);

            var result = _sprint.CanEditSprint(user);
            Assert.Equals(false, result);
        }

        [Test]
        public void AProductOwnerCanNotAddABacklogItemToASprint()
        {
            var productOwner = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(productOwner);

            var result = _sprint.CanEditSprint(user);
            Assert.Equals(false, result);
        }

        [Test]
        public void ASubtaskCanBeAddedToASprint()
        {
            var subtask = new SubTask("");
            var result = _sprint.AddBacklogItem(subtask);
            Assert.Equals(true, result);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanNotBeAddedToTheSprintIfNoSubtasksAreAddedToTheSprint()
        {
            var backlogItem = new BacklogItem(1, "");
            var subtask = new SubTask("");
            backlogItem.AddSubtask(subtask);

            var result = _sprint.AddBacklogItem(backlogItem);
            Assert.Equals(false, result);
        }

        [Test]
        public void NotAllSubtasksOfABacklogItemNeedToBeAddedToTheSprint()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            user1.AddRole(developer);
            _sprint.AddDeveloper(user1);

            var backlogItem = new BacklogItem(1, "");
            var subtask1 = new SubTask("");
            var subtask2 = new SubTask("");
            backlogItem.AddSubtask(subtask1);
            backlogItem.AddSubtask(subtask2);

            _sprint.AddBacklogItem(subtask1);
            _sprint.AddBacklogItem(backlogItem);

            var result = _sprint.CanSprintStart();
            Assert.That(result, Is.True);
        }

        [Test]
        public void ASprintWith3DevelopersCanNotHave2BacklogItemsOrSubtasks()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            var user3 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(developer);
            user3.AddRole(developer);

            _sprint.AddDeveloper(user1);
            _sprint.AddDeveloper(user2);
            _sprint.AddDeveloper(user3);

            var item1 = new BacklogItem(1, "");
            var item2 = new BacklogItem(2, "");

            _sprint.AddBacklogItem(item1);
            _sprint.AddBacklogItem(item2);

            var result = _sprint.CanSprintStart();
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASprintWith3DevelopersCanHave3BacklogItemsOrSubtasks()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            var user3 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(developer);
            user3.AddRole(developer);

            _sprint.AddDeveloper(user1);
            _sprint.AddDeveloper(user2);
            _sprint.AddDeveloper(user3);

            var item1 = new BacklogItem(1, "");
            var item2 = new BacklogItem(2, "");
            var item3 = new BacklogItem(3, "");

            _sprint.AddBacklogItem(item1);
            _sprint.AddBacklogItem(item2);
            _sprint.AddBacklogItem(item3);

            var result = _sprint.CanSprintStart();
            Assert.That(result, Is.True);
        }

        [Test]
        public void ASprintWith3DevelopersCanHave4BacklogItemsOrSubtasks()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            var user3 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(developer);
            user3.AddRole(developer);

            _sprint.AddDeveloper(user1);
            _sprint.AddDeveloper(user2);
            _sprint.AddDeveloper(user3);

            var item1 = new BacklogItem(1, "");
            var item2 = new BacklogItem(2, "");
            var item3 = new BacklogItem(3, "");
            var item4 = new BacklogItem(4, "");

            _sprint.AddBacklogItem(item1);
            _sprint.AddBacklogItem(item2);
            _sprint.AddBacklogItem(item3);
            _sprint.AddBacklogItem(item4);

            var result = _sprint.CanSprintStart();
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemCanBeRemovedFromASprint()
        {
            var item = new BacklogItem(1, "");

            _sprint.AddBacklogItem(item);
            var before = _sprint.ViewBacklog();

            _sprint.RemoveBacklogItem(item);
            var after = _sprint.ViewBacklog();

            Assert.That(after, Is.Not.EqualTo(before));
        }

        [Test]
        public void ASubtaskCanBeRemovedFromASprint()
        {
            var item = new SubTask("");

            _sprint.AddBacklogItem(item);
            var before = _sprint.ViewBacklog();

            _sprint.RemoveBacklogItem(item);
            var after = _sprint.ViewBacklog();

            Assert.That(after, Is.Not.EqualTo(before));
        }

        [Test]
        public void ASprintHasAListOfBackLogItemsAndSubtasks()
        {
            var after = _sprint.ViewBacklog();

            Assert.That(after, Is.Not.Empty);
        }

        [Test]
        public void BacklogItemsAndSubtasksStartInToDoWhenTheSprintStarts()
        {
            var todo = new ToDoState();
            var subtasks = new SubTask("");
            Assert.That(todo, Is.EqualTo(subtasks.State));
        }
    }
}
