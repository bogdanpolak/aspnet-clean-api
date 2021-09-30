using System;
using CleanApi.Core.Contracts;

namespace UnitTests.TestDoubles
{
    public class FakeNowProvider : INowProvider
    {
        public DateTime Now; 

        public FakeNowProvider()
        {
            Now = DateTime.Now;
        }

        public DateTime GetTodayMidDay()
        {
            return Now.Date.AddHours(12);
        }
    }
}