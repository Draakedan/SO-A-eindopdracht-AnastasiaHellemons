using avansDevOps.Users;
using System;
using System.Collections.Generic;
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
        public StateCount counter { get; set; }

        public CustomState() { }
        public void AddPossibleCustomState(string state) { }
        public void RemovePossibleCustomState(string state) { }
        public bool IsStatePossible(string state) { return false; }
        public void ChangeStateToToDo() { }
        public void ChangeStateToDoing() { }
        public void ChangeStateToReadyForTesting() { }
        public void ChangeStateToTesting() { }
        public void ChangeStateToTested() { }
        public void ChangeStateToDone() { }
        public bool CanEditRules(User user, int max, int min, bool todo, bool doing, bool readyForTesting, bool testing, bool tested, bool done, List<string> customs) { return false; }
        public void EditRules(int max, int min, bool todo, bool doing, bool readyForTesting, bool testing, bool tested, bool done, List<string> customs) { }
        public void ChangeStateToCustom() { }
        public bool CheckStateCount() { return false; }
    }
}
