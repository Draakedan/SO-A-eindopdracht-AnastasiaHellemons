using avansDevOps;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US14
    {
        private CustomStateRepo _customStateRepo;
        private CustomState _customState;
        [SetUp]
        public void Setup()
        {
            _customState = new CustomState(new StateCount(), "name");
            _customStateRepo = new CustomStateRepo();
        }

        [Test]
        public void TheScrumMasterCanMakeACustomState()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _customStateRepo.CanAddCustomState(user, _customState);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotMakeACustomState()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _customStateRepo.CanAddCustomState(user, _customState);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotMakeACustomState()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _customStateRepo.CanAddCustomState(user, _customState);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotMakeACustomState()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _customStateRepo.CanAddCustomState(user, _customState);
            Assert.That(result, Is.False);
        }

        [Test]
        public void TheScrumMasterCanAddCustomRulesToACustomState()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _customState.CanEditRules(user, 0 , 0, false, false, false, false, false, false, new List<string>());
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotAddCustomRulesToACustomState()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _customState.CanEditRules(user, 0, 0, false, false, false, false, false, false, new List<string>());
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotAddCustomRulesToACustomState()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _customState.CanEditRules(user, 0, 0, false, false, false, false, false, false, new List<string>());
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotAddCustomRulesToACustomState()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _customState.CanEditRules(user, 0, 0, false, false, false, false, false, false, new List<string>());
            Assert.That(result, Is.False);
        }

    }
}
