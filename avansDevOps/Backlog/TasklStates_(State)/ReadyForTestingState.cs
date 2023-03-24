using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog.TasklStates__State_
{
    public class ReadyForTestingState: IState
    {
        public int MaxItems { get; set; }
        public int MinItems { get; set; }
        public StateCount counter { get; set; }

        public void ChangeStateToCustom() { }
        public void ChangeStateToDoing() { }
        public void ChangeStateToDone() { }
        public void ChangeStateToReadyForTesting() { }
        public void ChangeStateToTested() { }
        public void ChangeStateToTesting() { }
        public void ChangeStateToToDo() { }
        public bool CheckStateCount() { return false; }
    }
}
