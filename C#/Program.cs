using System.Diagnostics;
using System;
using System.IO;
using System.IO.Pipes;

namespace PIPE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    using (NamedPipeServerStream pipeServer = new("MainPipe", PipeDirection.InOut))
                    {
                        Console.WriteLine("Waiting for connection...");
                        pipeServer.WaitForConnection();
                        Console.WriteLine("Connected");

                        using (StreamReader sr = new(pipeServer))
                        using (StreamWriter sw = new(pipeServer))
                        {
                            sw.AutoFlush = true;
                            string receivedMsg = sr.ReadLine();
                            Console.WriteLine($"Recieved From Python {receivedMsg}");

                            Console.WriteLine("Hello from c#");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }
            }
        }
    }
}
