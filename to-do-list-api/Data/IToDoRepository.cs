using to_do_list_api.Models;

namespace to_do_list_api.Data
{
    public interface IToDoRepository
    {
        public ToDoItem? GetToDoItem(Guid id);
        public List<ToDoItem> GetToDoItems();
        public void AddToDoItem(ToDoItem newItem);
        public void CompleteToDoItem(Guid id);
    }
}
