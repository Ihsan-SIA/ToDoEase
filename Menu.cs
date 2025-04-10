public class Menu
{
    private readonly ITakeAction _takeAction = new AppFunctionality();
    public void MainMenu()
    {
        bool openApp = true;
        while (openApp)
        {
            Console.Clear();
            Display();
            int.TryParse(Console.ReadLine(), out int select);
            switch (select)
            {
                case 0: openApp = false; break;
                case 1:
                    Console.Write("Enter your task title: ");
                    string taskTitle = Console.ReadLine()!;
                    Console.Write("Enter your task : ");
                    string task = Console.ReadLine()!;
                    _takeAction.CreateTask(taskTitle, task); break;
                case 2:
                    Console.Write("Enter the ID of the task to delete: ");
                    int.TryParse(Console.ReadLine()!, out int taskID);
                    _takeAction.DeleteTask(taskID); break;
                case 3:
                    Console.Write("Enter the ID of the task you want to edit: ");
                    int.TryParse(Console.ReadLine()!, out taskID);
                    Console.Write("Enter 1 if you want to edit the title, or 2 if you want to edit the task itself: ");
                    int.TryParse(Console.ReadLine()!, out int editWhat);
                    Console.Write("Edit here: ");
                    string edited = Console.ReadLine()!;
                    _takeAction.EditTask(taskID, editWhat, edited); break;
                case 5:
                    Console.Write("Enter the task ID to view: ");
                    int.TryParse(Console.ReadLine()!, out int taskId);
                    _takeAction.ViewATask(taskId); break;
            }
        }
    }
    private void Display()
    {
        Console.WriteLine("Click 1 to create new task");
        Console.WriteLine("Click 2 to delete existing task");
        Console.WriteLine("Click 3 to edit existing task");
        Console.WriteLine("Click 4 to mark a task as completed");
        Console.WriteLine("Click 5 to view a task details");
        Console.WriteLine("Click 0 to exit app");
    }
}