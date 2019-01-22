using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame.Networking
{
    class Message
    {
        public DateTime startTime { get; set; }
        public string text;
        int r = 0;
        int g = 0;
        int b = 0;
        public float time = 5;
        public Message(string _text, int _r = 255, int _g = 255, int _b = 255, float _time = 5F)
        {
            text = _text;
            r = _r;
            g = _g;
            b = _b;
            time = _time;
        }
        public Color GetColor()
        {
            Color color = new Color(r, g, b);
            return color;
        }
    }
}
