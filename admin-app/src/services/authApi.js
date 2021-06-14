import restApi from "./basicMethod";

export function getAdminToken(admin, password) {
  return restApi.post("/api/auth/admin-token", {admin, password});
}

export function checkLogin() {
  restApi.get("/api/auth/admin-token/authorization").then(
    respones => {
      return respones.ok;
    }
  )
}