using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame.Networking
{
    class Packet
    {
        public string send { get;}
        public List<string> value { get;}
        public bool end { get; }
        /// <summary>
        /// Simple wrapper to make standardized sending packets.
        /// </summary>
        /// <param name="_send"></param>
        /// <param name="_value"></param>
        public Packet(string _send, List<string> _value)
        {
            send = _send;
            value = _value;
        }
    }
}
