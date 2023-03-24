using avansDevOps;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US06
    {
        private Sprint _sprint;
        private ScrumProject _project;
        [SetUp]
        public void Setup()
        {
            _sprint = new();
            _project = new ScrumProject();
        }

        [Test]
        public void ASprintHasAStartDate()
        {
            var result = _sprint.StartDate;
            Assert.That(result, Is.Not.NaN);
        }

        [Test]
        public void ASprintHasAnEndDate()
        {
            var result = _sprint.EndDate;
            Assert.That(result, Is.Not.NaN);
        }

        [Test]
        public void ASprintHasAName()
        {
            var result = _sprint.Name;
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void ADeveloperCanNotCreateSprints()
        {
            var developer = new Developer();
            var user = new User("", "", "");
            user.AddRole(developer);

            var result = _project.CanCreateSprint(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotCreateSprints()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);

            var result = _project.CanCreateSprint(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AScrumMasterCanCreateSprints()
        {
            var scrumMaster = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(scrumMaster);

            var result = _project.CanCreateSprint(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void AProductOwnerCanNotCreateSprints()
        {
            var productOwner = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(productOwner);

            var result = _project.CanCreateSprint(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void TheStartDateCanNotBeEarlierThanToday()
        {
            var startDate = new DateTime().AddDays(-1);
            var endDate = new DateTime().AddDays(100);
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheStartDateCanNotBeAfterTheEndDate()
        {
            var startDate = new DateTime().AddDays(101);
            var endDate = new DateTime().AddDays(100);
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheEndDateCanNotBeBeforeToday()
        {
            var startDate = new DateTime().AddDays(-100);
            var endDate = new DateTime().AddDays(-1);
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheStartDateCanNotBeToday()
        {
            var startDate = new DateTime();
            var endDate = new DateTime().AddDays(100);
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheStartDateCanNotBeOnTheSameDayAsTheEndDate()
        {
            var startDate = new DateTime().AddDays(1);
            var endDate = new DateTime().AddDays(1);
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheEndDateCanNotBeToday()
        {
            var startDate = new DateTime().AddDays(10);
            var endDate = new DateTime();
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheStartDateCanNotBeAnInvalidDate()
        {
            var startDate = new DateTime(2025, 2, 30);
            var endDate = new DateTime().AddDays(100);
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheEndDateCanNotBeAnInvalidDate()
        {
            var startDate = new DateTime();
            var endDate = new DateTime(2025, 2, 30);
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheEndDateCanBeAValidDateAfterTheStartDateAndAfterToday()
        {
            var startDate = new DateTime(1);
            var endDate = new DateTime(11);
            var result = _sprint.EditSprint(startDate, endDate, "");

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheScrumMasterCanLinkDevelopersToASprint()
        {
            var scrumMaster = new ScrumMaster();
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(scrumMaster);
            user2.AddRole(developer);

            var result = _sprint.CanAddDeveloper(user1, user2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void TheScrumMasterCanLinkTestersToASprint()
        {
            var scrumMaster = new ScrumMaster();
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(scrumMaster);
            user2.AddRole(tester);

            var result = _sprint.CanAddTester(user1, user2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotLinkDevelopersToASprint()
        {
            var tester = new Tester();
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(tester);
            user2.AddRole(developer);

            var result = _sprint.CanAddDeveloper(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotLinkTestersToASprint()
        {
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(tester);
            user2.AddRole(tester);

            var result = _sprint.CanAddTester(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotLinkDevelopersToASprint()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(developer);

            var result = _sprint.CanAddDeveloper(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotLinkTestersToASprint()
        {
            var tester = new Tester();
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(tester);

            var result = _sprint.CanAddTester(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotLinkDevelopersToASprint()
        {
            var productOwner = new ProductOwner();
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(productOwner);
            user2.AddRole(developer);

            var result = _sprint.CanAddDeveloper(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotLinkTestersToASprint()
        {
            var productOwner = new ProductOwner();
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(productOwner);
            user2.AddRole(tester);

            var result = _sprint.CanAddTester(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void TheScrumMasterCanRemoveDevelopersFromASprint()
        {
            var scrumMaster = new ScrumMaster();
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(scrumMaster);
            user2.AddRole(developer);

            var result = _sprint.CanRemoveDeveloper(user1, user2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void TheScrumMasterCanRemoveTestersFromASprint()
        {
            var scrumMaster = new ScrumMaster();
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(scrumMaster);
            user2.AddRole(tester);

            var result = _sprint.CanRemoveTester(user1, user2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ATesterCanNotRemoveDevelopersFromASprint()
        {
            var tester = new Tester();
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(tester);
            user2.AddRole(developer);

            var result = _sprint.CanRemoveDeveloper(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotRemoveTestersFromASprint()
        {
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(tester);
            user2.AddRole(tester);

            var result = _sprint.CanRemoveTester(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotRemoveDevelopersFromASprint()
        {
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(developer);

            var result = _sprint.CanRemoveDeveloper(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ADeveloperCanNotRemoveTestersFromASprint()
        {
            var developer = new Developer();
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(developer);
            user2.AddRole(tester);

            var result = _sprint.CanRemoveTester(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotRemoveDevelopersFromASprint()
        {
            var productOwner = new ProductOwner();
            var developer = new Developer();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(productOwner);
            user2.AddRole(developer);

            var result = _sprint.CanRemoveDeveloper(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotRemoveTestersFromASprint()
        {
            var productOwner = new ProductOwner();
            var tester = new Tester();
            var user1 = new User("", "", "");
            var user2 = new User("", "", "");
            user1.AddRole(productOwner);
            user2.AddRole(tester);

            var result = _sprint.CanRemoveTester(user1, user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ASprintCanNotStartWithoutDevelopers()
        {
            var tester = new Tester();
            var user = new User("", "", "");
            user.AddRole(tester);
            _sprint.AddTester(user);

            var result = _sprint.CanSprintStart();

            Assert.That(result, Is.False);
        }

        [Test]
        public void ASprintCanStartWithOneDeveloper()
        {
            var tester = new Tester();
            var developer = new Developer();
            var dev1 = new User("", "", "");
            var user = new User("", "", "");
            dev1.AddRole(developer);
            user.AddRole(tester);

            _sprint.AddTester(user);
            _sprint.AddDeveloper(dev1);

            var result = _sprint.CanSprintStart();

            Assert.That(result, Is.True);
        }

        [Test]
        public void ASprintCanStartWithMultipleDevelopers()
        {
            var tester = new Tester();
            var developer = new Developer();

            var dev1 = new User("", "", "");
            var dev2 = new User("", "", "");
            var user = new User("", "", "");

            dev1.AddRole(developer);
            dev2.AddRole(developer);
            user.AddRole(tester);

            _sprint.AddTester(user);
            _sprint.AddDeveloper(dev1);
            _sprint.AddDeveloper(dev2);

            var result = _sprint.CanSprintStart();

            Assert.That(result, Is.True);
        }

        [Test]
        public void ASprintCanStartWithoutTesters()
        {
            var developer = new Developer();
            var user = new User("", "", "");
            user.AddRole(developer);

            _sprint.AddDeveloper(user);

            var result = _sprint.CanSprintStart();

            Assert.That(result, Is.False);
        }

        [Test]
        public void ASprintCanStartWithOneTester()
        {
            var tester = new Tester();
            var developer = new Developer();

            var test1 = new User("", "", "");
            var user = new User("", "", "");

            test1.AddRole(tester);
            user.AddRole(developer);

            _sprint.AddDeveloper(user);
            _sprint.AddTester(test1);

            var result = _sprint.CanSprintStart();

            Assert.That(result, Is.True);
        }

        [Test]
        public void ASprintCanStartWithMultipleTesters()
        {
            var tester = new Tester();
            var developer = new Developer();

            var test1 = new User("", "", "");
            var test2 = new User("", "", "");
            var user = new User("", "", "");

            test1.AddRole(tester);
            test2.AddRole(tester);
            user.AddRole(developer);

            _sprint.AddDeveloper(user);
            _sprint.AddTester(test1);
            _sprint.AddTester(test2);

            var result = _sprint.CanSprintStart();

            Assert.That(result, Is.True);
        }

        [Test]
        public void ASprintCanBeLabledAReviewSprint()
        {
            var before = _sprint.SprintLabel;

            _sprint.SetSprintLabel("review");

            var after = _sprint.SprintLabel;

            Assert.That(after, Is.Not.Null);
            Assert.That(after, Is.Not.EqualTo(before));
            Assert.That(after, Is.EqualTo("review"));
        }

        [Test]
        public void ASprintCanBeLabledADeploymentSprint()
        {
            var before = _sprint.SprintLabel;

            _sprint.SetSprintLabel("deployment");

            var after = _sprint.SprintLabel;

            Assert.That(after, Is.Not.Null);
            Assert.That(after, Is.Not.EqualTo(before));
            Assert.That(after, Is.EqualTo("deployment"));
        }
    }
}
