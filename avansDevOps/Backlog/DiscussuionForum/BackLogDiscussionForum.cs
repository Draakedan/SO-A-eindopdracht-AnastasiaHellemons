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

        public BackLogDiscussionForum(IState state)
        {
            this.State = state;
            Threads = new();
        }
        public void UpdateState(IState state)
        {
            this.State = state;
        }
        public void NewDiscussionThread(DiscussionThread thread)
        {
            Threads.Add(thread);
        }
        public string ViewDiscussionThreads()
        {
            string s = "";
            foreach (DiscussionThread thread in Threads)
                s += thread.Topic + "\n";
            return s;
        }
        public DiscussionThread? GetDiscussuionThread(string name)
        {
            DiscussionThread thread = null;
            foreach (DiscussionThread discussion in Threads)
            {
                if (discussion.Topic.Equals(name))
                    thread = discussion;
            }
            return thread;
        }
        public bool CanAddDiscussionThread(User user, DiscussionThread thread) 
        {
            Console.WriteLine(State.GetType() != typeof(DoneState));
            bool canAdd = (!user.HasRole(typeof(ProductOwner).ToString()) && State.GetType() != typeof(DoneState));
            if (canAdd)
                NewDiscussionThread(thread);
            return canAdd; 
        }
    }
}
