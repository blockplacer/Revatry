using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevatryFramework
{

    public enum FELibType
    {
        Css,
        Js
    }
    public class FELib
    {

        public string url;
        public FELibType type;
       /* /// <summary>
        /// Should it be automatically hosted?
        /// </summary>*/
       // public bool host = true;
        public FELib(string url,FELibType type)
        { this.url = url; this.type = type; }
    }
}
