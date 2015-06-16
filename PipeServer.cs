using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;

namespace Hexy
{
    class PipeServer
    {
        public delegate void MessageSender(string id);
        public PipeServer(string name)
        {
            NamedPipeServerStream _pipeStream = new NamedPipeServerStream(name, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);

        }
    }
}
