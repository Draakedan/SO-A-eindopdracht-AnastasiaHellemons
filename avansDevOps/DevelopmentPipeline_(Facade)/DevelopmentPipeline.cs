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
        private PackageInstallation Installation { get; set; }
        private Building Building { get; set; }
        private Testing Testing { get; set; }
        private Deployment Deployment { get; set; }

        public string CancelPipeline() { return string.Empty; }
        public bool CanCancelPipeline(User user) { return false; }
        public string StartPipeline() { return string.Empty; }
        public void SetUpPipeline(string instructions) { }
        public void SendMessage() { }
        public bool CanStartPipeline(User user) { return false; }
    }
}
