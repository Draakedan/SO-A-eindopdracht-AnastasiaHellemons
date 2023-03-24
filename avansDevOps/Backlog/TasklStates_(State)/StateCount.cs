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

        public void IncreaseStateCount(string state) { }
        public void DecreaseStateCount(string state) { }
        public int GetStateCount(string state) { return -1; }
        private void LoadStateCount() { }
        private void SaveStateCount() { }
    }
}
