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
    public class SubTask : IBacklogItem
    {
        public string Name { get; set; }
        public IState State { get; set; }
        public BackLogDiscussionForum Discussions { get; set; }
        public User Developer { get; set; }

        public SubTask(String name) 
        { 
            Name = name;
            State = new ToDoState();
            Discussions = new();
            this.Developer = new User("", "", "");
        }
        public bool AddDeveloper(User developer) { return false; }
        public void ChangeState(IState state) { }
        public void RemoveDeveloper() { }
        public bool CanAddUser(User Adder, User developer) { return false; }
        public bool CanChangeState(User user, IState state) { return false; }
    }
}
