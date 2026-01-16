using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> Apply<T>(
            IQueryable<T> query,
            ISpecification<T> spec)
        {
            return query.Where(spec.Criteria);
        }
    }
}
