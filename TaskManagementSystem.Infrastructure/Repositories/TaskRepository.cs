using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Specifications;
using TaskManagementSystem.Infrastructure.Persistence;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
            => await _db.Tasks.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(TaskItem task)
            => await _db.Tasks.AddAsync(task);

        public async Task SaveAsync()
            => await _db.SaveChangesAsync();

        public async Task<List<TaskItem>> ListAsync(Specification<TaskItem> spec)
        {
            return await _db.Tasks
                .Where(spec.Criteria)
                .ToListAsync();
        }
    }
}
