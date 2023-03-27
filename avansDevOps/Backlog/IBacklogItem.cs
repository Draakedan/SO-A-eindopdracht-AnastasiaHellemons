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
    public interface IBacklogItem
    {
        string Name { get; }
        IState State { get; }
        BackLogDiscussionForum Discussions { get; }
        User Developer { get; }

        void ChangeState(IState state);
        bool CanChangeState(User user, IState state);
        bool AddDeveloper(User developer);
        void RemoveDeveloper();
        bool CanAddUser(User Adder, User developer);
    }
}
