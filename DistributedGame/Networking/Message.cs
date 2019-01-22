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
        /// <summary>
        /// A simple wrapper for messages.
        /// </summary>
        /// <param name="_text"></param>
        /// <param name="_r"></param>
        /// <param name="_g"></param>
        /// <param name="_b"></param>
        /// <param name="_time"></param>
        public Message(string _text, int _r = 255, int _g = 255, int _b = 255, float _time = 5F)
        {
            text = _text;
            r = _r;
            g = _g;
            b = _b;
            time = _time;
        }
        /// <summary>
        /// Returns the color created from the given R,G,B values.
        /// </summary>
        /// <returns></returns>
        public Color GetColor()
        {
            Color color = new Color(r, g, b);
            return color;
        }
    }
}
