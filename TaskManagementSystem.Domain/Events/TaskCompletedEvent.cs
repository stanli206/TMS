using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Events
{
    public class TaskCompletedEvent : IDomainEvent
    {
        public Guid TaskId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public TaskCompletedEvent(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
