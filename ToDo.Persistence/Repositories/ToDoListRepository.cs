using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Models;
using ToDo.Persistence.Data;

namespace ToDo.Persistence.Repositories
{
    public class ToDoListRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ToDoListRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ToDoList>> GetAllToDoLists()
        {
            return await _dbContext.ToDoLists.ToListAsync();
        }

        public async Task<ToDoList?> GetToDoList(Guid Id)
        {
            return await _dbContext.ToDoLists.FindAsync(Id);
        }

        public async Task<List<ToDoItem>> GetItemsOfToDoList(Guid Id)
        {
            return await _dbContext.ToDoItems.Where(p => p.ToDoId == Id).ToListAsync();
        }

        public async Task<bool> titleCheck(string Title)
        {
            return await _dbContext.ToDoLists.FirstOrDefaultAsync(p => p.Title == Title) != null;           
        }

        public async Task<ToDoList> CreateToDoList(ToDoList toDoList)
        {
            _dbContext.ToDoLists.Add(toDoList);
            await _dbContext.SaveChangesAsync();
            return toDoList;
        }

        public async Task UpdateToDoList(ToDoList toDoList)
        {
            _dbContext.ToDoLists.Update(toDoList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteToDoList(ToDoList toDoList)
        {
            _dbContext.ToDoLists.Remove(toDoList);
            await _dbContext.SaveChangesAsync();
        }

    }
}
