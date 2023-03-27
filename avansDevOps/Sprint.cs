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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; private set; }
        private List<User> Developers { get; set; }
        private List<User> Testers { get; set; }
        private List<IBacklogItem> BacklogItems { get; set; }
        public string Name { get; private set; }
        private CustomStateRepo CustomStateRepo { get; set; }
        public string SprintLabel { get; private set; }

        public Sprint()
        {
            Developers = new();
            Testers = new();
            BacklogItems = new();
            CustomStateRepo = new CustomStateRepo();
            this.StartDate = DateTime.Now.AddYears(2023);
        }
        public void AddDeveloper(User developer)
        {
            this.Developers.Add(developer);
        }

        public bool CanAddDeveloper(User user, User developer)
        {
            bool canAdd = developer.HasRole(typeof(Developer).ToString()) && this.StartDate > DateTime.Now && user.HasRole(typeof(ScrumMaster).ToString());
            if (canAdd)
                AddDeveloper(developer);
            return canAdd;
        }
        public bool CanRemoveDeveloper(User user, User developer)
        {
            bool canRemove = developer.HasRole(typeof(Developer).ToString()) && this.StartDate > DateTime.Now && user.HasRole(typeof(ScrumMaster).ToString());
            if (canRemove)
                RemoveDeveloper(developer);
            return canRemove;
        }
        public void RemoveDeveloper(User developer)
        {
            this.Developers.Remove(developer);
        }
        public void SetSprintLabel(string label)
        {
            this.SprintLabel = label;
        }

        public void AddTester(User tester)
        {
            this.Testers.Add(tester);
        }

        public bool CanAddTester(User user, User tester)
        {
            bool canAdd = tester.HasRole(typeof(Tester).ToString()) && this.StartDate > DateTime.Now && user.HasRole(typeof(ScrumMaster).ToString());
            if (canAdd)
                AddTester(tester);
            return canAdd;
        }
        public bool CanRemoveTester(User user, User tester)
        {
            bool canRemove = tester.HasRole(typeof(Tester).ToString()) && this.StartDate > DateTime.Now && user.HasRole(typeof(ScrumMaster).ToString());
            if (canRemove)
                RemoveTester(tester);
            return canRemove;
        }
        public void RemoveTester(User tester)
        {
            this.Testers.Remove(tester);
        }
        public bool AddBacklogItem(IBacklogItem item)
        {
            bool canEdit = this.StartDate > DateTime.Now && item.Name != string.Empty;
            if (canEdit)
                this.BacklogItems.Add(item);
            return canEdit;
        }
        
        public void RemoveBacklogItem(IBacklogItem item)
        {
            this.BacklogItems.Remove(item);
        }
        private void SortBacklog()
        {
            List<BacklogItem> backlogItems = new();
            List<SubTask> subTasks = new();
            foreach (IBacklogItem item in this.BacklogItems)
            {
                if (item.GetType() == typeof(BacklogItem))
                    backlogItems.Add((BacklogItem)item);
                else
                    subTasks.Add((SubTask)item);
            }
            this.BacklogItems.Clear();
            backlogItems.Sort();
            backlogItems.ForEach(item => this.BacklogItems.Add(item));
            subTasks.ForEach(item => this.BacklogItems.Add(item));

        }

        public string ViewBacklog()
        {
            SortBacklog();
            string s = "";
            foreach (IBacklogItem item in this.BacklogItems)
                s += item.Name + ", ";
            return s;
        }
        public IBacklogItem GetBacklogItem(string itemName)
        {
            IBacklogItem item = null;
            foreach (IBacklogItem i in BacklogItems)
                if (i.Name.Equals(itemName))
                    item = i;
            return item;
        }
        public bool EditSprint(User user, DateTime startDate, DateTime endDate, string name)
        {
            Console.WriteLine(user.HasRole(typeof(ScrumMaster).ToString()));

            bool canEdit = this.StartDate > DateTime.Now && user.HasRole(typeof(ScrumMaster).ToString());
            Console.WriteLine(canEdit);
            if (canEdit)
            {
                if (startDate > endDate)
                    return false;
                if (startDate.ToShortDateString() == endDate.ToShortDateString())
                    return false;
                if (startDate <= DateTime.Now)
                    return false;
                this.StartDate = startDate;
                if (endDate <= DateTime.Now)
                    return false;
                this.EndDate = endDate;
                this.Name = name;
            }
            return canEdit;
        }
        public bool CanEditSprint(User editor)
        {
            return !editor.HasRole(typeof(ProductOwner).ToString()) && this.StartDate > DateTime.Now;
        }
        public bool CanSprintStart()
        {
            int devs = this.Developers.Count();
            Console.WriteLine("devs: " + devs);
            int testers = this.Testers.Count();
            Console.WriteLine("testers: " + testers);
            int items = this.BacklogItems.Count();
            Console.WriteLine("items: " + items);
            bool canStart = devs > 0 && testers > 0 && items > 0 && items >= devs;
            return canStart;
        }
    }
}
