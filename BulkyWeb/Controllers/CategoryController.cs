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
			//Category? categoryFromDb = _db.Categories.FirstOrDefault(u=> u.Name =="fdfsdfs") or Category? categoryFromDb = _db.Categories.FirstOrDefault(u => u.Name.Contains("fasdfa");
			//Advantage of this is it does not require primary key unlike Find
			//Category? categoryFromDb = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
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
				return RedirectToAction("Index"); // To return to the Index Action. (To see updated Categories)
			}
			return View();
		}
	}
}
