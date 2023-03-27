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
        private List<Sprint> Sprints { get; set; }

        public ScrumProject() 
        {
            this.Developers = new List<User>();
            this.Testers = new List<User>();
            this.Sprints = new List<Sprint>();
        }
        public void AddDeveloper(User developer)
        {
            if (developer.HasRole(typeof(Developer).ToString()))
                Developers.Add(developer);
        }

        public void AddTester(User tester)
        {
            if (tester.HasRole(typeof(Tester).ToString()))
                Testers.Add(tester);
        }
        public void SetProductOwner(User productOwner)
        {
            if (productOwner.HasRole(typeof(ProductOwner).ToString()))
                this.ProductOwner = productOwner;
        }
        public void RemoveDeveloper(User developer)
        {
            if (developer.HasRole(typeof(Developer).ToString()))
                this.Developers.Remove(developer);
        }
        public void RemoveTester(User tester) 
        {
            if (tester.HasRole(typeof(Tester).ToString()))
                Testers.Remove(tester);
        }
        public void CreateSprint(Sprint sprint) 
        {
            this.Sprints.Add(sprint);
        }
        public bool CanCreateSprint(User user)
        {
            return user.HasRole(typeof(ScrumMaster).ToString());
        }
        public void RemoveSprint(Sprint sprint) 
        {
            this.Sprints.Remove(sprint);
        }
        public string ViewSprints() {
            string s = "";
            this.Sprints.ForEach(sprint => s += sprint.Name + ", ");
            return s; 
        }
        public Sprint? GetSprint(string sprintName) 
        {
            Sprint s = null;
            foreach (Sprint sprint in this.Sprints)
            {
                if (sprint.Name.Equals(sprintName))
                    s = sprint;
            }
            return s;
        }
    }
}
