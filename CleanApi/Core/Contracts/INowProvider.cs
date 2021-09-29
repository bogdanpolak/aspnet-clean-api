using System;

namespace CleanApi.Core.Contracts
{
    public interface INowProvider
    {
        DateTime GetTodayMidDay();
    }
}