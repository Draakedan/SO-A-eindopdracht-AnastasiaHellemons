using avansDevOps;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US20
    {
        private SprintReview _review;
        [SetUp]
        public void Setup()
        {
            _review = new SprintReview();
        }

        [Test]
        public void TheScrumMasterCanUploadASprintSummary()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _review.CanUploadSummary(user, "");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotUploadASprintSummary()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _review.CanUploadSummary(user, "");
            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotUploadASprintSummary()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _review.CanUploadSummary(user, "");
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotUploadASprintSummary()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _review.CanUploadSummary(user, "");
            Assert.That(result, Is.False);
        }

    }
}
