public class TaskIcons
{
    public string ActualTask { get; set; } = default!;
    public string TaskTitle { get; set; } = default!;
    public int TaskID {  get; set; }
    public DateTime TimeCreated { get; set; }
    public readonly List<int> CompletedTasks = new List<int>();
    public string TaskStatus { get; set; } = "Not completed";
    //public TaskIcons()
    //{
    //    int taskID = 0;
    //    TaskID = taskID;
    //}
}