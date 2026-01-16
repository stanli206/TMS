using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Tasks.DTOs
{
    public record TaskDto(
    Guid Id,
    string Title,
    string Status,
    string Priority
);
}
