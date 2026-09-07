import React, { useState } from "react";
import { useAuth } from "./auth/AuthContext";
import { apiFetch } from "./api/apiFetch";

interface LoginProps {
  setShowRegister: React.Dispatch<React.SetStateAction<boolean>>;
}

export function Login({ setShowRegister }: LoginProps) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { login } = useAuth();

  const handleLogin = async (e: React.SubmitEvent) => {
    e.preventDefault();

    const userData = { username, password };

    const res = await apiFetch("http://localhost:5085/auth/login", {
      method: "POST",
      body: JSON.stringify(userData),
    });

    if (res.ok) {
      const jwt = await res.json();
      localStorage.setItem("loginJWTToken", jwt.token);

      login();
    } else {
      res.status === 401 ? alert("Invalid credentials") : alert(res.statusText);
    }
  };

  return (
    <div className="login-card">
      <h1 className="login-title">Login</h1>
      <form className="login-form" onSubmit={(e) => handleLogin(e)}>
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
          Login
        </button>

        <p>
          Dont have an account?
          <button onClick={() => setShowRegister(true)}>Register</button>
        </p>
      </form>
    </div>
  );
}
