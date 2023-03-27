using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.DevelopmentPipeline__facade_
{
    public class Building
    {
        public Building() { }
        public string Build(bool succeed) 
        {
            string s = "";
            if (succeed)
                s = "building: pass";
            return s;
        }
    }
}
