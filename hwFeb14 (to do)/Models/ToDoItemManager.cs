using System.Data.SqlClient;

namespace hwFeb14__to_do_.Models
{
    public class ToDoItemManager
    {
        private readonly string _connectionString;
        public ToDoItemManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Category> GetCategories()
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Category C\r\nleft JOIN ToDoItem T\r\nON C.Id = T.CategoryId";
            connection.Open();
            var categories = new List<Category>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var category = categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    category = new()
                    {
                        Id = id,
                        Name = (string)reader["Name"],
                    };
                    categories.Add(category);
                }
                if (reader.GetOrNull<string>("Title") != null)
                {
                    category.toDoItems.Add(new()
                    {
                        CategoryId = id,
                        DueDate = (DateTime)reader["DueDate"],
                        Title = (string)reader["Title"],
                        CompletedDate = reader.GetOrNull<DateTime?>("CompletedDate"),
                        Id = (int)reader["ItemId"]
                    });
                }
            }
            return categories;
        }

        public void AddToDoItem(ToDoItem item)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"insert into ToDoItem (Title, duedate, categoryid)
                                    values (@title, @dueDate, @catId)";
            command.Parameters.AddWithValue("@title", item.Title);
            command.Parameters.AddWithValue("@dueDate", item.DueDate);
            command.Parameters.AddWithValue("@catId", item.CategoryId);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void AddCategory(string name)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"insert into Category (Name)
                                    values (@name)";
            command.Parameters.AddWithValue("@name", name);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void UpdateCategory(Category category)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Category SET Name = @name WHERE id = @id";
            command.Parameters.AddWithValue("@id", category.Id);
            command.Parameters.AddWithValue("@name", category.Name);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void MarkAsCompleted(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "update ToDoItem set CompletedDate = GETDATE() WHERE ItemId = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
