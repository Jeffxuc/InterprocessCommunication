using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace AnonymousPipe_Client01
{
    class Pipe_Client01
    {
        static void Main(string[] args)
        {
            if(args.Length>0)
            {
                using(PipeStream pipeClient=new AnonymousPipeClientStream(PipeDirection.In,args[0]))
                {
                    Console.WriteLine("[Client] Current TransmissionMode: {0}, args length = {1} ,args[0] = {2}",
                        pipeClient.TransmissionMode, args.Length, args[0]);

                    using(StreamReader sr=new StreamReader(pipeClient))
                    {
                        string tmp;

                        do
                        {
                            Console.WriteLine("[Client] wait for sync...");
                            tmp = sr.ReadLine();
                        }
                        while (!tmp.StartsWith("SYNC"));

                        while((tmp=sr.ReadLine()) != null)
                        {
                            Console.WriteLine("[Client] Echo : " + tmp);
                        }
                    }
                }
            }

            Console.WriteLine("[CLIENT] Press Enter to continue...");
            Console.ReadKey();
        }
    }
}
