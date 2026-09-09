import { useEffect, useState } from "react";
import "./App.css";
import { Login } from "./Login";
import { useAuth } from "./auth/AuthContext";
import { useApiFetch } from "./api/useApiFetch";
import { Register } from "./Register";

interface Todo {
  id: number;
  name: string;
  dueDate: string | null;
  isCompleted: boolean;
}

const API_URL = "http://localhost:5085/tasks";

function App() {
  const [tasks, setTasks] = useState<Todo[]>([]);
  const [name, setName] = useState("");
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingName, setEditingName] = useState("");
  const [editingDueDate, setEditingDueDate] = useState("");
  const [newTaskDueDate, setNewTaskDueDate] = useState("");
  const [showRegister, setShowRegister] = useState(false);

  const { isLogged, userRole, logout } = useAuth();
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
      dueDate: newTaskDueDate || null,
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
      setNewTaskDueDate("");
    } else {
      console.error(res.statusText);
    }
  };

  // EDIT TASKS
  const handleEdit = (task: Todo) => {
    setEditingId(task.id);
    setEditingName(task.name);
    setEditingDueDate(task.dueDate!);
  };

  const handleSaveEdit = async (task: Todo) => {
    const res = await apiFetch(`${API_URL}/${task.id}`, {
      method: "PUT",
      body: JSON.stringify({
        name: editingName,
        dueDate: editingDueDate || null,
      }),
    });

    if (res.ok) {
      const updatedTask = await res.json();

      setTasks((prev) => prev.map((t) => (t.id === task.id ? updatedTask : t)));
      setEditingId(null);
      setEditingName("");
      setEditingDueDate("");
    } else {
      console.error(res.statusText);
    }
  };

  const handleCancelEdit = () => {
    setEditingId(null);
    setEditingName("");
    setEditingDueDate("");
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

  // TOGGLE
  const handleToggleComplete = async (task: Todo) => {
    const res = await apiFetch(`${API_URL}/${task.id}/status`, {
      method: "PATCH",
      body: JSON.stringify({
        isCompleted: !task.isCompleted,
      }),
    });

    if (res.ok) {
      const updatedTask = await res.json();
      setTasks((prev) => prev.map((t) => (t.id === task.id ? updatedTask : t)));
    } else {
      console.error(res.statusText);
    }
  };

  const handleAdminBtn = async () => {
    const res = await apiFetch(`${API_URL}/admin`);

    if (res.ok) {
      const tasks = await res.json();
      console.log(tasks);
    } else {
      console.error(res.statusText);
    }
  };

  const activeTasks = tasks.filter((t) => !t.isCompleted);
  const completedTasks = tasks.filter((t) => t.isCompleted);

  const tomorrowMin = (() => {
    const d = new Date();
    d.setDate(d.getDate() + 1);
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, "0");
    const day = String(d.getDate()).padStart(2, "0");
    return `${y}-${m}-${day}`;
  })();

  const formatDate = (dateStr: string) => {
    const [y, m, d] = dateStr.split("-");
    return `${d}.${m}.${y}`;
  };

  return (
    <div className="app">
      {!isLogged ? (
        showRegister ? (
          <Register setShowRegister={setShowRegister} />
        ) : (
          <Login setShowRegister={setShowRegister} />
        )
      ) : (
        <div className="todo-container">
          <h1 className="todo-title">My Tasks</h1>
          <button className="btn btn-logout" onClick={logout}>
            Log out
          </button>
          {userRole === "Admin" && (
            <button className="btn btn-admin" onClick={handleAdminBtn}>
              Get admin tasks
            </button>
          )}
          <form className="todo-form" onSubmit={handleSubmit}>
            <input
              className="todo-input"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="whats the new task..."
            />
            <input
              className="todo-date-input"
              type="date"
              min={tomorrowMin}
              value={newTaskDueDate}
              onChange={(e) => setNewTaskDueDate(e.target.value)}
              onFocus={(e) => {
                try {
                  (e.target as HTMLInputElement).showPicker?.();
                } catch {}
              }}
              onClick={(e) => {
                try {
                  (e.target as HTMLInputElement).showPicker?.();
                } catch {}
              }}
            />
            <button className="btn btn-add" type="submit">
              Add
            </button>
          </form>
          <ul className="todo-list">
            {activeTasks.map((task) => (
              <li className="todo-item" key={task.id}>
                {editingId === task.id ? (
                  <div className="todo-edit-row">
                    <input
                      className="todo-input"
                      value={editingName}
                      onChange={(e) => setEditingName(e.target.value)}
                    />
                    <input
                      className="todo-date-input"
                      type="date"
                      min={tomorrowMin}
                      value={editingDueDate}
                      onChange={(e) => setEditingDueDate(e.target.value)}
                      onFocus={(e) => {
                        try {
                          (e.target as HTMLInputElement).showPicker?.();
                        } catch {}
                      }}
                      onClick={(e) => {
                        try {
                          (e.target as HTMLInputElement).showPicker?.();
                        } catch {}
                      }}
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
                    <label className="todo-checkbox-label">
                      <input
                        type="checkbox"
                        className="todo-checkbox"
                        checked={task.isCompleted}
                        onChange={() => handleToggleComplete(task)}
                      />
                      <span className="todo-checkbox-custom" />
                    </label>
                    <span className="todo-name">{task.name}</span>
                    <span className="todo-date">
                      Due by {task.dueDate ? formatDate(task.dueDate) : "—"}
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
          {completedTasks.length > 0 && (
            <div className="completed-section">
              <h2 className="completed-title">Completed</h2>
              <ul className="todo-list">
                {completedTasks.map((task) => (
                  <li className="todo-item todo-item-done" key={task.id}>
                    {editingId === task.id ? (
                      <div className="todo-edit-row">
                        <input
                          className="todo-input"
                          value={editingName}
                          onChange={(e) => setEditingName(e.target.value)}
                        />
                        <input
                          className="todo-date-input"
                          type="date"
                          min={tomorrowMin}
                          value={editingDueDate}
                          onChange={(e) => setEditingDueDate(e.target.value)}
                          onFocus={(e) => {
                            try {
                              (e.target as HTMLInputElement).showPicker?.();
                            } catch {}
                          }}
                          onClick={(e) => {
                            try {
                              (e.target as HTMLInputElement).showPicker?.();
                            } catch {}
                          }}
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
                        <label className="todo-checkbox-label">
                          <input
                            type="checkbox"
                            className="todo-checkbox"
                            checked={task.isCompleted}
                            onChange={() => handleToggleComplete(task)}
                          />
                          <span className="todo-checkbox-custom" />
                        </label>
                        <span className="todo-name todo-name-done">
                          {task.name}
                        </span>
                        <span className="todo-date">
                          Due by {task.dueDate ? formatDate(task.dueDate) : "—"}
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
      )}
    </div>
  );
}

export default App;
