using to_do_list_api.Models;

namespace to_do_list_api.Data
{
    public class ToDoRepository : IToDoRepository
    {
        private static readonly List<ToDoItem> _toDoList = new();

        public void AddToDoItem(ToDoItem newItem)
        {
            _toDoList.Add(newItem);
        }

        public void CompleteToDoItem(Guid id)
        {
            throw new NotImplementedException();
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
