using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace TaskManagementSystem.Domain.Specifications
{

    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
    }

}
