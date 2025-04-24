# 🧰 Task Tracker CLI (.NET Console App)

A simple, beginner-friendly Task Tracker Command Line Tool built in C# with .NET. You can **add**, **update**, **delete**, **list**, and **change task statuses** (todo, in-progress, done) — all from your terminal!

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download) (or .NET 6+)
- Git (optional, to clone the repository)

---

### 📦 Clone the Repository

```bash
git clone https://github.com/your-username/task-tracker-cli.git
cd task-tracker-cli
```

---

### 🏃 Run the App

```bash
dotnet run -- <command> [arguments]
```

> The `--` is important. It separates `dotnet` args from your command args.

Example:
```bash
dotnet run -- add "Finish the .NET project"
```

---

## 🧭 Available Commands

| Command | Example | Description |
|--------|---------|-------------|
| `add` | `add "My new task"` | Adds a new task |
| `update` | `update 2 "New description"` | Updates the task with ID 2 |
| `delete` | `delete 3` | Deletes the task with ID 3 |
| `list` | `list` or `list done` | Lists all tasks or filters by status |
| `mark-todo` | `mark-todo 1` | Changes status of task to `todo` |
| `mark-in-progress` | `mark-in-progress 1` | Changes status to `in-progress` |
| `mark-done` | `mark-done 1` | Changes status to `done` |

---

## 💾 How It Works

- All tasks are stored in a local `tasks.json` file.
- The file is automatically created the first time you run the app.
- You can open `tasks.json` to view the raw data.

Example `tasks.json` content:
```json
[
  {
    "Id": 1,
    "Description": "Write documentation",
    "Status": "todo",
    "CreatedAt": "2025-04-24T10:15:00",
    "UpdatedAt": "2025-04-24T10:15:00"
  }
]
```

---

## 📁 Project Structure

```text
📂 task-tracker-cli
├── Program.cs          // Main logic and CLI entry point
├── tasks.json          // Local data store
├── README.md           // You're reading it now!
```

---

## 🤝 Contributing

Pull requests are welcome! If you'd like to contribute:

1. Fork this repo
2. Create a new branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -m 'Add feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Open a Pull Request

---

## 📃 License

This project is licensed under the [MIT License](LICENSE).

---

## 🙌 Acknowledgments

This project was created as a learning tool for improving .NET and C# CLI development skills.

---
