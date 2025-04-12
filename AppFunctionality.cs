using System.IO;
using System.Runtime.CompilerServices;
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
            Console.ReadKey(); return;
        }
        _allTasks.Remove(task);
        SaveData(_allTasks);
        Console.WriteLine($"Task with ID {task.TaskID:000} deleted successfully! Press any key to continue");
        Console.ReadKey();
    }

    public void EditTask(int taskId, int editWhat, string edited)
    {
        var task = _allTasks.Find(x => x.TaskID == taskId);
        if (task is null)
        {
            Console.WriteLine($"Task with ID {taskId:000} not found. Press any button to continue");
            Console.ReadKey(); return;
        }
        switch (editWhat)
        {
            case 1:
                task.TaskTitle = edited;
                SaveData(_allTasks);
                Console.WriteLine($"Task {taskId:000} title edited successfully!. Press any button to continue.");
                Console.ReadKey(); break;
            case 2:
                task.ActualTask = edited;
                SaveData(_allTasks);
                Console.WriteLine("Task edited successfully!. Press any button to continue.");
                Console.ReadKey(); break;
            default: return;
        }
    }

    public void MarkCompleted(int taskID, int status)
    {
        var tasks = _allTasks.Find(x => x.TaskID == taskID);
        if (tasks is null)
        {
            Console.WriteLine($"Sorry could not find task with ID {taskID:000}. Press any key to continue");
            Console.ReadKey(); return;
        }
        
        switch(status)
        {
            case 1: tasks.TaskStatus = true; break;
            case 0: tasks.TaskStatus = false; break;
        }
        SaveData(_allTasks);
        Console.WriteLine($"Task with ID {taskID:000} marked as completed successfully! Click any button to continue!");
        Console.ReadKey();
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
        Console.WriteLine($"Task ID: {tasks.TaskID:000}\t\tTask Titile: {tasks.TaskTitle}\t\tTask: {tasks.ActualTask}\t\tDate Created: {tasks.TimeCreated.ToString("f")}\t\tTask Status: {(tasks.TaskStatus == false ? "Not Completed": "Completed")}");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
    public void DisplayAllTasks()
    {
        foreach (var task in _allTasks)
        {
            Console.WriteLine($"Task ID: {task.TaskID:000}\t\tTask Title: {task.TaskTitle}\t\tTask: {task.ActualTask}\t\tDate Created: {task.TimeCreated.ToString("f")}\t\tTask status: {(task.TaskStatus == false ? "Not completed" : "Completed")}");
            Console.WriteLine();
        }

        Console.WriteLine("Click any button to continue");
        Console.ReadKey();
    }
}