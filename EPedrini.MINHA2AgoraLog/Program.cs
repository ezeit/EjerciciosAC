using System;
using System.IO;
using System.Net;
using EPedrini.MINHA2AgoraLog.Interfaces;
using EPedrini.MINHA2AgoraLog.Models;
using EPedrini.MINHA2AgoraLog.Parsers;

namespace EPedrini.MINHA2AgoraLog
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var url = args.Length == 2 && !string.IsNullOrEmpty(args[0]) ? args[0] : "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
                var path = args.Length == 2 && !string.IsNullOrEmpty(args[1]) ? args[1] : "./Output/minhaCDN1.txt";
                string logResource = string.Empty;

                if (String.IsNullOrEmpty(Path.GetExtension(path)))
                    path += ".txt";

                var fullPath = Path.GetFullPath(path);
                if (string.IsNullOrEmpty(fullPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Output");
                    return;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                //I'm using Webclient.DownloadString instead of WebRequest because in this case the app retrieve's only one remote resource
                using (var client = new WebClient())
                {
                    logResource = client.DownloadString(url);
                    IAgoraLogParser parser = new MINHA2AgoraParser();

                    using (StreamWriter file = new StreamWriter(fullPath))
                    {
                        file.WriteLine("#Version: 1.0");
                        file.WriteLine($"#Date: {DateTime.Now:dd/MM/yyyy hh:mm:ss}");
                        file.WriteLine("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");
                        file.WriteLine();
                        foreach (var entry in logResource.Split(new[] { "\r\n", "\r", "\n" },
                            StringSplitOptions.RemoveEmptyEntries))
                        {
                            AgoraLogEntry.TryParse(entry, parser, out var agoraLogEntry);
                            Console.WriteLine(agoraLogEntry.ToString());
                            file.WriteLine(agoraLogEntry.ToString());
                        }
                    }
                }

                if (string.IsNullOrEmpty(logResource))
                    Console.WriteLine("Nothing to convert");

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Press any key to quit");
                Console.ReadKey();
            }
        }
    }
}
