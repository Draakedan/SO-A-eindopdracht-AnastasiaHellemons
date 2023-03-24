using avansDevOps.Repport;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US26
    {
        private Repport _repport;
        [SetUp]
        public void Setup()
        {
            _repport = new Repport();
        }

        [Test]
        public void TheScrumMasterCanGenerateARepport()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _repport.CanGenerate(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotGenerateARepport()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _repport.CanGenerate(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotGenerateARepport()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _repport.CanGenerate(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotGenerateARepport()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _repport.CanGenerate(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ARepportCanBeSavedAsAPDF()
        {
            var result = _repport.Save("PDF");
            Assert.That(result, Is.EqualTo("PDF:"));
        }

        [Test]
        public void ARepportCanBeSavedAsAPNG()
        {
            var result = _repport.Save("PNG");
            Assert.That(result, Is.EqualTo("PNG:"));
        }

        [Test]
        public void ARepportCanBeSavedAsAJPEG()
        {
            var result = _repport.Save("JPEG");
            Assert.That(result, Is.EqualTo("JPEG:"));
        }

        [Test]
        public void ARepportContainsATeamSetup()
        {
            var result = _repport.Save("none").Contains("team setup");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportContainsABurndownChart()
        {
            var result = _repport.Save("none").Contains("burndown chart");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportContainsEffortPointsPerDeveloper()
        {
            var result = _repport.Save("none").Contains("effort points");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportCanContainTheCompanyNameInTheHeader()
        {
            var result = _repport.Save("header").Contains("header: company name");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportCanContainTheCompanyNameInTheFooter()
        {
            var result = _repport.Save("footer").Contains("footer: company name");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportCanContainTheProjectNameInTheHeader()
        {
            var result = _repport.Save("header").Contains("header: project name");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportCanContainTheProjectNameInTheFooter()
        {
            var result = _repport.Save("footer").Contains("footer: project name");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportCanContainTheVersionNumberInTheHeader()
        {
            var result = _repport.Save("header").Contains("header: version number");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportCanContainTheVersionNumberInTheFooter()
        {
            var result = _repport.Save("footer").Contains("footer: version number");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportCanContainTheDateInTheHeader()
        {
            var result = _repport.Save("header").Contains("header: date");
            Assert.That(result, Is.True);
        }

        [Test]
        public void ARepportCanContainTheDateInTheFooter()
        {
            var result = _repport.Save("footer").Contains("footer: date");
            Assert.That(result, Is.True);
        }

    }
}
