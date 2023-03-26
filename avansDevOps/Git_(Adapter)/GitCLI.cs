using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Git__Adapter_
{
    public class GitCLI
    {
        public GitCLI() { }
        public static string GitCreateRepo(string name)
        {
            return "New repo: " + name;
        }
        public string GitPushCode()
        {
            return "Pushed Code!";
        }
        public string GitPullCode()
        {
            return "Pulled Code!";
        }
        public string GitCreateBranch(string name)
        {
            return "New Branch: " + name;
        }
        public string GitSwapBranch(string name)
        {
            return "Sawp Branch: " + name;
        }
        public string GitMergeBranch(string branchA)
        {
            return "Merge Branch: " + branchA;
        }
        public string GitSolveMergeConflict()
        {
            return "Merge conflict solved!";
        }
        public string GitAddFilesToCommit(string files) 
        {
            return "Added Files " + files; 
        }
        public string GitPostCommit(string name) 
        {
            return "Posted " + name; 
        }
    }
}
