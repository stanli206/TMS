using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskStatus = TaskManagementSystem.Domain.Entities.TaskStatus;
//using TaskStatus = TaskManagementSystem.Domain.Entities.TaskStatus;


namespace TaskManagementSystem.Domain.Specifications
{
    public class TaskByStatusSpec : Specification<TaskItem>
    {
        private readonly TaskStatus _status;

        public TaskByStatusSpec(TaskStatus status)
        {
            _status = status;
        }

        public override Expression<Func<TaskItem, bool>> Criteria
            => t => t.Status == _status;
    }
}
