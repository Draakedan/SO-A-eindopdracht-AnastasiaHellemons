using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog.TasklStates__State_
{
    public class CustomState : IState
    {
        public int MaxItems { get; set; }
        public int MinItems { get; set; }
        private bool CanGoToToDo { get; set; }
        private bool CanGoToDoing { get; set; }
        private bool CanGoToReadyForTesting { get; set; }
        private bool CanGoToTesting { get; set; }
        private bool CanGoToTested { get; set; }
        private bool CanGoToDone { get; set; }
        private List<String> PossibleCustomStates { get; set; }
        public string Name { get; init; }
        public StateCount Counter { get; set; }

        public CustomState(StateCount counter, string name)
        {
            this.MinItems = 0;
            this.MaxItems = int.MaxValue;
            this.Counter = counter;
            this.Name = name;
            this.PossibleCustomStates = new();
            this.CanGoToDoing = false;
            this.CanGoToDone = false;
            this.CanGoToReadyForTesting = false;
            this.CanGoToTested = false;
            this.CanGoToTesting = false;
            this.CanGoToToDo = false;
        }
        public void AddPossibleCustomState(string state)
        {
            PossibleCustomStates.Add(state);
        }
        public void RemovePossibleCustomState(string state)
        {
            PossibleCustomStates.Remove(state);
        }
        public bool IsStatePossible(User user, IState state)
        {
            var canChange = state.GetType().ToString() switch
            {
                "avansDevOps.Backlog.TasklStates__State_.ToDoState" => ChangeStateToToDo(user),
                "avansDevOps.Backlog.TasklStates__State_.DoingState" => ChangeStateToDoing(user, state.MaxItems),
                "avansDevOps.Backlog.TasklStates__State_.ReadyForTestingState" => ChangeStateToReadyForTesting(user),
                "avansDevOps.Backlog.TasklStates__State_.TestingState" => ChangeStateToTesting(user, state.MaxItems),
                "avansDevOps.Backlog.TasklStates__State_.TestedState" => ChangeStateToTested(user),
                "avansDevOps.Backlog.TasklStates__State_.DoneState" => ChangeStateToDone(user),
                _ => ChangeStateToCustom(user, (CustomState)state),
            };
            return canChange;
        }
        public bool ChangeStateToToDo(User user)
        {
            if (!user.HasRole(typeof(ProductOwner).ToString()))
                return CanGoToToDo;
            return false;
        }
        public bool ChangeStateToDoing(User user, int items)
        {
            if (user.HasRole(typeof(Developer).ToString()))
                return CanGoToDoing;
            return false;
        }
        public bool ChangeStateToReadyForTesting(User user)
        {
            if (!user.HasRole(typeof(ProductOwner).ToString()))
                return CanGoToReadyForTesting;
            return false;
        }
        public bool ChangeStateToTesting(User user, int items)
        {
            if (user.HasRole(typeof(Tester).ToString()))
                return CanGoToTesting;
            return false;
        }
        public bool ChangeStateToTested(User user)
        {
            if (user.HasRole(typeof(Tester).ToString()))
                return CanGoToTested;
            return false;
        }
        public bool ChangeStateToDone(User user)
        {
            if (!user.HasRole(typeof(ProductOwner).ToString()))
                return CanGoToDone;
            return false;
        }
        public bool CanEditRules(User user, int max, int min, bool todo, bool doing, bool readyForTesting, bool testing, bool tested, bool done, List<string> customs)
        {
            if (user.HasRole(typeof(ScrumMaster).ToString()))
            {
                EditRules(max, min, todo, doing, readyForTesting, testing, tested, done, customs);
                return true;
            }
            return false;
        }
        public void EditRules(int max, int min, bool todo, bool doing, bool readyForTesting, bool testing, bool tested, bool done, List<string> customs)
        {
            this.MaxItems = max;
            this.MinItems = min;
            this.CanGoToToDo = todo;
            this.CanGoToDoing = doing;
            this.CanGoToReadyForTesting = readyForTesting;
            this.CanGoToTesting = testing;
            this.CanGoToTested = tested;
            this.CanGoToDone = done;
            this.PossibleCustomStates = customs;

        }
        public bool ChangeStateToCustom(User user, CustomState state)
        {
            if (!user.HasRole(typeof(ProductOwner).ToString()))
                return PossibleCustomStates.Contains(state.Name);
            return false;
        }
        public void ChangeStateCount(string state)
        {
            Counter.DecreaseStateCount(this.GetType().ToString());
            Counter.IncreaseStateCount(state);
        }
    }
}
