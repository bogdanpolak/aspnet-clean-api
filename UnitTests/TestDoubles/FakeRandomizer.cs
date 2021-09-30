using System;
using CleanApi.Core.Contracts;

namespace UnitTests.TestDoubles
{
    public class FakeRandomizer : IRandomizer
    {
        public enum Strategy
        {
            GenMinimal, GenAverage, GenMaximal
        }
        
        public Strategy CurrentStrategy { get; set; }  = Strategy.GenAverage;

        public int GenInRange(int low, int high)
        {
            return CurrentStrategy switch
            {
                Strategy.GenMinimal => low,
                Strategy.GenAverage => (low + high) / 2,
                Strategy.GenMaximal => (low == high) ? low : high - 1,
                _ => throw new ArgumentOutOfRangeException(nameof(CurrentStrategy))
            };
        }

        public int GenInt(int maxValue) => GenInRange(0, maxValue);
    }
}