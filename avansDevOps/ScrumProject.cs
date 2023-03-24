using avansDevOps.Git__Adapter_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps
{
    public class ScrumProject
    {
        private User ProductOwner { get; set; }
        private User ScrumMaster { get; set; }
        private List<User> Developers { get; set; }
        private List<User> Testers { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private GitHandler GitHandler { get; set; }

        public ScrumProject() { }
        public void AddDeveloper(User developer) { }
        public void AddTester(User tester) { }
        public void SetProductOwner(User productOwner) { }
        public void RemoveDeveloper(User developer) { }
        public void RemoveTester(User tester) { }
        public void CreateSprint(Sprint sprint) { }
        public bool CanCreateSprint(User user) { return false; }
        public void RemoveSprint(Sprint sprint) { }
        public string ViewSprints() { return string.Empty; }
        public Sprint GetSprint(string sprintName) { return null; }
    }
}
