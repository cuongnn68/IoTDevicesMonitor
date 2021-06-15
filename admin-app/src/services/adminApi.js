import rest from "./basicMethod.js"

export default {searchUser, createUser, getUserInfo, updateUser, deleteUser, addDevice, removeDevice}

function searchUser(keyword, page, row) {
  return rest.get("/api/admin/user", {
    keyword: keyword,
    rowPerPage: row,
    page: page,
  });
}

function createUser(newUser) {
  return rest.post("/api/admin/user", newUser);
}

function getUserInfo(username) {
  return rest.get("/api/admin/user/" + username, {});
}

function updateUser(username, fullName, phoneNumber, email) {
  return rest.put("/api/admin/user/" + username, {
    username,
    phoneNumber,
    fullName,
    email,
  })
}

function deleteUser(username) {
  return rest.deleteMethod("/api/admin/user/" + username);
}

function addDevice(username, deviceName, hasLight, hasTemp, hasHumi, hasPh) {
  return rest.post("/api/admin/user/" + username + "/device", {
    "name": deviceName,
    "hasLight": hasLight,
    "hasTemp": hasTemp,
    "hasHumi": hasHumi,
    "hasPH": hasPh,
  })
}

function removeDevice(username, deviceId) {
  return rest.deleteMethod("/api/admin/user/" + username + "/device/" + deviceId);
}