using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame.Maths
{
    class SuperRandom
    {
        /// <summary>
        /// Internal random object. Use for simple random like
        /// ints and doubles
        /// </summary>
        public Random random;

        /// <summary>
        /// A layer over the default C# Random. Might add more
        /// functions later?
        /// </summary>
        public SuperRandom()
        {
            random = new Random();
        }

        /// <summary>
        /// A random chance to return true.
        /// 
        /// 0.5 = 50% chance to return true.
        /// </summary>
        /// <param name="chance">chance to return true</param>
        /// <returns></returns>
        public bool Chance(float chance)
        {
            return random.NextDouble() < chance;
        }
    }
}
