using avansDevOps.DevelopmentPipeline__facade_;
using avansDevOps.Users;

namespace UnitTests
{
    public class US17
    {
        private DevelopmentPipeline _pipeline;
        [SetUp]
        public void Setup()
        {
            _pipeline = new DevelopmentPipeline();
        }

        [Test]
        public void TheScrumMasterCanManuallyStartADeploymentPipeline()
        {
            var role = new ScrumMaster();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _pipeline.CanStartPipeline(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ADeveloperCanNotStartADevelopmentPipeline()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _pipeline.CanStartPipeline(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ATesterCanNotStartADevelopmentPipeline()
        {
            var role = new Tester();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _pipeline.CanStartPipeline(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AProductOwnerCanNotStartADevelopmentPipeline()
        {
            var role = new ProductOwner();
            var user = new User("", "", "");
            user.AddRole(role);

            var result = _pipeline.CanStartPipeline(user);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ADevelopmentPipelineHasAPackageInstallationPhase()
        {
            var instructions = "pass skip skip skip";
            _pipeline.SetUpPipeline(instructions);

            var result = _pipeline.StartPipeline();

            Assert.That(result, Is.EqualTo("installation: pass"));
        }

        [Test]
        public void ADevelopmentPipelineHasABuildingPhase()
        {
            var instructions = "skip pass skip skip";
            _pipeline.SetUpPipeline(instructions);

            var result = _pipeline.StartPipeline();

            Assert.That(result, Is.EqualTo("building: pass"));
        }

        [Test]
        public void ADevelopmentPipelineHasATestingPhase()
        {
            var instructions = "skip skip pass skip";
            _pipeline.SetUpPipeline(instructions);

            var result = _pipeline.StartPipeline();

            Assert.That(result, Is.EqualTo("testing: pass"));
        }

        [Test]
        public void ADevelopmentPipelineHasADeplomentPhase()
        {
            var instructions = "skip skip skip pass";
            _pipeline.SetUpPipeline(instructions);

            var result = _pipeline.StartPipeline();

            Assert.That(result, Is.EqualTo("deploy: pass"));
        }

        [Test]
        public void TheScrumMasterCanRetryThePipelineWhenThePipelineFails()
        {
            var role = new Developer();
            var user = new User("", "", "");
            user.AddRole(role);

            var instructions = "fail skip skip skip";
            _pipeline.SetUpPipeline(instructions);

            _pipeline.StartPipeline();
            var result = _pipeline.CanStartPipeline(user);

            Assert.That(result, Is.True);
        }

    }
}
