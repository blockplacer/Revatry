using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
namespace RevatryFramework
{

    public static class FleckAbstraction
    {



        public static void Server(Action<string> OnGetMessage)
        {
            WebSocketServer server = new WebSocketServer("ws://0.0.0.0:7000");

            server.Start(socket =>
			{
				socket.OnOpen = () =>
				{
				};

				socket.OnClose = () =>
				{
				};

				socket.OnMessage = message =>
				{
                    OnGetMessage(message);
				};

                socket.OnError = exception =>
				{
                    Console.WriteLine("Uh, oh some error happened on the Fleck Heres the Info might help you to solve issue:" + exception);
				};
			});
        }

    }
}
