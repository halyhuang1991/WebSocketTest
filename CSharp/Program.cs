using System;
 using WebSocketSharp;
 using WebSocketSharp.Server;
namespace CSharp
{
    class Program
    {
        public class Laputa : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                Console.Write(e.Data);
                var msg = e.Data == "BALUS"
                          ? "I've been balused already..."
                          : "I'm not available now.";

                Send(msg);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var wssv = new WebSocketServer("ws://localhost:8880/");
            wssv.AddWebSocketService<Laputa>("/Laputa");
            wssv.AddWebSocketService<Laputa>("/");
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
