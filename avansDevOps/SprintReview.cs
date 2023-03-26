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

        public SprintReview()
        {
            SummaryPosted = false;
        }

        public void UploadSummary(string summary)
        {
            Console.WriteLine(summary);
            SummaryPosted = true;
        }

        public bool CanUploadSummary(User user, string summary)
        {
            bool canUpload = user.HasRole(typeof(ScrumMaster).ToString());
            if (canUpload)
                UploadSummary(summary);
            return canUpload;
        }
        public bool CanStartReview(User user) 
        { 
            bool canStart =  user.HasRole(typeof(ScrumMaster).ToString()) && SummaryPosted;
            if (canStart)
                StartReview();
            return canStart;
        }
        public void StartReview() 
        {
            Console.WriteLine("sprint review time!");
        }
    }
}
