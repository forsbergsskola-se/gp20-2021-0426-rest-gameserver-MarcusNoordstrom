using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace TinyBrowser
{
    class Program
    {
        static void Main(string[] args) {
            var tcpClient = new TcpClient();
            var webSite = "www.acme.com";
            tcpClient.Connect(webSite, 80);
            var stream = tcpClient.GetStream();
            var send = Encoding.ASCII.GetBytes("GET / HTTP/1.1" + Environment.NewLine +
                                               "Host: acme.com" + Environment.NewLine+Environment.NewLine);
            stream.Write(send, 0, send.Length);
            
            var sr = new StreamReader(stream);
            var str = sr.ReadToEnd();
            //Console.WriteLine(str);
            tcpClient.Close();
            stream.Close();

            var r = Regex.Matches(str, @"<(a|link).*?href=(""|')(.+?)(""|').*?>").Select(m =>
                m.Value).ToArray();
            var re = new RegExp("^(www|http|https)://", "i");

            foreach (var VARIABLE in r) {
                Console.WriteLine(VARIABLE);
            }
            
        }
    }
}
