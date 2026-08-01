namespace plzwork.Services;

using plzwork.Models;

public class TasksService : ITasksService
{
  private readonly List<Todo> _tasks = [];

  public List<Todo> GetTasks()
    {
        return _tasks;
    }

    public Todo AddTask(Todo task)
    {
        _tasks.Add(task);
        return task;
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