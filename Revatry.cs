using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using Fleck;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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



        public string sessionName = "Session";


        public string LoadLibs = ""; //String for htmlstart stuff a easier thing to put your libs eg: Bulma, Bootstrap, JQuery etc.

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


            try
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);
                var dataBytesLength = dataBytes.Length;
                res.ContentLength64 += dataBytesLength;
                try
                {


                await res.OutputStream.WriteAsync(dataBytes, 0, dataBytesLength);
                }
                catch (HttpListenerException)
                {

                    //Happens if too many requests done temporary fix
                }
            }
            catch (ObjectDisposedException)
            {
                /*ISSUE 1: For some reason object gets disposed and causes crashes a temporary "solution" to prevent the issue 
                 * even though there is error page sent correctly probably something you should be safe*/
                //Temporary fix
            }
            
            

        }


        ///<summary>
        ///Listen for get requests (GET) for (POST) <code>ListenForData</code>
        ///</summary>
        public async void ListenForPages() 
        {

            while (!serverStop)
           {


                var dt = await server.GetContextAsync();
                var req = dt.Request;

                var res = dt.Response;

                if (dt == null)
                    continue;
                string[] url = req.RawUrl.Split('/');
               
                var urlSize = url.Length;

                /*Redirect Web Socket requests to the Socketineer
                 * Windows 8/ Windows Server 2012/ Windows 10 / Windows Server 2016+ Is required
                 * */

                if (dt.Request.IsWebSocketRequest)
                {
                   
                    
                }

                for (int i = 0; i < urlSize - 1 ; i++)
                    {

                        
                        if(pages.Exists(find => find.relativePath == url[i + 1]))
                        {
                        var id = pages.FindIndex(find => find.relativePath == url[i + 1]);
                        //Use method to do extra stuff
                        pages[id].methodToCall(res,req);
                          
                        }
                        else
                        {
                            //TODO: REDIRECT TO ERROR PAGE

                            Send("404", res);
                            EndRequest(res);

                        }
                    }

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
            //Temporary fix until actual error has been fixed
            try
            {
                res.OutputStream.Close();
                res.Close();
            }
            catch (ObjectDisposedException)
            {

              
            }

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
            Cookie cookie = new Cookie();
            sessionName = session_name;
            cookie.Name = sessionName;
            Session session = new Session();
            session.generate();
            Sessions.Add(session);
            cookie.Value = session.key;
            res.SetCookie(cookie);
        }
        /// <summary>
        /// Get session s variables
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Session object variables list</returns>
        public List<object> GetSessionVariables(HttpListenerResponse req)
        {
            return Sessions.Find(x => x.key == req.Cookies[sessionName].Value).variables;
        }

        public void ResetSessionValues(HttpListenerRequest req)
        {
            var id = Sessions.FindIndex(x => x.key == req.Cookies[sessionName].Value);
            Sessions[id].variables = new List<object>(); //
        }
        /// <summary>
        /// Destroys a session from the use
        /// </summary>
        /// <param name="req"></param>
        public void DestroySession(HttpListenerRequest req)
        {
            var id = Sessions.FindIndex(x => x.key == req.Cookies[sessionName].Value);
            Sessions[id].variables = null;
            req.Cookies[sessionName].Expired = true;
        }
        /// <summary>
        /// Session Key
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Returns session key if it exist, if it cant find one its going to return "AnErrorHappened" </returns>
        public string GetSessionKey(HttpListenerResponse res,HttpListenerRequest req)
        {
            string toReturn = "AnErrorHappened"+ req.Cookies[sessionName].Value;
            if (Sessions.Exists(x => x.key == req.Cookies[sessionName].Value))
            {
                var valueKeeper = toReturn = Sessions.Find(x => x.key == req.Cookies[sessionName].Value).key;
                if(valueKeeper != null)
                    toReturn = valueKeeper;
            }
                return toReturn ;
        }

        /// <summary>
        /// Replaces variables inside of !!variable using regex
        /// </summary>
        public string TemplatingReplace(string variable,string toReplace,string html)
        {
            var code = "!!" + variable;
            return new Regex(@"\b" + Regex.Escape(variable)).Replace(html, toReplace);// regex.Replace(html, toReplace); 
        }

        /// <summary>
        /// Sends stuff like <code><html>etc.</html></code> You can add bulma, bootstrap, jquery etc. by modifying loadReplace!
        /// </summary>
        public string SendHtmlStart(string title)
        {
            return TemplatingReplace("PLACEHOLDER__LOAD_STUFF",LoadLibs, TemplatingReplace("__PLACEHOLDER_TITLE ", title, " <html><head> <title> __PLACEHOLDER_TITLE </title> PLACEHOLDER__LOAD_STUFF </head><body>"));//Send(,res);
        }
        public string SendHtmlEnd()
        {
            return "</body></html>"; 
            
        }
        /// <summary>
        /// Simple optmization
        /// </summary>
        /// <param name="html">Text</param>
        /// <returns>html document</returns>
        public string optimizeHtml(string html)
        { return html.Replace(" ", "").Replace(" !important","!important"); }

        /// <summary>
        /// You can use this for proxy, apis etc.
        /// Currently Only Supports GET and POST
        /// </summary>
        public string Request(HTTPReqs requestType,string url)
        {
            string toReturn = "";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            
            switch (requestType)
            {
                case HTTPReqs.POST:
                    request.Method = "POST";
                    break;
                case HTTPReqs.GET:
                    request.Method = "GET";
                    break;
                default:
                    break;
            }

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                Stream dt = response.GetResponseStream();
                StreamReader read = new StreamReader(dt);
                toReturn = read.ReadToEnd();
                read.Close();
                dt.Close();
            }
            return toReturn;
        }

        /// <summary>
        /// Use <code>EscapeFileLocation(text)</code> if you going to give user accces to reader urls
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public string LoadFile(string location)
        {
            return File.ReadAllText(location);
        }

        /// <summary>
        /// Path traversal prevention
        /// SEE: Path Trevarsal Vulnerability is a vulnerability that pages fetch data from server
        /// can receive directory parameters could be used to read sensitive information
        /// might result in leak of confidental data.
        /// </summary>
        /// <param name="location">Text</param>
        /// <returns>gives back a string doesnt have any path command strings</returns>
        public string EscapeFileLocation(string location)
        { return location.Replace("/", "").Replace(".", "").Replace(@"\", ""); }

    }

    public class HttpServerNotInitalizedException: Exception{
        public HttpServerNotInitalizedException():base("You did not started the server")
        {

        }
    }
}
