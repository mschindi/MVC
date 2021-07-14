using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET
        public IActionResult Index()
        {
            IEnumerable<Item> objList = _db.Items.ToList();
            return View(objList);
        }
        
        
        // Get-Create
        public IActionResult Create()
        {
            return View();
        }
        
        // Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item obj)
        {
            _db.Items.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}