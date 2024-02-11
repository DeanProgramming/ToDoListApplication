namespace DeanH_Task_Management_App
{
    public class ToDoItem
    { 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; } = false;
        public DateOnly DeadLine { get; set; }
        public int Priority { get; set; }
    }
}
