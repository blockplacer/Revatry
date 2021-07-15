using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevatryFramework
{
    /*public static class Aggregator
    {




        /// <summary>
        /// Puts every library for pages on single file so loading time and performance increases significantly
        /// </summary>
        /// <returns>Every library combined into one</returns>
        public static string Aggregate(FELibType type)
        {


        }
    }*/
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
