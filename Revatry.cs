using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RevatryFramework
{
    /// <summary>
    /// An Basic async Http Framework
    /// </summary>



    public class RevatryHTTP
    {



        //The http server
        private HttpListener server = null;

        public string serverUrl;

        public bool serverStop = false;

        public List<Session> Sessions = new List<Session>(); //Has sessions, sessions isnt permament if you reset server

        public List<Page> pages = new List<Page>(); //Contains pages 

        //A constructor to initalize objects
        /// <summary>
        /// Use this constructor to initalize variables of the server
        /// This is required to server function correctly
        /// You dont need to add "/" end of the server url the Revatry automatically adds it
        /// <code>rev = new RevatryHTTP(serverurlandport)</code>
        /// </summary>
        public RevatryHTTP(string index)
        {

            server = new HttpListener();
            //Reserves to future use by user of Revatry
            serverUrl = index;
            server.Prefixes.Add(index+"/");
        }
        /// <summary>
        /// Listens on the specified Port
        /// <exception> HttpServerNotInitalizedException happens if somehow server does not get initalized</exception>
        /// </summary>
        public void Listen()
        {
            if(server != null)
            {
                //Starts up the server
                server.Start();

            }
            else
            {
                throw new HttpServerNotInitalizedException();
            }

        }

        ///<summary>
        ///Sends a piece of html or data depending on mime and url specified
        ///</summary>
        public async void Send(string data,HttpListenerResponse res)
        {

            var dataBytes = Encoding.UTF8.GetBytes(data);
            var dataBytesLength = dataBytes.Length;
            res.ContentLength64 = dataBytesLength;
            await res.OutputStream.WriteAsync(dataBytes, 0, dataBytesLength);

        }

        ///<summary>
        ///Listen for get requests
        ///</summary>
        public async void ListenForPages() //,string message Get Action<HttpListenerResponse> method
        {

            //string dir,
            while (!serverStop)//serverstop
           {
                 //dt = null;

                var dt = await server.GetContextAsync();
                var req = dt.Request;

                var res = dt.Response;

                if (dt == null)
                    continue;
                string[] url = req.RawUrl.Split('/');
               
                var urlSize = url.Length;
                
                //var dirData = dir.Split('/');

                for (int i = 0; i < urlSize - 1 ; i++)
                    {

                        
                        if(pages.Exists(find => find.relativePath == url[i + 1]))//dirData[i] Find
                        {
                        var id = pages.FindIndex(find => find.relativePath == url[i + 1]);
                        //Use method to do extra stuff
                        pages[id].methodToCall(res);
                          //  method(res);
                        }
                        else
                        {
                            //TODO: REDIRECT TO ERROR PAGE
                          //  Redirect(serverUrl + "/404", res);
                            Send("404", res);
                            EndRequest(res);

                        }
                    }
               // }
            }
        }



        /// <summary>
        /// Listens for any request sent to server
        /// </summary>
        public async void ReqListener()
        {



        }

        public void EndRequest(HttpListenerResponse res)
        {

            res.OutputStream.Close();
            res.Close();
        }

        ///<summary>
        ///Redirects client to a url
        ///</summary>
        public void Redirect(string url,HttpListenerResponse res)
        { SetHeaders(res, HttpResponseHeader.Location, url, "text/plaintext");  }
        public void SetHeaders(HttpListenerResponse res,HttpResponseHeader header1,string headerConfig,string mime)
        {
            res.Headers.Add(header1, headerConfig);
            res.ContentType = mime;
            res.StatusCode = (int)HttpStatusCode.OK;
        }

        public void SessionGenerate(string session_name,HttpListenerResponse res)
        {
            //CookieCollection cookies = new CookieCollection();
            Cookie cookie = new Cookie();
            cookie.Name = session_name;
            Session session = new Session();
            Sessions.Add(session);
            cookie.Value = session.key;
            //cookies.Add(cookie);
            res.SetCookie(cookie);
        }
    }

    public class HttpServerNotInitalizedException: Exception{
        public HttpServerNotInitalizedException():base("You did not started the server") //string ex
        {

        }
    }
}
