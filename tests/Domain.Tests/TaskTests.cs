using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using Xunit;

namespace tests.Domain.Tests
{
    public class TaskTests
    {
        [Fact]
        public void OverdueCriticalTask_CannotComplete()
        {
            var task = new TaskItem(
                "Test",
                DateTime.UtcNow.AddDays(-1),
                TaskPriority.Critical,
                Guid.NewGuid()
            );

            Assert.Throws<Exception>(() => task.Complete());
        }
    }
}
