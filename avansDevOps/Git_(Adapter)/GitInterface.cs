using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Git__Adapter_
{
    public interface GitInterface
    {
        public string CreateRepo(string name);
        public string PushCode();
        public string PullCode();
        public string CreateBranch(string name);
        public string SwapBranch(string name);
        public string MergeBranch(string branchToMerge);
        public string SolveMergeConflict();
        public string AddFilesToCommit(string[] files);
        public string PostCommit(string name);

    }
}
