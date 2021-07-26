//MIT LICENSE
using System;
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

        public string relativePath; //Virtual path, its relative dont add https://example.com/bruh add bruh and dont use "/" it breaks stuff unless you using virtual directories correct one going to be like that: bruh/bruh/bruh or bruh but no / on start



        //--PLANNED STUFF--//

        /*
         * Making seperated systems for virtual pages can have data stored on some actual variable*/

        public string html = ""; //Contains page example: <html><head></head><body></body></html>

        //public int parameterLocation = 0; //After what location like https://example.com/shop but what if you wanted to do https://example.com/shop/1 well this is for you shop there is parameter 0 so
        //if you make this 0 its going to take any extension to url after this point as parameter 1 here could be taken in the source

        //public string[] parameters; //The variable contains parameters


        public Action<HttpListenerResponse,HttpListenerRequest> methodToCallGet = null; //Gets called if detector finds the page, for get requests
        public Action<HttpListenerResponse, HttpListenerRequest> methodToCallPost = null;
        public Action<HttpListenerResponse, HttpListenerRequest> methodToCallPut = null;
        public Action<HttpListenerResponse, HttpListenerRequest> methodToCallDelete = null;

        //public string method = "GET"; //It could been POST,GET,PUT,DELETE
        public Page(string path,Action<HttpListenerResponse,HttpListenerRequest> call)
        {
            relativePath = path;
            methodToCallGet = call;
        }
        public Page(string path)
        {
            relativePath = path;
        }
        public Page(string path, Action<HttpListenerResponse, HttpListenerRequest> get, Action<HttpListenerResponse, HttpListenerRequest> post)
        {
            relativePath = path;
            methodToCallGet = get;
            methodToCallPost = post;
        }
        public Page(string path, Action<HttpListenerResponse, HttpListenerRequest> get, Action<HttpListenerResponse, HttpListenerRequest> post, Action<HttpListenerResponse, HttpListenerRequest> put)
        {
            relativePath = path;
            methodToCallGet = get;
            methodToCallPost = post;
            methodToCallPut = put;
        }

        HTTPReqs reqType = new HTTPReqs();
    }
}
