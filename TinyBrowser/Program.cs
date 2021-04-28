﻿using System;
using System.Collections.Generic;
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

            var linkList = new Dictionary<string, string>();

            var links = new List<string>();
            
            //Gets links
            for (int i = 0; i < str.Length; i++) {
                var link = "";
                var name = "";
                string result = "";
                if (str[i] == '<') {
                    if (str[i + 1] == 'a') {
                        i += 9;
                        while (str[i] != '"') {
                            result += str[i];
                            i++;
                        }
                        links.Add(result);
                    }
                }
            }








            //Console.WriteLine(str);
            
            
            
            
            
            // var links = Regex.Matches(str, @"<(a|link).*?href=(""|')(.+?)(""|').*?>").Select(m =>
            //     m.Value).ToArray();
            //
            // // var names = Regex.Matches(str, @"\"">(.*?)\</a>").Select(m =>
            // //     m.Value).ToArray();
            // List<string> names = new List<string>();
            //
            // for (int i = 0; i < links.Length; i++) {
            //     var current = str.IndexOf(links[i]);
            //     var result = "";
            //     
            //     while (str[current] != '>') {
            //         current++;
            //         // Console.Write(str[current]);
            //     }
            //     
            //     while (str[current] != '<') {
            //         current++;
            //         var charToadd = str[current];
            //
            //         if (charToadd != '<') {
            //             result += charToadd;    
            //         }
            //     }
            //
            //     if (result != "") {
            //         names.Add(result);
            //     }
            // }
            //
            // names = names.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            //
            // for (int i = 0; i < names.Count; i++) {
            //     Console.WriteLine(names[i]);
            // }
            //
            // Console.WriteLine("---------------------------------------------");
            //
            // foreach (var link in links) {
            //     Console.WriteLine(GetStringBetweenSameChar(link, '"'));
            // }
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
