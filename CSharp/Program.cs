using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
namespace CSharp
{
    class Program
    {
        public class Laputa : WebSocketBehavior
        {
            private string _name;
            protected override void OnOpen()
            {
                _name = Context.QueryString["name"];
                Console.WriteLine(Context.Origin);
                Console.WriteLine(_name);
            }
            protected override void OnMessage(MessageEventArgs e)
            {
                Console.Write(e.Data);
                Sessions.Broadcast ("Broadcast:"+e.Data + _name);//广播给所有用户
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
            //var wssv = new WebSocketServer (4649);
   
            var a = Configuration.GetAppConfig("a");
            Console.WriteLine(a);
            // var cert = ConfigurationManager.AppSettings["ServerCertFile"];
            // var passwd = ConfigurationManager.AppSettings["CertFilePassword"];
            
            // wssv.SslConfiguration.ServerCertificate = new X509Certificate2(cert, passwd);
            wssv.AddWebSocketService<Laputa>("/Laputa");
            wssv.AddWebSocketService<Laputa>("/");
            //--------------加用户验证    客户端发送验证

            // wssv.AuthenticationSchemes = AuthenticationSchemes.Basic;
            // wssv.Realm = "WebSocket Test";
            // wssv.UserCredentialsFinder = id =>
            // {
            //     var name = id.Name;

            //     // Return user name, password, and roles.
            //     return name == "nobita"
            //            ? new NetworkCredential(name, "password", "gunfighter")
            //            : null; // If the user credentials are not found.
            // };

            wssv.Start();
           //---------------客户端发送信息
            Client.SendMsg();
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
