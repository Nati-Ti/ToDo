
namespace ToDo.Application.DTO
{
    public class UpdateItemProperty
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ToDoId { get; set; }
    }
}
