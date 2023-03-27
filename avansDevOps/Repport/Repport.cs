using avansDevOps.Repport.RepportGenerate__builder_;
using avansDevOps.Repport.SaveFile__Factory_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Repport
{
    public class Repport
    {
        public Repport() { }
        public string Generator(string extra)
        {

            RepportGenerator generator = extra switch
            {
                "footer" => new(new FooterRepportGenerator()),
                "header" => new(new HeaderRepportGenerator()),
                _ => new(),
            };

            return generator.BuildAll();
        }

        public string Save(string type)
        {
            string s;
            switch (type)
            {
                case "PDF":
                    s = new PDFFileSaver().Save(); 
                    break;
                case "PNG": 
                    s = new PNGFileSaver().Save(); 
                    break;
                case "JPEG":
                    s = new JPEGFileSaver().Save(); 
                    break;
                case "none": 
                    s = new RepportGenerator().BuildAll();
                    break;
                default: 
                    s = Generator(type); 
                    break;
            };
            return s;
        }
        public bool CanGenerate(User user)
        {
            return user.HasRole(typeof(ScrumMaster).ToString());
        }
    }
}
