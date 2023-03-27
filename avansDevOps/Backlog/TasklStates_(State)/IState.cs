using avansDevOps.Users;
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
        StateCount Counter { get; }

        bool ChangeStateToToDo(User user);
        bool ChangeStateToDoing(User user, int items);
        bool ChangeStateToReadyForTesting(User user);
        bool ChangeStateToTesting(User user, int items);
        bool ChangeStateToTested(User user);
        bool ChangeStateToDone(User user);
        bool ChangeStateToCustom(User user, CustomState state);
        void ChangeStateCount(string state);
    }
}
