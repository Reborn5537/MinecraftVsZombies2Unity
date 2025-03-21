﻿namespace Tools
{
    internal class XORShift128
    {
        public uint x = 0;
        public uint y = 0;
        public uint z = 0;
        public uint w = 0;

        private const uint MT19937 = 1812433253;

        // Initialize Xorshift using a signed integer seed, calculating the state values using the initialization method from Mersenne Twister (MT19937)
        // https://en.wikipedia.org/wiki/Mersenne_Twister#Initialization
        public void InitSeed(int seed)
        {
            x = (uint)seed;
            y = (uint)(MT19937 * x + 1);
            z = (uint)(MT19937 * y + 1);
            w = (uint)(MT19937 * z + 1);
        }

        // Explicitly set the state parameters
        public void InitState(uint x, uint y, uint z, uint w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        // XORShift, returns an unsigned 32-bit integer
        private uint XORShift()
        {
            uint t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;
            w = w ^ (w >> 19) ^ t ^ (t >> 8);
            return w;
        }

        // UInt32 / uint

        // UnityEngine.Random doesn't have any uint functions so these functions behave exactly like int Random.Range

        // Alias of base Next/XORShift
        public uint NextUInt()
        {
            return XORShift();
        }

        // Generate a random unsigned 32-bit integer value in the range 0 (inclusive) to max (exclusive)
        public uint NextUIntMax(uint max)
        {
            if (max == 0) return 0;
            return XORShift() % max;
        }

        // Generate random unsigned 32-bit integer value in the range min (inclusive) to max (exclusive)
        public uint NextUIntRange(uint min, uint max)
        {
            if (max - min == 0) return min;

            if (max < min)
                return min - XORShift() % (max + min);
            else
                return min + XORShift() % (max - min);
        }

        // Int32 / int

        // Generate a random signed 32-bit integer value in the range -2,147,483,648 (inclusive) to 2,147,483,647 (inclusive)
        public int NextInt()
        {
            return (int)XORShift();
        }

        public int NextIntMax(int max)
        {
            return NextInt() % max;
        }

        // Generate a random signed 32-bit integer value in the range min (inclusive) to max (exclusive)
        // If you only need to generate positive integers, use NextUIntRange instead
        public int NextIntRange(int min, int max)
        {
            // If max and min are the same, just return min since it will result in a DivideByZeroException
            if (max - min == 0) return min;

            // Do operations in Int64 to prevent overflow that might be caused by any of the following operations
            // I'm sure there's a faster/better way to do this and avoid casting, but we prefer equivalence to Unity over performance
            long minLong = (long)min;
            long maxLong = (long)max;
            long r = XORShift();

            // Flip the first operator if the max is lower than the min,
            if (max < min)
                return (int)(minLong - r % (maxLong - minLong));
            else
                return (int)(minLong + r % (maxLong - minLong));
        }

        // Single / float

        // Generate a random floating point between 0.0 and 1.0 (inclusive?)
        public float NextFloat()
        {
            return 1.0f - NextFloatRange(0.0f, 1.0f);
        }

        // Generate a random floating point between min (inclusive) and max (exclusive) 
        public float NextFloatRange(float min, float max)
        {
            return (min - max) * ((float)(XORShift() << 9) / 0xFFFFFFFF) + max;
        }
    }
}
