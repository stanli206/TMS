using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskStatus = TaskManagementSystem.Domain.Entities.TaskStatus;

namespace TaskManagementSystem.Domain.Specifications
{
    public class TaskFilterSpec : BaseSpecification<TaskItem>
    {
        public TaskFilterSpec(TaskStatus? status)
        {
            Criteria = status == null
                ? t => true
                : t => t.Status == status;
        }
    }
}
