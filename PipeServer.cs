using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;

namespace Hexy
{
    public delegate void MessageSender(string id);
    class PipeServer
    {
        public event MessageSender CallbackMethod;
        
        NamedPipeServerStream pipeStream;
        public PipeServer(string pipeName)
        {
            pipeStream = new NamedPipeServerStream(pipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            pipeStream.BeginWaitForConnection(ConnectWait, pipeStream);
            
        }

        private void ConnectWait(IAsyncResult result)
        {
            try
            {
                NamedPipeServerStream pipe = (NamedPipeServerStream)result.AsyncState;
                byte[] pipeBuffer = new byte[255];

                string data = Encoding.UTF8.GetString(pipeBuffer);
            }
            catch (Exception e)
            {
                
                throw;
            }
            
        }
    }
}
