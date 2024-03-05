using hwFeb14__to_do_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hwFeb14__to_do_.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ToDoItems; Integrated Security=true;";

        public IActionResult Index()
        {
            var manager = new ToDoItemManager(_connectionString);
            return View(new ToDoItemsViewModel { categories = manager.GetCategories() });
        }

        public IActionResult NewItem()
        {
            var manager = new ToDoItemManager(_connectionString);
            return View(new ToDoItemsViewModel { categories = manager.GetCategories() });
        }

        public IActionResult AddItem(ToDoItem item)
        {
            var manager = new ToDoItemManager(_connectionString);
            manager.AddToDoItem(item);
            return Redirect("/home/index");
        }

        public IActionResult Categories()
        {
            var manager = new ToDoItemManager(_connectionString);
            return View(new ToDoItemsViewModel { categories = manager.GetCategories() });
        }

        public IActionResult EditCategory(int id)
        {
            var manager = new ToDoItemManager(_connectionString);
            var category = manager.GetCategories().FirstOrDefault(c => c.Id == id);
            return View(new EditCategoryViewModel { Category = category});
        }

        public IActionResult UpdateCategory(Category category)
        {
            var manager = new ToDoItemManager(_connectionString);
            manager.UpdateCategory(category);
            return Redirect("/home/categories");
        }

        public IActionResult ByCategory(int id)
        {
            var manager = new ToDoItemManager(_connectionString);
            var category = manager.GetCategories().FirstOrDefault(c => c.Id == id);
            return View(new EditCategoryViewModel { Category = category});
        }

        public IActionResult NewCategory()
        {
            return View();
        }

        public IActionResult AddCategory(string name)
        {
            var manager = new ToDoItemManager(_connectionString);
            manager.AddCategory(name);
            return Redirect("/home/categories");
        }

        public IActionResult MarkAsCompleted(int id)
        {
            var manager = new ToDoItemManager(_connectionString);
            manager.MarkAsCompleted(id);
            return Redirect("/home/completed");
        }

        public IActionResult Completed()
        {
            var manager = new ToDoItemManager(_connectionString);
            return View(new ToDoItemsViewModel { categories = manager.GetCategories()});
        }
    }
}
