using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Users
{
    public class User
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        List<IRole> Roles { get; set; }
        List<string> Projects { get; set; }

        public User(string email, string name, string password)
        {
            Email = email;
            Name = name;
            Password = password;

            Roles = new();
            Projects = new();

        }
        public void AddRole(IRole role)
        {
            Roles.Add(role);
        }
        public void RemoveRole(IRole role)
        {
            Roles.Remove(role);
        }
        public void JoinProject(string project)
        {
            Projects.Add(project);
            foreach (IRole role in Roles)
            {
                role.JoinProject(project);
            }
        }
        public void LeaveProject(string project)
        {
            Projects.Remove(project);
            foreach (IRole role in Roles)
            {
                role.LeaveProject(project);
            }
        }
    }
}
