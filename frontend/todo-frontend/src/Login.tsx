import React, { useState } from "react";

interface LoginProps {
  setLoginHandler: () => void;
}

export function Login({ setLoginHandler }: LoginProps) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async (e: React.SubmitEvent) => {
    e.preventDefault();

    const userData = { username, password };

    const res = await fetch("http://localhost:5085/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(userData),
    });

    if (res.ok) {
      const jwt = await res.json();
      localStorage.setItem("loginJWTToken", jwt.token);

      setLoginHandler();
    } else {
      alert(res.statusText);
    }
  };

  return (
    <form onSubmit={(e) => handleLogin(e)}>
      <label htmlFor="username">Username</label>
      <input
        type="text"
        onChange={(e) => setUsername(e.target.value)}
        value={username}
        id="username"
        name="username"
        required
      />

      <label htmlFor="password">Password</label>
      <input
        type="password"
        onChange={(e) => setPassword(e.target.value)}
        value={password}
        id="password"
        name="password"
        required
      />

      <button type="submit">Login</button>
    </form>
  );
}
