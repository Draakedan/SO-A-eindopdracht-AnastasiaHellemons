using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace avansDevOps.Backlog.TasklStates__State_
{
    public class TestingState : IState
    {
        public int MaxItems { get; set; }
        public int MinItems { get; set; }
        public StateCount Counter { get; set; }

        public TestingState(StateCount counter)
        {
            this.Counter = counter;
            this.MinItems = 0;
            this.MaxItems = int.MaxValue;
        }

        public TestingState(StateCount counter, int testers)
        {
            this.Counter = counter;
            this.MinItems = 0;
            this.MaxItems = testers;
        }

        public bool ChangeStateToCustom(User user, CustomState state)
        {
            if (!user.HasRole(typeof(ProductOwner).ToString()))
            {
                int stateCount = Counter.GetStateCount(state.Name);
                return state.MaxItems > stateCount + 1;
            }
            return false;
        }
        public bool ChangeStateToDoing(User user, int items) { return false; }
        public bool ChangeStateToDone(User user) { return false; }
        public bool ChangeStateToReadyForTesting(User user) { return false; }
        public bool ChangeStateToTested(User user)
        {
            return (user.HasRole(typeof(Tester).ToString()));
        }
        public bool ChangeStateToTesting(User user, int items) { return false; }
        public bool ChangeStateToToDo(User user)
        {
            return (!user.HasRole(typeof(ProductOwner).ToString()));
        }
        public void ChangeStateCount(string state)
        {
            Counter.DecreaseStateCount(this.GetType().ToString());
            Counter.IncreaseStateCount(state);
        }
    }
}
