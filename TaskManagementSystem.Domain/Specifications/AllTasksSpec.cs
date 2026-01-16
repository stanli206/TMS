using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Specifications
{
    public class AllTasksSpec : Specification<TaskItem>
    {
        public override Expression<Func<TaskItem, bool>> Criteria
            => _ => true;
    }
}
