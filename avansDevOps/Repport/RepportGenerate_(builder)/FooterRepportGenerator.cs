using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Repport.RepportGenerate__builder_
{
    public class FooterRepportGenerator : IRepportExtraGenerator
    {
        public string BuildCompanyName()
        {
            return "footer: company name";
        }

        public string BuildDate()
        {
            return "footer: date";
        }

        public string BuildProjectName()
        {
            return "footer: project name";
        }

        public string BuildSprintVersion()
        {
            return "footer: version number";
        }
    }
}
