using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Git__Adapter_
{
    public class GitAddapter : GitInterface
    {
        private GitCLI GitCLI { get; set; }
        public GitAddapter()
        {
            GitCLI = new GitCLI();
        }

        public string CreateRepo(string name)
        {
            string s = GitCLI.GitCreateRepo(name);
            return s;
        }
        public string PushCode() 
        {
            return GitCLI.GitPushCode(); 
        }
        public string PullCode() 
        {
            return GitCLI.GitPullCode(); 
        }
        public string CreateBranch(string name) 
        {
            return GitCLI.GitCreateBranch(name); 
        }
        public string SwapBranch(string branch) 
        {
            return GitCLI.GitSwapBranch(branch); 
        }
        public string MergeBranch(string branchToMerge) 
        {
            return GitCLI.GitMergeBranch(branchToMerge); 
        }
        public string SolveMergeConflict() 
        {
            return GitCLI.GitSolveMergeConflict(); 
        }
        public string AddFilesToCommit(string[] files) 
        {
            string fileString = "";
            foreach (string file in files)
                fileString += file + ", ";
            return GitCLI.GitAddFilesToCommit(fileString); 
        }
        public string PostCommit(string name) 
        {
            return GitCLI.GitPostCommit(name); 
        }
    }
}
