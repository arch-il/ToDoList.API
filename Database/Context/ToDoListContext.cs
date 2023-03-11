using Microsoft.EntityFrameworkCore;
using ToDoList.API.Database.Entities;

namespace ToDoList.API.Database.Context
{
    public sealed class ToDoListContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        public ToDoListContext(DbContextOptions<ToDoListContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
