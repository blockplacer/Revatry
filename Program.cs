using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RevatryFramework
{
    class Program
    {
        public static RevatryHTTP revatry = new RevatryHTTP("http://localhost:80");



        static void Main(string[] args)
        {
            revatry.pages.Add(new Page("test",testMethod));
            revatry.pages.Add(new Page("random", randomNumber));
            revatry.Listen();
            revatry.Get(testMethod);//"test", 
            Console.ReadKey();
        }
        static void testMethod(HttpListenerResponse res)
        {


            revatry.Send("<b>Test</b>", res);
            revatry.EndRequest(res);
           
        }
        static void randomNumber(HttpListenerResponse res)
        {


            revatry.Send("<b>Random Number Generator:"+new Random().Next(0,55555)+ "</b>", res);
            revatry.EndRequest(res);

        }
    }
}
