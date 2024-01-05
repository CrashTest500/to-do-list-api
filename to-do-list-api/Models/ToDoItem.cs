namespace to_do_list_api.Models
{
    public class ToDoItem
    {
        public Guid Key { get; init; } = Guid.NewGuid();
        public string Description { get; set; }
        public bool IsComplete { get; set; }

        internal void Validate()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new Exception($"{nameof(Description)} must have a value");
            }
        }
    }
}
