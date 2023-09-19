using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Models;
using ToDo.Persistence.Data;

namespace ToDo.Persistence.Repositories
{
    public class ToDoItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ToDoItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ToDoItem>> GetAllToDoItems()
        {
            return await _dbContext.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem?> GetToDoItem(Guid Id)
        {
            return await _dbContext.ToDoItems.FindAsync(Id);
        }

        public async Task<ToDoItem> CreateToDoItem(ToDoItem toDoItem)
        {
            _dbContext.ToDoItems.Add(toDoItem);
            await _dbContext.SaveChangesAsync();
            return toDoItem;
        }

        public async Task UpdateToDoItem(ToDoItem toDoItem)
        {
            _dbContext.ToDoItems.Update(toDoItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteToDoItem(ToDoItem toDoItem)
        {
            _dbContext.ToDoItems.Remove(toDoItem);
            await _dbContext.SaveChangesAsync();
        }



    }
}
