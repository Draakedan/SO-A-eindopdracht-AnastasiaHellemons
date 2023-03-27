using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog.TasklStates__State_
{
    public class ToDoState : IState
    {
        public int MaxItems { get; set; }
        public int MinItems { get; set; }
        public StateCount Counter { get; set; }

        public ToDoState(StateCount counter) 
        {
            this.Counter = counter;
            this.MinItems = 0;
            this.MaxItems = int.MaxValue;
        }
        public bool ChangeStateToCustom(User user, CustomState state)
        {
            if (!user.HasRole(typeof(ProductOwner).ToString()))
            {
                int stateCount = Counter.GetStateCount(state.Name);
                return state.MaxItems < stateCount + 1 || state.MinItems > stateCount - 1;
            }
            return false;
        }
        public bool ChangeStateToDoing(User user, int items) 
        {
            DoingState doing = new(Counter, items);
            if (user.HasRole(typeof(Developer).ToString()))
            {
                int stateCount = Counter.GetStateCount(doing.GetType().ToString());
                Console.WriteLine(stateCount + 1);
                Console.WriteLine(MaxItems);
                Console.WriteLine(doing.MaxItems > stateCount + 1);
                return doing.MaxItems > stateCount + 1;
            }
            return false;

        }
        public bool ChangeStateToDone(User user) { return false; }
        public bool ChangeStateToReadyForTesting(User user) { return false; }
        public bool ChangeStateToTested(User user) { return false; }
        public bool ChangeStateToTesting(User user, int items) { return false; }
        public bool ChangeStateToToDo(User user) { return false; }
        public void ChangeStateCount(string state)
        {
            Counter.DecreaseStateCount(this.GetType().ToString());
            Counter.IncreaseStateCount(state);
        }
    }
}
