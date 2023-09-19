

namespace ToDo.Application.DTO
{
    public class GetStatus
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public decimal Percentage { get; set; }
    }
}
