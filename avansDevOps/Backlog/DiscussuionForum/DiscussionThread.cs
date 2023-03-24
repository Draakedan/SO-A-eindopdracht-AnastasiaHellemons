using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog.DiscussuionForum
{
    public class DiscussionThread
    {
        private IState State { get; set; }
        public string Topic { get; private set; }
        private User Poster { get; set; }
        private List<Response> responses { get; set; }

        public DiscussionThread(IState state, string topic, User poster, Response first)
        {
            State = state;
            Topic = topic;
            Poster = poster;
            this.responses = new();
            this.responses.Add(first);
        }

        public void AddResponse(Response response) { }
        public void UpdateState(IState state) { }
        public void EditTopic(string topic) { }
        public void DeleteResponse(Response response) { }
        public bool CanDeleteResponse(User user, Response response) { return false; }
        public bool CanEdit(User user) { return false; }
        public bool CanAddResponse(User user) { return false; }
    }
}
