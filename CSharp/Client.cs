using System;
using WebSocketSharp;
namespace CSharp
{
    public class Client{
        public static void SendMsg(){
            using (var ws = new WebSocket("ws://localhost:8880/Laputa?name=nobita"))
            {
                ws.OnOpen += (sender, e) =>
                {
                    Console.WriteLine("Laputa open!");
                };
                ws.OnMessage += (sender, e) =>{
                    if (e.IsText) {
                        Console.WriteLine("is Text");
                    }
                     Console.WriteLine("Laputa says: " + e.Data);
                };
                ws.OnError += (sender, e) =>
                {
                    Console.WriteLine("Laputa Error!");
                };
                ws.OnClose += (sender, e) =>
                {
                    Console.WriteLine("Laputa Close!");

                };
                ws.SetCredentials ("nobita", "password", true);  
                ws.Connect();
                ws.Send("BALUS");
                Console.ReadKey(true);
            }
        }

    }
}