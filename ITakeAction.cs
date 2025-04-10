﻿public interface ITakeAction
{
    void CreateTask(string taskTitle, string actualTask);
    void DeleteTask(int id);
    void EditTask(int taskId, int editWhat, string edited);
    void MarkCompleted(int id);
    void ViewATask(int id);
}