using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Repport.RepportGenerate__builder_
{
    public class RepportGenerator
    {
        private IRepportExtraGenerator? _extraGenerator;
        public RepportGenerator(IRepportExtraGenerator extra)
        {
            _extraGenerator = extra;
        }
        public RepportGenerator()
        {
            _extraGenerator = null;
        }
        public string BuildExtra()
        {
            string s = "";
            if (_extraGenerator != null)
            {
                s = _extraGenerator.BuildProjectName() +
                    "\n" + _extraGenerator.BuildCompanyName() +
                    "\n" + _extraGenerator.BuildSprintVersion() +
                    "\n" + _extraGenerator.BuildDate();
            }
            return s;
        }
        public string BuildTeamSetup()
        {
            return "team setup";
        }
        public string BuildBurnDownChart()
        {
            return "burndown chart";
        }
        public string BuildEffortPoints()
        {
            return "effort points";
        }

        public string BuildBody()
        {
            return BuildTeamSetup() +
                "\n" + BuildBurnDownChart() +
                "\n" + BuildEffortPoints();
        }

        public string BuildAll()
        {
            string s = "";
            if (_extraGenerator != null && _extraGenerator.GetType() == typeof(HeaderRepportGenerator))
                s = BuildExtra() + "\n";
            s += BuildBody();
            if (_extraGenerator != null && _extraGenerator.GetType() == typeof(FooterRepportGenerator))
                s += "\n" + BuildExtra() ;

            return s;
        }
    }
}
