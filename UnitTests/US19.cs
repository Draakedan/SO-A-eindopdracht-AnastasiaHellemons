using avansDevOps;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US19
    {
        private SprintReview _review;
        [SetUp]
        public void Setup()
        {
            _review = new SprintReview();
        }

        [Test]
        public void TheScrumMasterCanInitiateASprintReview()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            _review.UploadSummary("");
            var result = _review.CanStartReview(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotInitiateASprintReview()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            _review.UploadSummary("");
            var result = _review.CanStartReview(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotInitiateASprintReview()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            _review.UploadSummary("");
            var result = _review.CanStartReview(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotInitiateASprintReview()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            _review.UploadSummary("");
            var result = _review.CanStartReview(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ASprintReviewCanNotBeInitatedWhenNoSprintSummayHasBeenUploaded()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _review.CanStartReview(user);
            Assert.That(result, Is.False);
        }

    }
}
