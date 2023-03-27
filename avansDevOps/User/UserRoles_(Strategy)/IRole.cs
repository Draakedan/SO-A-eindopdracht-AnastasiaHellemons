using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Users
{
    public interface IRole
    {
        string Name { get; }

        public void JoinProject(string name);
        public void LeaveProject(string name);
    }
}
