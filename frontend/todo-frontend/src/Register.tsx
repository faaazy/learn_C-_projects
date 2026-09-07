import React, { useState } from "react";
import { apiFetch } from "./api/apiFetch";

interface RegisterProps {
  setShowRegister: React.Dispatch<React.SetStateAction<boolean>>;
}

export function Register({ setShowRegister }: RegisterProps) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleRegister = async (e: React.SubmitEvent) => {
    e.preventDefault();

    const userData = { username, password };

    const res = await apiFetch("http://localhost:5085/auth/register", {
      method: "POST",
      body: JSON.stringify(userData),
    });

    if (res.ok) {
      const successfulMsg = await res.text();
      alert(successfulMsg);

      setShowRegister(false);
    } else {
      alert(res.statusText);
    }
  };

  return (
    <div className="login-card">
      <h1 className="login-title">Welcome</h1>
      <form className="login-form" onSubmit={(e) => handleRegister(e)}>
        <label className="login-label" htmlFor="username">
          Username
        </label>
        <input
          className="todo-input"
          type="text"
          onChange={(e) => setUsername(e.target.value)}
          value={username}
          id="username"
          name="username"
          placeholder="your username"
          required
        />

        <label className="login-label" htmlFor="password">
          Password
        </label>
        <input
          className="todo-input"
          type="password"
          onChange={(e) => setPassword(e.target.value)}
          value={password}
          id="password"
          name="password"
          placeholder="your password"
          required
        />

        <button className="btn btn-add" type="submit">
          Register
        </button>

        <p>
          Have an account?
          <button onClick={() => setShowRegister(false)}>Login</button>
        </p>
      </form>
    </div>
  );
}
