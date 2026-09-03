export async function apiFetch(
  link: string,
  options: RequestInit = {},
): Promise<Response> {
  const jwtToken = localStorage.getItem("loginJWTToken");

  const headers = new Headers(options.headers);

  if (jwtToken) {
    headers.set("Authorization", `Bearer ${jwtToken}`);
  }

  if (options.body) {
    headers.set("Content-Type", "application/json");
  }

  const res = await fetch(link, {
    ...options,
    headers,
  });

  if (res.status === 401) {
    localStorage.removeItem("loginJWTToken");
  }

  return res;
}
