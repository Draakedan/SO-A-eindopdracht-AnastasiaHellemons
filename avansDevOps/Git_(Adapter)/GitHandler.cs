using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Git__Adapter_
{
    public class GitHandler
    {
        private GitInterface git { get; set; }
        public GitHandler() 
        {
            git = new GitAddapter();
        }
        public string CreateRepo(string name) 
        { 
            return git.CreateRepo(name); 
        }
        public string PushCode() 
        { 
            return git.PushCode(); 
        }
        public string PullCode() 
        { 
            return git.PullCode(); 
        }
        public string CreateBranch(string name) 
        {
            return git.CreateBranch(name);
        }
        public string SwapBranch(string branch) 
        {
            return git.SwapBranch(branch); 
        }
        public string MergeBranch(string branchToMerge) 
        {
            return git.MergeBranch(branchToMerge); 
        }
        public string SolveMergeConflict() 
        { 
            return git.SolveMergeConflict(); 
        }
        public string AddFilesToCommit(string[] files) 
        {
            return git.AddFilesToCommit(files);
        }
        public string PostCommit(string name) 
        {
            return git.PostCommit(name); 
        }
    }
}
