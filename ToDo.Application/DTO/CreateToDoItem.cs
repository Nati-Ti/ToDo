
namespace ToDo.Application.DTO
{
    public class CreateToDoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ToDoId { get; set; }
        public int Value { get; set; }

    }
}
