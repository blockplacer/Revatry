using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RevatryFramework
{
    class Program
    {
        public static RevatryHTTP revatry = new RevatryHTTP("http://localhost:80");


        static string googly;
        static Database db = new Database();
        static void Main(string[] args)
        {
            revatry.pages.Add(new Page("test",testMethod));
            revatry.pages.Add(new Page("random", randomNumber));
            revatry.pages.Add(new Page("session", session));
            revatry.pages.Add(new Page("sessiongen", sessiongen));
            revatry.pages.Add(new Page("google", googleAsync));
            db.AddTable("test", 50);

            revatry.Listen();
            revatry.ListenForPages();//testMethodGet
            Console.ReadKey();
        }
        static void testMethod(HttpListenerResponse res, HttpListenerRequest req)
        {

            //Sends data to user
            revatry.Send(revatry.TemplatingReplace("__PLACEHOLDER_TEST", revatry.SendHtmlStart("Test") +"<b>Currently time is: " +DateTime.Now+ "</b>"+ revatry.SendHtmlEnd(), " __PLACEHOLDER_TEST"), res);
            
            revatry.EndRequest(res);

        }
        static void randomNumber(HttpListenerResponse res,HttpListenerRequest req)
        {


            revatry.Send("<b>Random Number Generator:"+new Random().Next(0,55555)+ "</b>", res);
            revatry.EndRequest(res);

        }
        static void session(HttpListenerResponse res,HttpListenerRequest req)
        {

            revatry.Send("<b>Key:" + revatry.GetSessionKey(res,req) + "</b>", res);
            revatry.EndRequest(res);

        }
        static void sessiongen(HttpListenerResponse res,HttpListenerRequest req)
        {

            revatry.SessionGenerate("Session", res);

            revatry.EndRequest(res);
        }
        static string dbb = db.Serialize();
        static async void googleAsync(HttpListenerResponse res, HttpListenerRequest req)
        {
            //Proxy
            googly = revatry.Request(HTTPReqs.GET, "https://google.com");

            revatry.Send(googly, res);

            revatry.EndRequest(res);
        }
    }
}
