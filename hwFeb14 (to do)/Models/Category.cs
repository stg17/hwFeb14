namespace hwFeb14__to_do_.Models
{
    public class Category
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<ToDoItem> toDoItems { get; set; } = new();
    }

    public class ToDoItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
