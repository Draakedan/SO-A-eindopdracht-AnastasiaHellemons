using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US27
    {
        private Developer _developer;
        [SetUp]
        public void Setup()
        {
            _developer = new Developer();
        }

        [Test]
        public void TheScrumMasterCanLinkEffortPointsToADeveloper()
        {
            var role = new ScrumMaster();
            var user = new User("","","");
            user.AddRole(role);

            var result = _developer.CanAssingPoints(user, "", 3);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanGainEffortPoint()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            _developer.CanAssingPoints(user, "", 3);
            var result = _developer.ViewPoints("");
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void ATesterCanNotLinkEffortPointsToADeveloper()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _developer.CanAssingPoints(user, "", 3);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotLinkEffortPointsToOtherDevelopers()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _developer.CanAssingPoints(user, "", 3);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotLinkEffortPointsToADeveloper()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _developer.CanAssingPoints(user, "", 3);
            Assert.That(result, Is.False);
        }

        [Test]
        public void TheScrumMasterCanRemoveEffortPointsFromADeveloper()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _developer.CanRemovePoints(user, "", 3);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotRemoveEffortPointsFromADeveloper()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _developer.CanRemovePoints(user, "", 3);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotRemoveEffortPointsFromADeveloper()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _developer.CanRemovePoints(user, "", 3);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotRemoveEffortPointsFromADeveloper()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _developer.CanRemovePoints(user, "", 3);
            Assert.That(result, Is.False);
        }

    }
}
