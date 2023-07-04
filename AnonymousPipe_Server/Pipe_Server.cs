using System;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

namespace AnonymousPipe_Server
{
    class Pipe_Server
    {
        static void Main(string[] args)
        {
            Process AnonymousPipe_Client = new Process();
            // Pipe_Client01.exe 需要与Server程序位于同一目录下，否则需要给出相对路径会绝对路径。
            AnonymousPipe_Client.StartInfo.FileName = "Pipe_Client01.exe";

            using(AnonymousPipeServerStream pipeServerStream=new AnonymousPipeServerStream(PipeDirection.Out,HandleInheritability.Inheritable))
            {
                Console.WriteLine("[Server] Current TransmissionMode is : {0}", pipeServerStream.TransmissionMode);

                // 将客户端进程的句柄传递给服务端
                AnonymousPipe_Client.StartInfo.Arguments = pipeServerStream.GetClientHandleAsString();
                AnonymousPipe_Client.StartInfo.UseShellExecute = false;
                AnonymousPipe_Client.Start();

                Console.WriteLine("[Server] Handle = {0}", AnonymousPipe_Client.StartInfo.Arguments);

                pipeServerStream.DisposeLocalCopyOfClientHandle();

                try
                {
                    // Read user input and send that to the client process.
                    using(StreamWriter sw=new StreamWriter(pipeServerStream))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine("SYNC");
                        pipeServerStream.WaitForPipeDrain();

                        Console.WriteLine("[Server]Enter Text: ");
                        sw.WriteLine(Console.ReadLine());


                    }
                }
                catch(IOException e)
                {
                    Console.WriteLine("[Server] Error: {0}", e.Message);
                }
            }

            AnonymousPipe_Client.WaitForExit();
            AnonymousPipe_Client.Close();
            Console.WriteLine("[SERVER] Client quit. Server terminating.");

        }
    }
}
