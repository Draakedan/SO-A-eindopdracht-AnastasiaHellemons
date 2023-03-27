using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.DevelopmentPipeline__facade_
{
    public class Deployment
    {
        public Deployment() { }
        public string Deploy(bool succeed) 
        {
            string s = "";
            if (succeed)
                s = "deploy: pass";
            return s;
        }
    }
}
