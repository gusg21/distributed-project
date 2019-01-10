    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame
{
    class Peer
    {
        public void main()
        {

            try
            {
                String toSend = "U r dum";
                byte[] data = System.Text.Encoding.ASCII.GetBytes(toSend);
                //socket 
            }
            catch
            {
                Console.WriteLine("WHY DO YOU HATE ME?");
            }
        }
        
        
    }
}
