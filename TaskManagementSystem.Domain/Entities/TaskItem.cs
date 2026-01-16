using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Events;
using TaskManagementSystem.Domain.Exceptions;


namespace TaskManagementSystem.Domain.Entities
{
    public enum TaskStatus
    {
        New,
        InProgress,
        Completed,
        Archived
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        Critical
    }

    public class TaskItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; }
        public DateTime DueDate { get; private set; }
        public TaskStatus Status { get; private set; } = TaskStatus.New;
        public TaskPriority Priority { get; private set; }
        public bool IsDeleted { get; private set; }
        public Guid OwnerId { get; private set; }

        [Timestamp]
        public byte[]? RowVersion { get; private set; }

        private TaskItem() { }

        public TaskItem(string title, DateTime dueDate, TaskPriority priority, Guid ownerId)
        {
            Title = title;
            DueDate = dueDate;
            Priority = priority;
            OwnerId = ownerId;
        }

        public List<IDomainEvent> DomainEvents { get; } = new();

        public void Complete()
        {
            if (Priority == TaskPriority.Critical && DueDate < DateTime.UtcNow)
                throw new DomainException("Overdue critical task cannot be completed");

            if (Status == TaskStatus.Archived)
                throw new DomainException("Archived task is read-only");

            Status = TaskStatus.Completed;
            DomainEvents.Add(new TaskCompletedEvent(Id));
        }

        public void Archive()
        {
            Status = TaskStatus.Archived;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
        }

        public void AdminOverrideComplete()
        {
            Status = TaskStatus.Completed;
        }

    }
}

