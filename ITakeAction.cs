public interface ITakeAction
{
    void CreateTask(string taskTitle, string actualTask);
    void DeleteTask(int id);
    void EditTask(int id);
    void MarkCompleted(int id);
    void ViewATask(int id);
}