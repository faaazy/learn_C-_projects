import { useEffect, useState } from "react";
import "./App.css";

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

  // first tasks
  useEffect(() => {
    fetch(API_URL)
      .then((res) => res.json())
      .then((data) => setTasks(data));
  }, []);

  const handleSubmit = async (e: React.SubmitEvent) => {
    e.preventDefault();

    const newTask: Todo = {
      id: 0,
      name,
      dueDate: new Date().toISOString(),
      isCompleted: false,
    };

    const response = await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(newTask),
    });
    const createdTask = await response.json();

    setTasks((prev) => [...prev, createdTask]);
    setName("");
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <input
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="whats the new task..."
        />
        <button type="submit">Send</button>
      </form>
      <ul>
        {tasks.map((task) => (
          <li key={task.id}>
            {task.name} - {task.isCompleted ? "completed" : "in progress!"}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
