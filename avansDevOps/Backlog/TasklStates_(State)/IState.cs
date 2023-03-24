using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog.TasklStates__State_
{
    public interface IState
    {
        int MaxItems { get; }
        int MinItems { get; }
        StateCount counter { get; }

        void ChangeStateToToDo();
        void ChangeStateToDoing();
        void ChangeStateToReadyForTesting();
        void ChangeStateToTesting();
        void ChangeStateToTested();
        void ChangeStateToDone();
        void ChangeStateToCustom();
        bool CheckStateCount();
    }
}
