using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog.DiscussuionForum
{
    public class BackLogDiscussionForum
    {
        private List<DiscussionThread> Threads { get; set; }
        private IState State { get; set; }

        public BackLogDiscussionForum(IState state) { }
        public void UpdateState(IState state) { }
        public void NewDiscussionThread(DiscussionThread thread) { }
        public string ViewDiscussionThreads() { return string.Empty; }
        public void GetDiscussuionThread(string name) { }
        public bool CanAddDiscussionThread(User user, DiscussionThread thread) { return false; }
    }
}
