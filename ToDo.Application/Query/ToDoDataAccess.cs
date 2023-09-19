using Dapper;
using ToDo.Domain.Models;
using ToDo.Application.DTO;
using ToDo.Persistence.Data;

namespace ToDo.Application.Query
{
    public class ToDoDataAccess
    {
        private readonly DapperContext _context;
        public ToDoDataAccess(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoList>> GetAllToDoLists()
        {
            var query = "SELECT * FROM ToDoLists";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QueryAsync<ToDoList>(query);

                return toDos;
            }
        }

        public async Task<IEnumerable<CreateToDoList>> GetListProperty()
        {
            var query = "SELECT Id, Title, Value FROM ToDoLists";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QueryAsync<CreateToDoList>(query);

                return toDos;
            }
        }

        public async Task<IEnumerable<GetStatus>> GetStatusOfList()
        {
            var query = "SELECT Id, Title, Value, Percentage FROM ToDoLists";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QueryAsync<GetStatus>(query);

                return toDos;
            }
        }

        //Get by Id

        public async Task<ToDoList> GetToDoListById(Guid Id)
        {
            var query = "SELECT * FROM ToDoLists WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QuerySingleOrDefaultAsync<ToDoList>(query, new { Id = Id });

                return toDos;
            }
        }

        

        public async Task<GetStatus> GetStatusOfListById(Guid Id)
        {
            var query = "SELECT Id, Title, Value, Percentage FROM ToDoLists WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QuerySingleOrDefaultAsync<GetStatus>(query, new { Id = Id });

                return toDos;
            }
        }



        //Get items of ToDo List



        public async Task<IEnumerable<ToDoItem>> GetAllToDoItems()
        {
            var query = "SELECT * FROM ToDoItems";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QueryAsync<ToDoItem>(query);

                return toDos;
            }
        }

        public async Task<IEnumerable<GetStatus>> GetStatusOfItems()
        {
            var query = "SELECT Id, Title, Value, Percentage FROM ToDoItems";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QueryAsync<GetStatus>(query);

                return toDos;
            }
        }

        public async Task<IEnumerable<CreateToDoList>> GetItemProperty()
        {
            var query = "SELECT Id, Title, Value FROM ToDoItems";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QueryAsync<CreateToDoList>(query);

                return toDos;
            }
        }


        //Get by Id

        public async Task<ToDoItem> GetToDoItemById(Guid Id)
        {
            var query = "SELECT * FROM ToDoItems WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QuerySingleOrDefaultAsync<ToDoItem>(query, new { Id = Id });

                return toDos;
            }
        }



        public async Task<GetItemStatus> GetItemPropertyById(Guid Id)
        {
            var query = "SELECT Id, Title, Value, Progress, Percentage FROM ToDoItems WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QuerySingleOrDefaultAsync<GetItemStatus>(query, new { Id = Id });

                return toDos;
            }
        }

        public async Task<IEnumerable<GetItemStatus>> GetItemsOfToDoListById(Guid Id)
        {
            var query = "SELECT Id, Title, Value, Progress, Percentage FROM ToDoItems WHERE ToDoId = @ToDoId";

            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QueryAsync<GetItemStatus>(query, new { ToDoId = Id });

                return toDos;
            }
        }






        //public async Task<GetProperty> GetListPropertyById(Guid Id)
        //{
        //    var query = "SELECT Id, Title, Value FROM ToDoLists WHERE Id = @Id";

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var toDos = await connection.QuerySingleOrDefaultAsync<GetProperty>(query, new { Id = Id });

        //        return toDos;
        //    }
        //}
        ////public async Task<GetStatus> GetStatusOfItemById(Guid Id)
        //{
        //    var query = "SELECT Id, Title, Value, Percentage FROM ToDoItems WHERE Id = @Id";

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var toDos = await connection.QuerySingleOrDefaultAsync<GetStatus>(query, new { Id = Id });

        //        return toDos;
        //    }
        //}
        //public async Task<IEnumerable<ToDoItem>> GetItemsOfToDoList(Guid Id)
        //{
        //    var query = "SELECT * FROM ToDoItems WHERE ToDoId = @Id";

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var items = await connection.QueryAsync<ToDoItem>(query, new { ToDoId = Id });

        //        return items;
        //    }
        //}

        //public async Task<IEnumerable<GetProperty>> GetItemsPropertyOfToDoList(Guid Id)
        //{
        //    var query = "SELECT Id, Title, Value FROM ToDoItems WHERE ToDoId = @Id";

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var items = await connection.QueryAsync<GetProperty>(query, new { ToDoId = Id });

        //        return items;
        //    }
        //}


    }
}