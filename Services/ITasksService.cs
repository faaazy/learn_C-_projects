namespace plzwork.Services;

using plzwork.Models;

public interface ITasksService
{
    List<Todo> GetTasks();

    Todo AddTask(Todo Task);

    Todo? GetTaskById(int id);

    void DeleteTaskById(int id);
};