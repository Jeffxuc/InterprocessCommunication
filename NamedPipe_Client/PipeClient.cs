using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace NamedPipe_Client
{
    class PipeClient
    {
        static void Main(string[] args)
        {
            using(NamedPipeClientStream pipe_Client=new NamedPipeClientStream(".","testPipe",PipeDirection.In))
            {
                // Connect to the pipe or wait until the pipe is available.
                Console.WriteLine("Attempting to connect to pipe...");
                pipe_Client.Connect();

                Console.WriteLine("connected to pipe.");
                Console.WriteLine("There are currently {0} pipe server instances open.", pipe_Client.NumberOfServerInstances);

                using(StreamReader sr=new StreamReader(pipe_Client))
                {
                    // Display the read text to the console
                    string tmp;
                    while((tmp=sr.ReadLine()) != null)
                    {
                        Console.WriteLine("Receive from server: {0}", tmp);
                    }
                }

            }

            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
