using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;

namespace Hexy
{
    class PipeClient
    {
        public delegate void MessageSender(string id);

        public PipeClient(string name)
        {
            //NamedPipeClientStream _clientStream = new NamedPipeClientStream(".", name, PipeDirection.Out, PipeOptions.Asynchronous);
        }
    }
}
