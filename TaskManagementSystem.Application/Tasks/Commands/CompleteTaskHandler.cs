using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Tasks.Commands
{
    public class CompleteTaskHandler
    {
        private readonly ITaskRepository _repo;

        public CompleteTaskHandler(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(Guid taskId, Guid userId, bool isAdmin)
        {
            var task = await _repo.GetByIdAsync(taskId)
                ?? throw new Exception("Task not found");

            if (!isAdmin && task.OwnerId != userId)
                throw new Exception("Forbidden");

            if (isAdmin)
                task.AdminOverrideComplete();
            else
                task.Complete();

            await _repo.SaveAsync();
        }
    }

}
