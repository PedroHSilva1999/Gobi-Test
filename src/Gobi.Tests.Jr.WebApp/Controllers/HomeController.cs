
using Gobi.Test.Jr.Application;
using Gobi.Test.Jr.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Gobi.Tests.Jr.WebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoItemService _todoItemService;

        public TodoController(TodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public IActionResult Index()
        {
            var items = _todoItemService.GetAll();

            return View(items);
        }

        public IActionResult Add()
        {
			return View();
		}
        [HttpPost]
		public IActionResult Add(TodoItem TodoItem)
		{
			if(ModelState.IsValid)
            {
                _todoItemService.Save(TodoItem); 
			}

			return RedirectToAction("Index");  
		}

        public IActionResult Edit(int id)
        {
			var item = _todoItemService.GetAll().FirstOrDefault(x => x.Id == id);
			if (item == null)
			{
				return NotFound();
			}

			return View(item);
		}
        [HttpPost]
		public IActionResult Edit(TodoItem TodoItem)
		{
			if (ModelState.IsValid)
			{
				_todoItemService.Edit(TodoItem);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var item = _todoItemService.GetAll().FirstOrDefault(x => x.Id == id);
			if (item == null)
			{
				return NotFound();
			}

			_todoItemService.Delete(item);
			return RedirectToAction("Index");
		}
	}
}