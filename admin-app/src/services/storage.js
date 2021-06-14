export function setInfo(admin, token) {
  localStorage.setItem("adminInfo", JSON.stringify({admin, token}));
}

export function getInfo() {
  let info = localStorage.getItem("adminInfo");
  return JSON.parse(info);
}

export function removeInfo() {
  setInfo(null, null);
}

export function getToken() {
  return getInfo()?.token;
}
export function getAdmin() {
  return getInfo()?.admin;
}
