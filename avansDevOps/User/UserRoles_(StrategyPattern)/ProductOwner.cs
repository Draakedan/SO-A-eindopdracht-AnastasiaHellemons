using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Users
{
    public class ProductOwner : IRole
    {
        public string Name { get; }

        ProductOwner()
        {
            Name = "Product Owner";
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
