﻿using System;
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
        public static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        public string key = "";


        public List<object> variables = new List<object>();
        /// <summary>
        /// A method that generates secure token keys
        /// </summary>
        public void generate()
        {
            byte[] randomData = new byte[30];

            rng.GetBytes(randomData,0,30);

            for (int i = 0; i < randomData.Length; i++)
            {
                key += randomData[i];
            }
            


        }
    }
}
