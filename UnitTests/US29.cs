using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US29
    {
        private LogInInstance _logInInstance;
        [SetUp]
        public void Setup()
        {
            var userRepo = new UserRepo();
            var user = new User("u@er.com", "name", "pass");
            userRepo.Add(user);
            _logInInstance = new LogInInstance(userRepo);
        }

        [Test]
        public void AnUserCanAccessTheSystemWhenThePasswordAndUsernameAreCorrect()
        {
            var result = _logInInstance.LogIn("name", "pass");
            Assert.That(result, Is.True);
        }

        [Test]
        public void AnUserCanNotAccessTheSystemWhenThePasswordAndUsernameAreNotCorrect()
        {
            var result = _logInInstance.LogIn("nam", "pas");
            Assert.That(result, Is.False);
        }

        [Test]
        public void AnUserCanRetryLoggingInWhenThePasswordAndUsernameAreNotCorrect()
        {
            _logInInstance.LogIn("nam", "pas");
            var result = _logInInstance.LogIn("name", "pass");
            Assert.That(result, Is.True);
        }

    }
}
