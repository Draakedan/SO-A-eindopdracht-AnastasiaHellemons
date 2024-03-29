﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Users
{
    public class UserRepo
    {
        private readonly List<User> Users;
        public UserRepo()
        {
            Users = new();
        }

        public void Add(User user)
        {
            if (UserIsUnique(user.Name))
                Users.Add(user);
        }
        public User? GetUser(string Name)
        {
            foreach (User user in Users)
                if (user.Name.Equals(Name)) return user;
            return null;
        }
        public bool UserIsUnique(string Name)
        {
            bool unique = true;
            foreach (User user in Users)
                if (user.Name.Equals(Name))
                    unique = false;
            return unique;
        }
    }
}
