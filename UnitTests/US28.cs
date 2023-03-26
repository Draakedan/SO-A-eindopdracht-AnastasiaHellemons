using avansDevOps.Git__Adapter_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class US28
    {
        private GitHandler _git;
        [SetUp]
        public void Setup()
        {
            _git = new GitHandler();
        }

        [Test]
        public void TheApplicationCanCreateAGitRepo()
        {
            var result = _git.CreateRepo("test");
            Assert.That(result, Is.EqualTo("New repo: test"));
        }

        [Test]
        public void TheApplicationCanPushCode()
        {
            var result = _git.PushCode();
            Assert.That(result, Is.EqualTo("Pushed Code!"));
        }

        [Test]
        public void TheApplicationCanPullCode()
        {
            var result = _git.PullCode();
            var expect = "Pulled Code!";
            var res2 = result.Equals(expect);
            Assert.That(res2, Is.True);
        }

        [Test]
        public void TheApplicationCanCreateBranches()
        {
            var result = _git.CreateBranch("test");
            Assert.That(result, Is.EqualTo("New Branch: test"));
        }

        [Test]
        public void TheApplicationCanSwapBranches()
        {
            var result = _git.SwapBranch("test");
            Assert.That(result, Is.EqualTo("Sawp Branch: test"));
        }

        [Test]
        public void TheApplicationCanMergeBranches()
        {
            var result = _git.MergeBranch("test");
            Assert.That(result, Is.EqualTo("Merge Branch: test"));
        }

        [Test]
        public void TheApplicationCanSolveMergeConflicts()
        {
            var result = _git.SolveMergeConflict();
            Assert.That(result, Is.EqualTo("Merge conflict solved!"));
        }

        [Test]
        public void TheApplicationCanAddFilesToAGitCommit()
        {
            var result = _git.AddFilesToCommit(new List<string>() { "a", "b"}.ToArray());
            Assert.That(result, Is.EqualTo("Added Files a, b, "));
        }

        [Test]
        public void TheApplicationCanPostAGitCommit()
        {
            var result = _git.PostCommit("test");
            Assert.That(result, Is.EqualTo("Posted test"));
        }

    }
}
