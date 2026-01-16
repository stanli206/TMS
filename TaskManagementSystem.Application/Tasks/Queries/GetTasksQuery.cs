using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainTaskStatus = TaskManagementSystem.Domain.Entities.TaskStatus;



namespace TaskManagementSystem.Application.Tasks.Queries
{
    public class GetTasksQuery
    {
        public DomainTaskStatus? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
