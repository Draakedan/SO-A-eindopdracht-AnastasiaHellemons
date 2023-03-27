using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Repport.RepportGenerate__builder_
{
    public class HeaderRepportGenerator : IRepportExtraGenerator
    {
        public string BuildCompanyName()
        {
            return "header: company name";
        }

        public string BuildDate()
        {
            return "header: date";
        }

        public string BuildProjectName()
        {
            return "header: project name";
        }

        public string BuildSprintVersion()
        {
            return "header: version number";
        }
    }
}
