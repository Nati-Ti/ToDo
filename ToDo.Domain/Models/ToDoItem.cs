
namespace ToDo.Domain.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public decimal Progress { get; set; }
        public decimal Percentage { get; set; }
        public Guid ToDoId { get; set; }
        public ToDoList ToDo { get; set; }


    }
}
