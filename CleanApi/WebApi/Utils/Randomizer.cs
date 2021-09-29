using System;
using CleanApi.Core.Contracts;

namespace CleanApi.WebApi.Utils
{
    public class Randomizer : IRandomizer
    {
        private readonly Random _rnd;
        
        public Randomizer()
        {
            _rnd = new Random();
        }

        public int GenInRange(int low, int high) => _rnd.Next(low,high);
        public int GenInt(int maxValue) => _rnd.Next(maxValue);
    }
}