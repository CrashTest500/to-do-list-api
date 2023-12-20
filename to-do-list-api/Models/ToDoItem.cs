namespace to_do_list_api.Models
{
    public enum Status
    {
        Incomplete,
        Complete
    }

    public class ToDoItem
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Description { get; set; }
        public Status Status { get; set; }

        internal void Validate()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new Exception($"{nameof(Description)} must have a value");
            }


        }
    }
}
