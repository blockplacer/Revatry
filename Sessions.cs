using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RevatryFramework
{

    public class Session
    {


        //Prediction Prevention
       
        public string key = "";


        public List<SessionVariable> variables = new List<SessionVariable>();//objectobject
        /// <summary>
        /// A method that generates secure token keys
        /// </summary>
        public void generate()
        {
            byte[] randomData = new byte[30];

            RevatryHTTP.rng.GetBytes(randomData,0,30);

            for (int i = 0; i < randomData.Length; i++)
            {
                key += randomData[i];
            }
            


        }
        public Session(string key)
        { this.key = key; }
        public Session()
        { }
    }

    public class SessionVariable
    {
        public string name;
        public object variable;
        /// <summary>
        /// An easy to use session variable holder
        /// </summary>
        /// <param name="name">Name of the variable so you can easily call it</param>
        /// <param name="variable">Variable itself</param>
        public SessionVariable(string name,object variable)
        { this.name = name; this.variable = variable; }
    }
}
