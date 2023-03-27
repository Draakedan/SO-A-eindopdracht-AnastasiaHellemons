using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Repport.SaveFile__Factory_
{
    public class PDFFileSaver : IFileSaver
    {
        public string Save()
        {
            return "PDF:";
        }
    }
}
