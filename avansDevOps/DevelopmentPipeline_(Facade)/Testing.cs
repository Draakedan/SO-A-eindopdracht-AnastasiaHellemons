﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.DevelopmentPipeline__facade_
{
    public class Testing
    {
        public Testing() { }
        public string Test(bool succeed) {
            string s = "";
            if (succeed)
                s = "testing: pass";
            return s; 
        }
    }
}
