﻿using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.NotificationService__Observer_;
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

        public User Poster { get; init; }
        public bool FirstPost { get; init; }

        public Response(IState state, string content, User poster, bool firstPost)
        {
            State = state;
            Content = content;
            Poster = poster;
            FirstPost = firstPost;
        }
        public void ChangeState(IState state)
        {
            this.State = state;
        }
        public void Edit(string newContent) 
        {
            this.Content = newContent;
        }

        public bool CanEdit(User user, string newContent) 
        {

            bool canEdit = user.Name.Equals(this.Poster.Name) && State.GetType() != typeof(DoneState);
            if (canEdit) Edit(newContent);
            return canEdit; 
        }
    }
}
