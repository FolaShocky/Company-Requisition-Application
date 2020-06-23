using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Threading;
using static WebApplication1.Models.Constants;
namespace WebApplication1.Models
{
    public class ApplicationServer
    {

        private static ApplicationServer applicationServerInstance;
        private const string New_Line = "\n";
        
        private ApplicationServer()
        {

        }

        public static ApplicationServer GetInstance()
        {
            if (applicationServerInstance == null)
                applicationServerInstance = new ApplicationServer();
            return applicationServerInstance;
        }

        public string RetrieveMessage()
        {
            return "";
        }



        public void RunServer()
        {
            try
            {
                TcpListener tcpListener = new TcpListener(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], 54229);
                tcpListener.Start();
                Debug.WriteLine("Awaiting Server...");
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Thread serverThread = new Thread(ThreadLogic);
                    serverThread.Start();
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
            }
        }
         

        public static void ThreadLogic(object _tcpClient)
        {
            try
            {
                Debug.WriteLine("Connected to the server successfully");
                using (TcpClient tcpClient = _tcpClient as TcpClient)
                {
                    using (NetworkStream networkStream = tcpClient.GetStream())
                    {
                        using (StreamReader streamReader = new StreamReader(networkStream))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(networkStream))
                            {
                                while (!streamReader.EndOfStream)
                                {
                                    string line = streamReader.ReadLine();
                                    if (line != null)
                                        streamWriter.WriteLine(Change_Made);
                                    else
                                        streamWriter.WriteLine(No_Change_Made);
                                }   
                            }
                        }
                    }
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }
        }
    }
   
}