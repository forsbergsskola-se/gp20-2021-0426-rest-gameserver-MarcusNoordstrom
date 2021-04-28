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

            Console.WriteLine(str);
            
            var links = Regex.Matches(str, @"<(a|link).*?href=(""|')(.+?)(""|').*?>").Select(m =>
                m.Value).ToArray();
            
            var names = Regex.Matches(str, @"\"">(.*?)\</a>").Select(m =>
                m.Value).ToArray();

            foreach (var name in names) {
                Console.WriteLine(name);
            }
            
            foreach (var link in links) {
                //Console.WriteLine(GetStringBetweenSameChar(link, '"'));
            }
        }

         static string GetStringBetweenSameChar(string toGetFrom, char splitter) {
            var result = "";
            var shouldAppend = false;
            
            foreach (var c in toGetFrom) {
                if (c == splitter) {
                    shouldAppend = !shouldAppend;
                }

                if (shouldAppend && c != '"') {
                    result += c;
                }
            }
            
            return result;
        }
        
    }
}
