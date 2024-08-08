using Gobi.Test.Jr.Domain;
using Gobi.Test.Jr.Domain.Interfaces;

namespace Gobi.Test.Jr.Application
{
    public class TodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todoItemRepository.GetAll();
        }
		public void Save(TodoItem item)
        {
            _todoItemRepository.Save(item);

		}
    
		public void Edit(TodoItem item)
		{
			
			_todoItemRepository.Edit(item);

		}

		public void Delete(TodoItem item)
		{
			_todoItemRepository.Delete(item);
		}

	}
}