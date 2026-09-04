import { useAuth } from "../auth/AuthContext";
import { apiFetch } from "./apiFetch";

export const useApiFetch = () => {
  const { logout } = useAuth();

  return (link: string, options: RequestInit = {}) => {
    return apiFetch(link, options, logout);
  };
};
