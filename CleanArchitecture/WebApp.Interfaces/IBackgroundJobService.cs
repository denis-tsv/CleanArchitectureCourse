using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Interfaces
{
    public interface IBackgroundJobService
    {
        void Schedule<T>(Expression<Func<T, Task>> expression);
    }
}
