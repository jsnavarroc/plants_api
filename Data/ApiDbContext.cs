using Microsoft.EntityFrameworkCore;
using _Net.Models;

namespace _Net.Data
{
    public class ApiDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TodoDB");
        }

        public ApiDBContext(DbContextOptions<ApiDBContext> options)
           : base(options)
        {
            TodoItems = Set<TodoItem>();
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
