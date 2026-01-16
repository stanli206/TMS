using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Tasks.Commands
{
    public class CreateTaskHandler
    {
        private readonly ITaskRepository _repo;

        public CreateTaskHandler(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateTaskCommand cmd)
        {
            var task = new TaskItem(
                cmd.Title,
                cmd.DueDate,
                cmd.Priority,
                cmd.UserId
            );

            await _repo.AddAsync(task);
            await _repo.SaveAsync();
            return task.Id;
        }
    }
}
