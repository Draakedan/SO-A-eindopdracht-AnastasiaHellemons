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
        public User Poster { get; init; }
        private List<Response> Responses { get; set; }

        public DiscussionThread(IState state, string topic, User poster, Response first)
        {
            State = state;
            Topic = topic;
            Poster = poster;
            this.Responses = new()
            {
                first
            };
        }

        public void AddResponse(Response response)
        {
            Responses.Add(response);
        }

        public void UpdateState(IState state)
        {
            this.State = state;
        }

        public void EditTopic(string topic)
        {
            this.Topic = topic;
        }

        public void DeleteResponse(Response response)
        {
            Responses.Remove(response);
        }
        public bool CanDeleteResponse(User user, Response response)
        {
            Console.WriteLine(user.Name.Equals(response.Poster.Name));
            Console.WriteLine(State.GetType() != typeof(DoneState));
            Console.WriteLine(!response.FirstPost);
            bool canDel = (user.Name.Equals(this.Poster.Name) && State.GetType() != typeof(DoneState) && !response.FirstPost);
            if (canDel)
                DeleteResponse(response);
            return canDel;
        }
        public bool CanEdit(User user) 
        {
            return user.Name.Equals(this.Poster.Name) && State.GetType() != typeof(DoneState);
        }
        public bool CanAddResponse(User user) 
        {
            return !user.HasRole(typeof(ProductOwner).ToString()) && State.GetType() != typeof(DoneState);
        }
    }
}
