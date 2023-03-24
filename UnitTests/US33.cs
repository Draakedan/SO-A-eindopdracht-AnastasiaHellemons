using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US33
    {
        private UserRepo _userRepo;
        private User _user;
        [SetUp]
        public void Setup()
        {
            _userRepo = new UserRepo();
            _user = new User("u@er.com", "name", "pass");
            _userRepo.Add(_user);
        }

        [Test]
        public void AnUserHasAUsername()
        {
            var result = _user.Name;
            Assert.That(result, Is.EqualTo("name"));
        }

        [Test]
        public void AnUserHasAEmail()
        {
            var result = _user.Email;
            Assert.That(result, Is.EqualTo("u@er.com"));
        }

        [Test]
        public void AnUserHasAPassword()
        {
            var result = _user.Password;
            Assert.That(result, Is.EqualTo("pass"));
        }

        [Test]
        public void AnUsernameMustBeUnique()
        {
            var uniqueUser = new User("a@z.com", "unique", "pass");
            var result = _userRepo.UserIsUnique(uniqueUser.Name);
            Assert.That(result, Is.True);
        }

        [Test]
        public void AnUserGetsAnErrorMessageWhenTheUsernameIsUsed()
        {
            var notUniqueUser = new User("a@z.com", "name", "pass");
            var result = _userRepo.UserIsUnique(notUniqueUser.Name);
            Assert.That(result, Is.True);
        }
    }
}