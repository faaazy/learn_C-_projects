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

function getInitialAuthState() {
  const jwtToken = localStorage.getItem("loginJWTToken");

  if (!jwtToken) return false;

  const decodedJwt = decodeJwtToken(jwtToken);

  if (!decodedJwt) return false;

  if (decodedJwt.exp === null || decodedJwt.exp < Date.now() / 1000) {
    localStorage.removeItem("loginJWTToken");
    return false;
  }

  return true;
}

export const AuthContext = createContext<AuthContextValue | null>(null);

export const AuthProvider = ({ children }: PropsWithChildren) => {
  const [isLogged, setIsLogged] = useState(getInitialAuthState);

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
