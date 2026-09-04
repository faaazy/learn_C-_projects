import {
  createContext,
  useContext,
  useState,
  type PropsWithChildren,
} from "react";

interface AuthContextValue {
  isLogged: boolean;
  login: () => void;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextValue | null>(null);

export const AuthProvider = ({ children }: PropsWithChildren) => {
  const [isLogged, setIsLogged] = useState(
    localStorage.getItem("loginJWTToken") !== null,
  );

  const login = () => setIsLogged(true);

  const logout = () => {
    setIsLogged(false);

    localStorage.removeItem("loginJWTToken");
  };

  const value = {
    isLogged,
    login,
    logout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);

  if (!ctx) {
    throw new Error("useAuth must be used within AuthProvider");
  }

  return ctx;
};
