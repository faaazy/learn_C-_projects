import { useAuth } from "../auth/AuthContext";
import { apiFetch } from "./apiFetch";

export const useApiFetch = () => {
  const { logout } = useAuth();

  return async (link: string, options: RequestInit = {}) => {
    const res = await apiFetch(link, options);

    if (res.status === 401) {
      logout();
    }
    return res;
  };
};
