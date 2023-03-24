using avansDevOps.Backlog;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps
{
    public class Sprint
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        private List<User> Developers { get; set; }
        private List<User> Testers { get; set; }
        private List<IBacklogItem> BacklogItems { get; set; }
        public string Name { get; private set; }
        private CustomStateRepo CustomStateRepo { get; set; }
        public string SprintLabel { get; private set; }

        public Sprint() { }
        public void AddDeveloper(User developer) { }
        public bool CanAddDeveloper(User user, User developer) { return false; }
        public bool CanRemoveDeveloper(User user, User developer) { return false; }
        public void RemoveDeveloper(User developer) { }
        public void SetSprintLabel(string label) { }
        public void AddTester(User tester) { }
        public bool CanAddTester(User user, User tester) { return false; }
        public bool CanRemoveTester(User user, User tester) { return false; }
        public void RemoveTester(User tester) { }
        public bool AddBacklogItem(IBacklogItem item) { return false; }
        public void RemoveBacklogItem(IBacklogItem item) { }
        public override string ToString() { return string.Empty; }
        public string ViewBacklog() { return string.Empty; }
        public IBacklogItem GetBacklogItem(string itemName) { return null; }
        public bool EditSprint(DateTime startDate, DateTime endDate, string name) { return false; }
        public bool CanEditSprint(User editor) { return false; }
        public bool CanSprintStart() { return false; }
    }
}
