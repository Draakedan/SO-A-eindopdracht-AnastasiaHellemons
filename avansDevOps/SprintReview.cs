using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps
{
    public class SprintReview
    {
        private bool SummaryPosted { get; set; }

        public void UploadSummary(string summary) { }
        public bool CanUploadSummary(User user, string summary) { return false; }
        public bool CanStartReview(User user) { return false; }
        public void StartReview() { }
    }
}
