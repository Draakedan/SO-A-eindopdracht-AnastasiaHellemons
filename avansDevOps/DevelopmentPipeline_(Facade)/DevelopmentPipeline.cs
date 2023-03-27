using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.DevelopmentPipeline__facade_
{
    public class DevelopmentPipeline
    {
        private string Instruct { get; set; }
        private PackageInstallation Installation { get; set; }
        private Building Building { get; set; }
        private Testing Testing { get; set; }
        private Deployment Deployment { get; set; }

        public DevelopmentPipeline() 
        {
            Instruct = "pass pass pass pass";
            Installation = new();
            Building = new();
            Testing = new();
            Deployment = new();
        }

        public string CancelPipeline() 
        {
            return "cancled";
        }
        public bool CanCancelPipeline(User user) 
        {
            return user.HasRole(typeof(ScrumMaster).ToString()); 
        }
        public string StartPipeline() 
        {
            string s = "";
            string[] indiv = Instruct.Split(" ");

            if (indiv[0].Equals("pass"))
                s += this.Installation.Install(true);
            else if (indiv[0].Equals("fail"))
                s += this.Installation.Install(false);

            if (indiv[1].Equals("pass"))
                s += this.Building.Build(true);
            else if (indiv[1].Equals("fail"))
                s += this.Building.Build(false);

            if (indiv[2].Equals("pass"))
                s += this.Testing.Test(true);
            else if (indiv[2].Equals("fail"))
                s += this.Testing.Test(false);

            if (indiv[3].Equals("pass"))
                s += this.Deployment.Deploy(true);
            else if (indiv[3].Equals("fail"))
                s += this.Deployment.Deploy(false);
            return s; 
        }
        public void SetUpPipeline(string instructions) 
        {
            this.Instruct = instructions;
        }
        public bool CanStartPipeline(User user) 
        {
            return user.HasRole(typeof(ScrumMaster).ToString()); 
        }
    }
}
