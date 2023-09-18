namespace _Net.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public required ImageInfo Img { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}