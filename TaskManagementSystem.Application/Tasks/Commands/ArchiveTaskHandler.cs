using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.Application.Tasks.Commands
{
    public class ArchiveTaskHandler
    {
        private readonly ITaskRepository _repo;

        public ArchiveTaskHandler(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(Guid taskId)
        {
            var task = await _repo.GetByIdAsync(taskId)
                ?? throw new DomainException("Task not found");

            task.Archive();
            await _repo.SaveAsync();
        }
    }
}
