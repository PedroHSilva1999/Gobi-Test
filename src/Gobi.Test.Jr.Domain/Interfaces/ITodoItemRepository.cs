namespace Gobi.Test.Jr.Domain.Interfaces
{
    public interface ITodoItemRepository
    {
        IEnumerable<TodoItem> GetAll();
		void Save(TodoItem item);
		void Edit(TodoItem item);
		void Delete(TodoItem item);
	}
}
