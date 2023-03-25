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
        private List<CustomState> CustomStates { get; set; }

        public CustomStateRepo()
        {
            CustomStates = new();
        }

        public void AddCustomState(CustomState state)
        {
            CustomStates.Add(state);
        }

        public CustomState? GetCustomState(string name)
        {
            CustomState? state = null;
            foreach (CustomState customState in CustomStates)
                if (customState.Name == name)
                    state = customState;
            return state;
        }
        public string GetAllCustomStates()
        {
            string names = "";
            foreach (CustomState customState in CustomStates)
                names += customState.Name + "\n";
            return names;
        }
        public bool CanAddCustomState(User user, CustomState customState)
        {
            bool canAdd = (user.HasRole(typeof(ScrumMaster).ToString()));
            if (canAdd)
                AddCustomState(customState);
            return canAdd;
        }
    }
}
