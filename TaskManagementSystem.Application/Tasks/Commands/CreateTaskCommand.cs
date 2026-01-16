using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Tasks.Commands
{
    public record CreateTaskCommand(
    string Title,
    DateTime DueDate,
    TaskPriority Priority,
    Guid UserId
);
}
