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
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); // Keeping track of what to add
                _db.SaveChanges(); // Saving the changes and adding to the database
				TempData["Success"] = "Category created successfully"; // To add TempData that notifies the user on create.
				return RedirectToAction("Index"); // To return to the Index Action. (To see updated Categories)
			}
            return View();
		}
		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
			if (categoryFromDb == null) NotFound();
			return View(categoryFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{
		
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj); // Keeping track of what to Update
				_db.SaveChanges(); // Saving the changes and adding to the database
				TempData["Success"] = "Category updated successfully";
				return RedirectToAction("Index"); // To return to the Index Action. (To see updated Categories)
			}
			return View();
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.Find(id);
			if (categoryFromDb == null) NotFound();
			return View(categoryFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Category? obj = _db.Categories.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.Categories.Remove(obj); 
			_db.SaveChanges();
			TempData["Success"] = "Category deleted successfully";
			return RedirectToAction("Index"); 
		}
	}
}
