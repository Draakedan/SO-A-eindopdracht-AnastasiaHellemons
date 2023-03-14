using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Repport.RepportGenerate__builder_
{
    public interface IRepportExtraGenerator
    {
        public void BuildCompanyName();
        public void BuildProjectName();
        public void BuildSprintVersion();
        public void BuildDate();
    }
}
