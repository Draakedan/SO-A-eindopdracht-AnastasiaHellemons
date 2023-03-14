using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Users
{
    public class ScrumMaster : IRole
    {
        public string Name { get; }

        ScrumMaster()
        {
            Name = "Scrum Master";
        }

        public void JoinProject(string name)
        {
            Console.WriteLine($"Joined {name} as {Name}!");
        }

        public void LeaveProject(string name)
        {
            Console.WriteLine($"Left {name} as {Name}!");
        }
    }
}
