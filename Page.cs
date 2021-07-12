﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RevatryFramework
{
    public class Page
    {




        /*
         * Pages are objects that stores "links" to methods to your get request
         * page urls
         * and other stuff
         * main difference from php etc. is
         * you can generate a virtual page without any disk operation!*/

        public string relativePath; //Virtual path, its relative dont add https://example.com/bruh add bruh and dont use "/" it breaks stuff



        //--PLANNED STUFF--//

        /*
         * Making seperated systems for virtual pages can have data stored on some actual variable*/

        public string html = ""; //Contains page example: <html><head></head><body></body></html>

        //public int parameterLocation = 0; //After what location like https://example.com/shop but what if you wanted to do https://example.com/shop/1 well this is for you shop there is parameter 0 so
        //if you make this 0 its going to take any extension to url after this point as parameter 1 here could be taken in the source

        //public string[] parameters; //The variable contains parameters


        public Action<HttpListenerResponse,HttpListenerRequest> methodToCall = null; //Gets called if detector finds the page

        public Page(string path,Action<HttpListenerResponse,HttpListenerRequest> call)
        {
            relativePath = path;
            methodToCall = call;
        }

        HTTPReqs reqType = new HTTPReqs();
    }
}
