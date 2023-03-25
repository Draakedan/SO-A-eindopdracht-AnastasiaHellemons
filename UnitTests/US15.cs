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
        private StateCount _stateCount;

        [SetUp]
        public void Setup()
        {
            _stateCount = new StateCount();
            var role = new Developer();
            _user = new("", "", "");
            _user.AddRole(role);
            _backlogItem = new BacklogItem(1, "", _stateCount);
            _customStateCanMove = new CustomState(_stateCount, "move");
            _customStateCanNotMove = new CustomState(_stateCount, "no move");
            _customStateCanMove.EditRules(1, 1, true, true, true, true, true, true, new List<string>());
            _customStateCanNotMove.EditRules(1, 1, false, false, false, false, false, false, new List<string>());
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromTodoWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanMove);
            var result = _backlogItem.CanChangeState(_user, new ToDoState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromTodoWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanNotMove);
            var result = _backlogItem.CanChangeState(_user, new ToDoState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromDoingWhenTheRulesAllowIt()
        {

            _backlogItem.AddDeveloper(_user);
            _backlogItem.ChangeState(_customStateCanMove);
            var result = _backlogItem.CanChangeState(_user, new DoingState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromDoingWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanNotMove);
            var result = _backlogItem.CanChangeState(_user, new DoingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromReadyForTestingWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanMove);
            var result = _backlogItem.CanChangeState(_user, new ReadyForTestingState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromReadyForTestingWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanNotMove);
            var result = _backlogItem.CanChangeState(_user, new ReadyForTestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromTestingWhenTheRulesAllowIt()
        {
            _user.AddRole(new Tester());
            _backlogItem.ChangeState(_customStateCanMove);
            var result = _backlogItem.CanChangeState(_user, new TestingState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromTestingWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanNotMove);
            var result = _backlogItem.CanChangeState(_user, new TestingState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromTestedWhenTheRulesAllowIt()
        {
            _user.AddRole(new Tester());
            _backlogItem.ChangeState(_customStateCanMove);
            var result = _backlogItem.CanChangeState(_user, new TestedState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromTestedWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanNotMove);
            var result = _backlogItem.CanChangeState(_user, new TestedState(_stateCount));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanBePlacedInACustomStateFromDoneWhenTheRulesAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanMove);
            var result = _backlogItem.CanChangeState(_user, new DoneState(_stateCount));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ABacklogItemOrSubtaskCanNotBePlacedInACustomStateFromDoneWhenTheRulesNotAllowIt()
        {
            _backlogItem.ChangeState(_customStateCanNotMove);
            var result = _backlogItem.CanChangeState(_user, new DoneState(_stateCount));
            Assert.That(result, Is.False);
        }

    }
}
