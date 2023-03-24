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
    public class US15
    {
        private CustomState _customStateCanMove;
        private CustomState _customStateCanNotMove;
        private BacklogItem _backlogItem;
        private User _user;

        [SetUp]
        public void Setup()
        {
            var role = new Developer();
            _user = new("", "", "");
            _user.AddRole(role);
            _backlogItem = new BacklogItem(1, "");
            _customStateCanMove = new CustomState();
            _customStateCanNotMove = new CustomState();
            _customStateCanMove.EditRules(1, 1, true, true, true, true, true, true, new List<string>());
            _customStateCanNotMove.EditRules(1, 1, false, false, false, false, false, false, new List<string>());
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromTodoWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(new ToDoState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanMove);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromTodoWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(new ToDoState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanNotMove);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromDoingWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(new DoingState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanMove);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromDoingWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(new DoingState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanNotMove);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromReadyForTestingWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(new ReadyForTestingState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanMove);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromReadyForTestingWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(new ReadyForTestingState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanNotMove);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromTestingWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(new TestingState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanMove);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromTestingWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(new TestingState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanNotMove);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromTestedWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(new TestedState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanMove);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromTestedWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(new TestedState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanNotMove);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromDoneWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(new DoneState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanMove);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromDoneWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(new DoneState());
            var result = _backlogItem.CanChangeState(_user, _customStateCanNotMove);
            Assert.That(result, Is.False);
        }

    }
}
