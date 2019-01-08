using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FunNetworkKing
{
    class FNK
    {
        Socket listeningSocket;

        public void Run()
        {
            listeningSocket.Bind(new IPEndPoint(new IPAddress(), ));
        }
    }
}
