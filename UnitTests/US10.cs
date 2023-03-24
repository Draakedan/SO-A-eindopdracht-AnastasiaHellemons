﻿using avansDevOps.Backlog.TasklStates__State_;
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
    public class US10
    {
        private SubTask _subtask;
        private SubTask _subtask2;
        private SubTask _subtask3;
        private BacklogItem _backlog;
        private ReadyForTestingState _state;
        private Sprint _sprint;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _backlog = new BacklogItem(1, "");
            _subtask = new SubTask("");
            _subtask2 = new SubTask("");
            _subtask3 = new SubTask("");
            _state = new ReadyForTestingState();

            _backlog.State = new DoingState();
            _subtask.State = new DoingState();
            _subtask2.State = new DoingState();
            _subtask3.State = new DoingState();

            _backlog.AddSubtask(_subtask);
            _backlog.AddSubtask(_subtask2);
            _backlog.AddSubtask(_subtask3);

            _sprint = new Sprint();
            _sprint.AddBacklogItem(_subtask);
            _sprint.AddBacklogItem(_subtask2);
            _sprint.AddBacklogItem(_subtask3);
            _sprint.AddBacklogItem(_backlog);
            var role = new Developer();
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
        public void AScrumMasterCanPlaceABacklogItemOrSubtaskInReadyForTesting()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new ReadyForTestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanPlaceASubtaskOrBacklogItemInReadyForTesting()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new ReadyForTestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanPlaceASubtaksOrBacklogItemInReadyForTesting()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _subtask.CanChangeState(user, new ReadyForTestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABaclogItemOrSubtaskCanNotBePalcedInReadyForTestingFromToDo()
        {
            _subtask.ChangeState(new ToDoState());

            var result = _subtask.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaksCanBePlacedInReadyForTestingFromDoing()
        {
            _subtask.ChangeState(new DoingState());

            var result = _subtask.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlaedInReadyForTestingFromTesting()
        {
            _subtask.ChangeState(new TestingState());

            var result = _subtask.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInReadyForTestingFromTested()
        {
            _subtask.ChangeState(new TestedState());

            var result = _subtask.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInReadyForTestingFromDone()
        {
            _subtask.ChangeState(new DoneState());

            var result = _subtask.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReadyForTestingCanHaveNoBacklogItemsOrSubtasks()
        {
            var result = _state.MinItems;

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void ReadyForTestingCanHaveOneBacklogItemOrSubtask()
        {
            var result = _subtask.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReadyForTestingCanHaveMultipleBacklogItemsOrSubtasks()
        {
            _subtask.CanChangeState(_user, new ReadyForTestingState());
            _subtask2.CanChangeState(_user, new ReadyForTestingState());
            var result = _subtask3.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanBeInReadyForTestingIfOneSubtaskIsInReadyForTesting()
        {
            _subtask.ChangeState(new TestingState());
            _subtask2.ChangeState(new TestingState());
            _subtask3.ChangeState(new ReadyForTestingState());
            var result = _backlog.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemWithSubtasksCanNotBePlacedInReadyForTestingIfOneSubtaskIsInDoingOrLower()
        {
            _subtask.ChangeState(new DoingState());
            _subtask2.ChangeState(new TestingState());
            _subtask3.ChangeState(new ReadyForTestingState());
            var result = _backlog.CanChangeState(_user, new ReadyForTestingState());
            Assert.That(result, Is.False);
        }
    }
}
