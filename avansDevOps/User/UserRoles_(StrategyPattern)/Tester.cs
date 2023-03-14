using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Users
{
    public class Tester : IRole
    {
        public string Name { get; }

        Tester()
        {
            Name = "Tester";
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
