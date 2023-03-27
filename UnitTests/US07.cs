using avansDevOps;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US07
    {
        private Sprint _sprint;

        [SetUp]
        public void Setup()
        {
            _sprint = new();
        }

        [Test]
        public void TheStartDateOfASprintCanBeEdited()
        {
            var user = new User("", "", "");
            var role = new ScrumMaster();
            user.AddRole(role);

            _sprint.EditSprint(user, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "testSprint");
            var result = _sprint.StartDate.ToShortDateString();
            Assert.That(result, Is.EqualTo(DateTime.Now.AddDays(1).ToShortDateString()));
        }

        [Test]
        public void TheNameOfASprintCanBeEdited()
        {
            var user = new User("", "", "");
            var role = new ScrumMaster();
            user.AddRole(role);

            _sprint.EditSprint(user, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "testSprint");
            var result = _sprint.Name;
            Assert.That(result, Is.EqualTo("testSprint"));
        }

        [Test]
        public void TheEndDateOfASprintCanBeEdited()
        {
            var user = new User("", "", "");
            var role = new ScrumMaster();
            user.AddRole(role);

            _sprint.EditSprint(user, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "testSprint");
            var result = _sprint.EndDate.ToShortDateString();
            Assert.That(result, Is.EqualTo(DateTime.Now.AddDays(2).ToShortDateString()));
        }

        [Test]
        public void TheScrumMasterCanEditSprints()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _sprint.EditSprint(user, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "testSprint");

            Assert.That(result, Is.True);

        }

        [Test]
        public void ADeveloperCanNotEditSprints()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _sprint.EditSprint(user, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "testSprint");

            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotEditSprints()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _sprint.EditSprint(user, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "testSprint");

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotEditSprints()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _sprint.EditSprint(user, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "testSprint");

            Assert.That(result, Is.False);
        }

    }
}
