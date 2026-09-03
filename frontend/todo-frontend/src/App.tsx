import { useEffect, useState } from "react";
import "./App.css";
import { Login } from "./Login";
import { apiFetch } from "./api/apiFetch";

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
  const [isLogged, setIsLogged] = useState(
    localStorage.getItem("loginJWTToken") !== null,
  );

  // first tasks
  useEffect(() => {
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
  }, []);

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

  function handleLogin() {
    setIsLogged(true);
  }

  return (
    <div>
      <button onClick={() => setIsLogged(!isLogged)}>Log in/Log out</button>

      {!isLogged ? (
        <Login setLoginHandler={handleLogin} />
      ) : (
        <>
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
        </>
      )}
    </div>
  );
}

export default App;
