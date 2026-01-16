using System.Linq;
using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Application.Common;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Specifications;

namespace TaskManagementSystem.Application.Tasks.Queries;

public class GetTasksHandler
{
    private readonly ITaskRepository _repository;

    public GetTasksHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<TaskItem>> Handle(GetTasksQuery q)
    {
        Specification<TaskItem> spec;

        if (q.Status.HasValue)
        {
           
            spec = new TaskByStatusSpec(q.Status.Value);
        }
        else
        {
            spec = new AllTasksSpec();
        }

        var items = await _repository.ListAsync(spec);

        var total = items.Count;

        var pagedItems = items
            .Skip((q.Page - 1) * q.PageSize)
            .Take(q.PageSize)
            .ToList();

        return new PagedResult<TaskItem>
        {
            Items = pagedItems,
            TotalCount = total
        };
    }
}