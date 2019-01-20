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
        public string value { get;}
        public bool end { get; }
        public Packet(string _send, string _value = "", bool _end = false)
        {
            send = _send;
            value = _value;
            end = _end;
        }
    }
}
