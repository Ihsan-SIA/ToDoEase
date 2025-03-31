﻿using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

public class AppFunctionality : ITakeAction
{
    private readonly List<TaskIcons> _allTasks = LoadData();
    public void CreateTask(string taskTitle, string actualTask)
    {
        var task = new TaskIcons
        {
            TaskTitle = taskTitle,
            ActualTask = actualTask,
            TimeCreated = DateTime.Now,
        };

        int emptyID = 1;
        while (true)
        {
            var checkID = _allTasks.Find(x => x.TaskID == emptyID);
            if (checkID is null)
            {
                task.TaskID = emptyID;
                break;
            }
            
            emptyID++;

            if (emptyID >= _allTasks.Count)
            {
                task.TaskID = _allTasks.Count + 1;
                break;
            }
        }

        _allTasks.Add(task);
        SaveData(_allTasks);
        Console.WriteLine($"Task with ID {task.TaskID:000} created successfully! Click any button to continue");
        Console.ReadKey();
        Console.WriteLine();
    }
    private static List<TaskIcons> LoadData()
    {
        string path = @"C:\Users\ihsan\Desktop\TodoEaseData\TodoEaseData.txt";
        if (File.Exists(path))
        {
            string taskData = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<TaskIcons>>(taskData) ?? [];
        }

        return [];
    }
    private void SaveData(List<TaskIcons> taskData)
    {
        string path = @"C:\Users\ihsan\Desktop\TodoEaseData\TodoEaseData.txt";
        string serialEase = JsonSerializer.Serialize(taskData, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
        File.WriteAllText(path, serialEase);
    }
    
    public void DeleteTask(int taskId)
    {
        var task = _allTasks.Find(x => x.TaskID == taskId);
        if (task is null)
        {
            Console.WriteLine($"Unable to find task {taskId:000}. Kindly confirm if the task still exist! Press any key to continue");
            Console.ReadKey();
            return;
        }
        _allTasks.Remove(task);
        SaveData(_allTasks);
        Console.WriteLine($"Task with ID {task.TaskID:000} deleted successfully! Press any key to continue");
        Console.ReadKey();
    }

    public void EditTask(int id)
    {

    }

    public void MarkCompleted(int id)
    {

    }

    public void ViewATask(int taskID)
    {
        var tasks = _allTasks.Find(x => x.TaskID == taskID);
        if (tasks is null)
        {
            Console.WriteLine($"{taskID:000} does not exist. Click any button to continue");
            Console.ReadKey();
            return;
        }
        Console.WriteLine($"Task ID: {tasks.TaskID:000}\tTask: {tasks.ActualTask}\tDate Created: {tasks.TimeCreated.ToLongDateString()}\tTask Status: {tasks.TaskStatus}");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();

    }
}