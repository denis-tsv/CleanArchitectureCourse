using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public void Schedule<T>(Expression<Func<T, Task>> expression)
        {
            BackgroundJob.Schedule(expression, TimeSpan.Zero);
        }
    }
}
