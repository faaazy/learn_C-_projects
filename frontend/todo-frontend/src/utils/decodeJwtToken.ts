export function decodeJwtToken(token: string | null) {
  if (!token) return;

  const tokenPayload = token.split(".")[1];

  const decodedToken = JSON.parse(atob(tokenPayload));

  const jwtObject = { id: null, username: null, role: null };

  for (const key in decodedToken) {
    const lastKeyPart = key.split("/").at(-1);

    switch (lastKeyPart) {
      case "nameidentifier":
        jwtObject.id = decodedToken[key];
        break;
      case "name":
        jwtObject.username = decodedToken[key];
        break;
      case "role":
        jwtObject.role = decodedToken[key];
        break;

      default:
        break;
    }
  }

  return jwtObject;
}
