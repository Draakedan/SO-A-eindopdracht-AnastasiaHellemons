using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog.TasklStates__State_
{
    public class StateCount
    {
        private readonly Dictionary<string, int> ItemsPerState;

        public StateCount()
        {
            ItemsPerState = new()
            {
                { typeof(ToDoState).ToString(), 0 },
                { typeof(DoingState).ToString(), 0 },
                { typeof(ReadyForTestingState).ToString(), 0 },
                { typeof(TestingState).ToString(), 0 },
                { typeof(TestedState).ToString(), 0 },
                { typeof(DoneState).ToString(), 0 },
                { typeof(CustomState).ToString(), 0 }
            };
        }

        public void IncreaseStateCount(string state)
        {
            ItemsPerState[state] += 1;
        }
        public void DecreaseStateCount(string state)
        {
            ItemsPerState[state] -= 1;
        }
        public int GetStateCount(string state)
        {
            return ItemsPerState[state];
        }
    }
}
