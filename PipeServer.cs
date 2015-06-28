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
        string pipeName;
        
        NamedPipeServerStream pipeStream;

        public PipeServer(string pipeName)
        {
            this.pipeName = pipeName;
        }

        public void Listener(string pipeName)
        {
            try
            {
                pipeStream = new NamedPipeServerStream(pipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                pipeStream.BeginWaitForConnection(Waiter, pipeStream);                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void Waiter(IAsyncResult result)
        {
            try
            {
                NamedPipeServerStream pipe = (NamedPipeServerStream)result.AsyncState;
                pipe.EndWaitForConnection(result);

                byte[] pipeBuffer = new byte[255];
                
                pipe.Read(pipeBuffer, 0, 255);
                
                string position = Encoding.UTF8.GetString(pipeBuffer, 0, pipeBuffer.Length);
                CallbackMethod.Invoke(position);

                pipe.Close();
                pipe = null;
                pipe = new NamedPipeServerStream(pipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                pipe.BeginWaitForConnection(Waiter, pipe);

            }
            catch (Exception e)
            {
                
                throw e;
            }
            
        }
    }
}
