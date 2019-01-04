using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedGame.Math
{
    class SuperRandom
    {
        public Random random;

        public SuperRandom()
        {
            random = new Random();
        }

        public int Int(int min, int max = 0)
        {
            return random.Next(min, max);
        }

        public bool Chance(float chance)
        {
            return random.NextDouble() < chance;
        }
    }
}
