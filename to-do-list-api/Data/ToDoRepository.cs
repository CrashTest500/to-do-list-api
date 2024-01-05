using to_do_list_api.Models;

namespace to_do_list_api.Data
{
    public class ToDoRepository : IToDoRepository
    {
        private static readonly List<ToDoItem> _toDoList = new();

        public ToDoItem AddToDoItem(ToDoItem newItem)
        {
            _toDoList.Add(newItem);

            return newItem;
        }

        public ToDoItem ToggleItem(Guid id)
        {
            ToDoItem itemForUpdate = GetToDoItem(id);

            _toDoList.FirstOrDefault(t => t.Key == itemForUpdate.Key).IsComplete = !itemForUpdate.IsComplete;

            return _toDoList.FirstOrDefault(t => t.Key == itemForUpdate.Key);
        }

        public ToDoItem? GetToDoItem(Guid id)
        {
            return _toDoList.FirstOrDefault(toDo => toDo.Key == id);
        }

        public List<ToDoItem> GetToDoItems()
        {
            return _toDoList;
        }
    }
}
