using Chatty.Application.Common.Interfaces;
using System;

namespace Chatty.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
