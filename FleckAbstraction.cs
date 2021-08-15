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



        public static void Server(Action<string> OnGetMessage,Action onOpen,Action onClose, string addr = "ws://0.0.0.0:7000")
        {
            WebSocketServer server = new WebSocketServer(addr);

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
