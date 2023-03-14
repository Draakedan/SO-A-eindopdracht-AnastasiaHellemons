using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Git__Adapter_
{
    public class GitHandler
    {
        public GitHandler() { }
        public void CreateRepo(string name) { }
        public void PushCode() { }
        public void PullCode() { }
        public void CreateBranch(string name) { }
        public void SwapBranch(string branch) { }
        public void MergeBranch(string branchToMerge) { }
        public void SolveMergeConflict() { }
        public void AddFilesToCommit(string[] files) { }
        public void PostCommit(string name) { }
    }
}
