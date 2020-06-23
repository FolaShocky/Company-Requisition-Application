using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace DissertWindowsFormApplication
{
    class Message
    {
        private static Message _MessageThreadInstance;
        private Thread MessagingThread;
        public ConcurrentQueue<string> ConcurrentMessageQueue { get; private set; }
        private Message()
        {
            ConcurrentMessageQueue = new ConcurrentQueue<string>();   
            
        }
        public static Message GetInstance()
        {
            if (_MessageThreadInstance == null)
            {
                _MessageThreadInstance = new Message();
            }
            return _MessageThreadInstance;
        }

        public bool SendMessage()
        {
            try
            { 
                IPHostEntry ipHostEntry = Dns.GetHostEntry("localhost/WebApplication1");
                IPAddress ipAddress = ipHostEntry.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 54429);
                using(Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(ipEndPoint);
                    using(NetworkStream networkStream = new NetworkStream(socket))
                    {
                        using(StreamWriter streamWriter = new StreamWriter(networkStream))
                        {
                            streamWriter.WriteLine(Constants.Change_Made);
                        }
                    }
                }
                return true;
            }
            catch(IOException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public ConcurrentQueue<string> RetrieveMessageQueue()
        {
            try
            {
                 IPHostEntry ipHostEntry = Dns.GetHostEntry("localhost/WebApplication1");
                 IPAddress ipAddress = ipHostEntry.AddressList[0];
                 IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 54429);
                 using (Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                 {
                     socket.Connect(ipEndPoint);
                     using (NetworkStream networkStream = new NetworkStream(socket))
                     {
                         using (StreamReader streamReader = new StreamReader(networkStream))
                         {
                             while (!streamReader.EndOfStream)
                             {
                                 ConcurrentMessageQueue.Enqueue(streamReader.ReadLine());
                             }
                         }
                     }
                 }
                 MessagingThread.Start();
                 return ConcurrentMessageQueue;
            }
            catch (SocketException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public void ClearQueue()
        {
            ConcurrentMessageQueue = new ConcurrentQueue<string>();
        }
    }
}
