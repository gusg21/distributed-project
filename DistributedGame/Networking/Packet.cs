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
        public Packet(string _send, List<string> _value , bool _end = false)
        {
            send = _send;
            value = _value;
            end = _end;
        }
    }
}
