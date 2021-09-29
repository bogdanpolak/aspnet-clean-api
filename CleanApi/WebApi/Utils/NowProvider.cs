using System;
using CleanApi.Core.Contracts;

namespace CleanApi.WebApi.Utils
{
    public class NowProvider : INowProvider
    {
        public DateTime GetTodayMidDay()
        {
            return DateTime.Now.Date.AddHours(12);
        }
    }
}