namespace plzwork.Services;

using plzwork.Models;

public class TasksService : ITasksService
{
  private readonly List<Todo> _tasks = [];
  private int _nextId = 1;

  public List<Todo> GetTasks()
    {
        return _tasks;
    }

    public Todo AddTask(Todo task)
    {
        var newTask = task with {Id = _nextId++};
        _tasks.Add(newTask);
        return newTask;
    }

    public Todo? GetTaskById(int id)
    {
        return _tasks.SingleOrDefault(task => task.Id == id);
    }

    public void DeleteTaskById(int id)
    {
        _tasks.RemoveAll(task => task.Id == id);
    }
};