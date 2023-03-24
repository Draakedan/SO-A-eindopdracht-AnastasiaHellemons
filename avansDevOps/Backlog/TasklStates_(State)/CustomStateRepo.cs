using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps
{
    public class CustomStateRepo
    {
        private List<CustomState> customStates { get; set; }

        public void AddCustomState(CustomState state) { }
        public CustomState GetCustomState() { return null; }
        public string GetCustomStateName(string name) { return string.Empty; }
        public string GetAllCustomStates() { return string.Empty; }
        public bool CanAddCustomState(User user, CustomState customState) { return false; }
    }
}
