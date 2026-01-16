using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Specifications;

namespace TaskManagementSystem.Application.Abstractions
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem task);
        Task SaveAsync();
        Task<List<TaskItem>> ListAsync(Specification<TaskItem> spec);

    }
}