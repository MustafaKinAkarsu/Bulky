using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Create(Category obj)
		{
            //if(obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The Display Order cannot match the name");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); // Keeping track of what to add
                _db.SaveChanges(); // Saving the changes and adding to the database
				return RedirectToAction("Index"); // To return to the Index Action. (To see updated Categories)
			}
            return View();
		}
	}
}
