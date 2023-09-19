
namespace ToDo.Application.DTO
{
    public class GetItemStatus
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public decimal Progress { get; set; }
        public decimal Percentage { get; set; }
    }
}
