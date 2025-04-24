using System.Text.Json; // For JSON serialization/deserialization
using System.Text.Json.Serialization; // Optional, for fine-tuning JSON behavior

// File to store the task list
string filePath = "tasks.json";

// Create the JSON file if it doesn't exist, and initialize with an empty list
if (!File.Exists(filePath))
{
    File.WriteAllText(filePath, "[]");
}

// Deserialize the JSON file to a list of tasks, or use an empty list if it fails
List<TaskItem> tasks = JsonSerializer.Deserialize<List<TaskItem>>(File.ReadAllText(filePath)) ?? new();

if (args.Length == 0)
{
    Console.WriteLine("Please provide a command. Example: add \"Your task description\"");
    return;
}

// Extract command and arguments
string command = args[0].ToLower();
string[] commandArgs = args.Skip(1).ToArray();

// Command handler
switch (command)
{
    case "add":
        if (commandArgs.Length < 1)
        {
            Console.WriteLine("Please provide a task description.");
            return;
        }
        AddTask(commandArgs);
        break;

    case "update":
        if (commandArgs.Length < 2 || !int.TryParse(commandArgs[0], out int updateId))
        {
            Console.WriteLine("Usage: update <id> \"new description\"");
            return;
        }
        UpdateTask(updateId, string.Join(" ", commandArgs.Skip(1)));
        break;

    case "delete":
        if (commandArgs.Length < 1 || !int.TryParse(commandArgs[0], out int deleteId))
        {
            Console.WriteLine("Usage: delete <id>");
            return;
        }
        DeleteTask(deleteId);
        break;

    case "list":
        string? statusFilter = commandArgs.Length > 0 ? commandArgs[0].ToLower() : null;
        ListTasks(statusFilter);
        break;

    case "mark-in-progress":
    case "mark-done":
    case "mark-todo":
        if (commandArgs.Length < 1 || !int.TryParse(commandArgs[0], out int markId))
        {
            Console.WriteLine($"Usage: {command} <id>");
            return;
        }
        string newStatus = command.Substring(5); // Extract "in-progress", "done", etc.
        UpdateTaskStatus(markId, newStatus);
        break;

    default:
        Console.WriteLine("Unknown command.");
        break;
}

// --- Methods ---

void AddTask(string[] descriptionParts)
{
    string description = string.Join(" ", descriptionParts);
    int nextId = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;

    var newTask = new TaskItem
    {
        Id = nextId,
        Description = description,
        Status = "todo",
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };

    tasks.Add(newTask);
    SaveTasks();
    Console.WriteLine($"Task added successfully (ID: {newTask.Id})");
}

void UpdateTask(int id, string newDescription)
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task == null)
    {
        Console.WriteLine("Task not found.");
        return;
    }

    task.Description = newDescription;
    task.UpdatedAt = DateTime.Now;
    SaveTasks();
    Console.WriteLine($"Task updated successfully (ID: {task.Id})");
}

void DeleteTask(int id)
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task == null)
    {
        Console.WriteLine("Task not found.");
        return;
    }

    tasks.Remove(task);

    // Reassign IDs to keep them sequential
    for (int i = 0; i < tasks.Count; i++)
    {
        tasks[i].Id = i + 1;
    }

    SaveTasks();
    Console.WriteLine($"Task deleted successfully (ID: {id})");
}

void ListTasks(string? filter)
{
    var filteredTasks = string.IsNullOrEmpty(filter)
        ? tasks
        : tasks.Where(t => t.Status.Equals(filter, StringComparison.OrdinalIgnoreCase)).ToList();

    if (filteredTasks.Count == 0)
    {
        Console.WriteLine("No matching tasks found.");
        return;
    }

    Console.WriteLine("ID || Description || Status");
    foreach (var task in filteredTasks)
    {
        Console.WriteLine($"{task.Id} || {task.Description} || {task.Status}");
    }
}

void UpdateTaskStatus(int id, string status)
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task == null)
    {
        Console.WriteLine("Task not found.");
        return;
    }

    task.Status = status;
    task.UpdatedAt = DateTime.Now;
    SaveTasks();
    Console.WriteLine($"Task status updated to '{status}' (ID: {task.Id})");
}

void SaveTasks()
{
    File.WriteAllText(filePath, JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));
}

// --- Task Model ---

public class TaskItem
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public string Status { get; set; } = "todo";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
