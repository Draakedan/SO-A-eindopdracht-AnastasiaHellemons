using avansDevOps.DevelopmentPipeline__facade_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US18
    {
        private DevelopmentPipeline _pipeline;
        [SetUp]
        public void Setup()
        {
            _pipeline = new DevelopmentPipeline();
        }

        [Test]
        public void TheScrumMasterCanCancelARelease()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _pipeline.CanCancelPipeline(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotCancelARelease()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _pipeline.CanCancelPipeline(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotCancelARelease()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _pipeline.CanCancelPipeline(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanCancelARelease()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _pipeline.CanCancelPipeline(user);
            Assert.That(result, Is.False);
        }

    }
}
