

namespace ToDo.Domain.Models
{
    public class ToDoList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public decimal Percentage { get; set; }
        public List<ToDoItem> Items { get; set; }

    }
}
