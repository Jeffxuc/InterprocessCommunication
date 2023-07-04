using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace NamedPipe_Server
{
    class PipeServer
    {
        static void Main(string[] args)
        {
            using(NamedPipeServerStream pipe_Server=new NamedPipeServerStream("testPipe",PipeDirection.Out))
            {
                Console.WriteLine("Named Pipe Server Stream object is created.");

                Console.WriteLine("Waiting for client connection..");
                pipe_Server.WaitForConnection();
                Console.WriteLine("Client has been connected.");

                try
                {
                    // Read user input and send it to the client process.
                    using(StreamWriter sw=new StreamWriter(pipe_Server))
                    {
                        sw.AutoFlush = true;
                        Console.WriteLine("Enter text: ");
                        sw.WriteLine(Console.ReadLine());

                    }
                }
                catch(IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }

            }
        }
    }
}
