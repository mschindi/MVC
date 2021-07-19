using System.Collections.Generic;
using System.Linq;
using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        // GET
        private readonly ApplicationDbContext db;

        public ExpenseController(ApplicationDbContext db)
        {
            this.db = db;
        }
        // GET
        public IActionResult Index()
        {
            IEnumerable<Expense> objList = this.db.Expenses.ToList();

            foreach (var obj in objList)
            {
                obj.ExpenseType = db.ExpenseTypes.FirstOrDefault( u => u.Id == obj.ExpenseTypeId);
            }
            return View(objList);
        }
        
        // Get-Create
        public IActionResult Create()
        {
            // IEnumerable<SelectListItem> typeDropDown = db.ExpenseTypes.Select( i => new SelectListItem()
            // {
            //     Text = i.Name,
            //     Value = i.Id.ToString()
            // });
            //
            // ViewBag.TypeDropDown = typeDropDown;


            ExpenseVM expenseVm = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = db.ExpenseTypes.Select( i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
                
            };

            return View(expenseVm);
        }
        
        // Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        // Serverseitige Validation
        {
            if (ModelState.IsValid)
            {
                //obj.ExpenseTypeId = 1;
                this.db.Expenses.Add(obj.Expense);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }
        
        // GET Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = this.db.Expenses.Find(id);
            if (obj== null)
            {
                return NotFound();
            }

            return View(obj);
        }
        
        // POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = this.db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
            this.db.Expenses.Remove(obj);
            this.db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        // GET Update
        public IActionResult Update(int? id)
        {
            ExpenseVM expenseVm = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = db.ExpenseTypes.Select( i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
                
            };
            
            if (id == null || id == 0)
            {
                return NotFound();
            }

            expenseVm.Expense = this.db.Expenses.Find(id);
            if (expenseVm.Expense== null)
            {
                return NotFound();
            }

            return View(expenseVm);
        }
        
        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
            // Serverseitige Validation
        {
            if (ModelState.IsValid)
            {
                this.db.Expenses.Update(obj.Expense);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
    
}