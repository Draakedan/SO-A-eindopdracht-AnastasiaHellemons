using avansDevOps.Backlog.DiscussuionForum;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog
{
    public class BacklogItem : IBacklogItem
    {
        private int PriorityNo { get; set; }
        public string Name { get; set; }
        public IState State { get; set; }
        public BackLogDiscussionForum Discussions { get; set; }
        private List<SubTask> subTasks { get; set; }

        public User Developer { get; set; }

        public BacklogItem(int priorityNo, string name)
        {
            PriorityNo = priorityNo;
            Name = name;
            this.State = new ToDoState();
            this.Discussions = new(this.State);
            this.subTasks = new();
            this.Developer = new User("", "", "");
        }
        public bool CanAddSubtasks(User user) { return false; }
        public string GetSubtasks() { return string.Empty; }
        public void AddSubtask(SubTask name) { }
        public void RemoveSubtask(SubTask name) { }
        public void EditPriority(int priorityNo) { }
        public void ChangeState(IState state) { }
        public bool AddDeveloper(User developer) { return false; }
        public void RemoveDeveloper() { }
        public bool CanAddUser(User Adder, User developer) { return false; }
        public bool CanChangeState(User user, IState state) { return false; }
    }
}
