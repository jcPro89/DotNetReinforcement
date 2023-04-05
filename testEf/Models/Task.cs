namespace testEf.Models
{

    public class Task
    {
        public Guid TaskId { get; set; }
        public Guid CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Priority TaskPriority { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Category? Category { get; set; } // Will enable retrieving the associated category to get the remaining attributes if needed


    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
}