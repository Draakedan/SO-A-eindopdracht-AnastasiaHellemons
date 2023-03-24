using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog.DiscussuionForum
{
    public class Response
    {
        private IState State { get; set; }
        private string Content { get; set; }
        private User Poster { get; set; }
        private bool FirstPost { get; set; }

        public Response(IState state, string content, User poster, bool firstPost)
        {
            State = state;
            Content = content;
            Poster = poster;
            FirstPost = firstPost;
        }
        public void ChangeState(IState state) { }
        public void Edit(string newContente) { }
        public void EditState(IState state) { }
        public void SendMessage(string message) { }
        public bool CanEdit(User user, string newContent) { return false; }
    }
}
