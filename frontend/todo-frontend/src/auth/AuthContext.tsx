import {
  createContext,
  useContext,
  useState,
  type PropsWithChildren,
} from "react";
import { decodeJwtToken } from "../utils/decodeJwtToken";

interface AuthContextValue {
  isLogged: boolean;
  login: () => void;
  logout: () => void;
  userRole: string | null;
}

export const AuthContext = createContext<AuthContextValue | null>(null);

export const AuthProvider = ({ children }: PropsWithChildren) => {
  const [isLogged, setIsLogged] = useState(
    localStorage.getItem("loginJWTToken") !== null,
  );

  const userClaims = isLogged
    ? decodeJwtToken(localStorage.getItem("loginJWTToken"))
    : null;

  const userRole = userClaims?.role ?? null;

  const login = () => setIsLogged(true);

  const logout = () => {
    setIsLogged(false);

    localStorage.removeItem("loginJWTToken");
  };

  const value = {
    isLogged,
    login,
    logout,
    userRole,
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
