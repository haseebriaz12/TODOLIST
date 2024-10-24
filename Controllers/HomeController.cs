using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TODOAPP.Models;

namespace TODOAPP.Controllers
{
    public class HomeController : Controller
    {

        private static List<ToDoItem> toDoItems = new List<ToDoItem>();
        private static int currentId = 1;


        public IActionResult Index()
        {
            return View(toDoItems);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                toDoItem.Id = currentId++;
                toDoItems.Add(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
        }


        public IActionResult Edit(int id)
        {
            var item = toDoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        [HttpPost]
        public IActionResult Edit(ToDoItem toDoItem)
        {
            var existingItem = toDoItems.FirstOrDefault(t => t.Id == toDoItem.Id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Title = toDoItem.Title;
            existingItem.IsCompleted = toDoItem.IsCompleted;
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var item = toDoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = toDoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            toDoItems.Remove(item);
            return RedirectToAction(nameof(Index));
        }

    }
}