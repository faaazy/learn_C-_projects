import { useEffect, useState } from "react";
import "./App.css";
import { Login } from "./Login";
import { useAuth } from "./auth/AuthContext";
import { useApiFetch } from "./api/useApiFetch";

interface Todo {
  id: number;
  name: string;
  dueDate: string;
  isCompleted: boolean;
}

const API_URL = "http://localhost:5085/tasks";

function App() {
  const [tasks, setTasks] = useState<Todo[]>([]);
  const [name, setName] = useState("");
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingName, setEditingName] = useState("");

  const { isLogged } = useAuth();
  const apiFetch = useApiFetch();

  // first tasks
  useEffect(() => {
    if (!isLogged) {
      setTasks([]);
      return;
    }

    const getTasks = async () => {
      const res = await apiFetch(API_URL);

      if (res.ok) {
        const data = await res.json();
        setTasks(data);
      } else {
        console.error(res.status);
      }
    };

    getTasks();
  }, [isLogged]);

  // POST tasks
  const handleSubmit = async (e: React.SubmitEvent) => {
    e.preventDefault();

    const newTask: Todo = {
      id: 0,
      name,
      dueDate: new Date().toISOString(),
      isCompleted: false,
    };

    const res = await apiFetch(API_URL, {
      method: "POST",
      body: JSON.stringify(newTask),
    });

    if (res.ok) {
      const createdTask = await res.json();

      setTasks((prev) => [...prev, createdTask]);
      setName("");
    } else {
      console.error(res.statusText);
    }
  };

  // EDIT TASKS
  const handleEdit = (task: Todo) => {
    setEditingId(task.id);
    setEditingName(task.name);
  };

  const handleSaveEdit = async (task: Todo) => {
    const res = await apiFetch(`${API_URL}/${task.id}`, {
      method: "PUT",
      body: JSON.stringify({
        name: editingName,
        dueDate: task.dueDate,
        isCompleted: task.isCompleted,
      }),
    });

    if (res.ok) {
      const updatedTask = await res.json();

      setTasks((prev) => prev.map((t) => (t.id === task.id ? updatedTask : t)));
      setEditingId(null);
      setEditingName("");
    } else {
      console.error(res.statusText);
    }
  };

  const handleCancelEdit = () => {
    setEditingId(null);
    setEditingName("");
  };

  // DELETE TASKS
  const handleDelete = async (id: number) => {
    const res = await apiFetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });

    if (res.ok) {
      setTasks((prev) => prev.filter((t) => t.id !== id));
    } else {
      console.error(res.statusText);
    }
  };

  return (
    <div className="app">
      {!isLogged ? (
        <Login />
      ) : (
        <div className="todo-container">
          <h1 className="todo-title">My Tasks</h1>
          <form className="todo-form" onSubmit={handleSubmit}>
            <input
              className="todo-input"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="whats the new task..."
            />
            <button className="btn btn-add" type="submit">
              Add
            </button>
          </form>
          <ul className="todo-list">
            {tasks.map((task) => (
              <li className="todo-item" key={task.id}>
                {editingId === task.id ? (
                  <div className="todo-edit-row">
                    <input
                      className="todo-input"
                      value={editingName}
                      onChange={(e) => setEditingName(e.target.value)}
                    />
                    <button
                      className="btn btn-save"
                      onClick={() => handleSaveEdit(task)}
                    >
                      Save
                    </button>
                    <button
                      className="btn btn-cancel"
                      onClick={handleCancelEdit}
                    >
                      Cancel
                    </button>
                  </div>
                ) : (
                  <div className="todo-view-row">
                    <span className="todo-name">{task.name}</span>
                    <span
                      className={`todo-status ${task.isCompleted ? "done" : ""}`}
                    >
                      {task.isCompleted ? "completed" : "in progress"}
                    </span>
                    <div className="todo-actions">
                      <button
                        className="btn btn-edit"
                        onClick={() => handleEdit(task)}
                      >
                        Edit
                      </button>
                      <button
                        className="btn btn-delete"
                        onClick={() => handleDelete(task.id)}
                      >
                        Delete
                      </button>
                    </div>
                  </div>
                )}
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}

export default App;
