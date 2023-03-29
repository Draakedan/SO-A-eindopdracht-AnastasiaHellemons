using avansDevOps;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US28
    {
        private ScrumProject _project;
        [SetUp]
        public void Setup()
        {
            _project = new ScrumProject();
        }

        [Test]
        public void TheScrumMasterCanLinkASprintToAScrumProject()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _project.CanCreateSprint(user);
            Assert.That(result, Is.True);
        }
    }
}
