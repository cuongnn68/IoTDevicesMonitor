import * as myStorage from "./storage.js"
export default {get, post, deleteMethod, put};

//??? does it need async await here // async function return Promise so guess not
function get(/** @type {string} */url, data) {
  // url = "https://" + localStorage.getItem("domain") + url;
  url = localStorage.getItem("domain") + url;
  if(data) {
    let keys = Object.keys(data);
    if(keys.length > 0) {
      url = url + "?"
      keys.forEach((key) => {
            if(data[key] !== null)
              url += key + "=" + data[key] + "&";
          });
      url = url.slice(0, url.length-1);
    }
  }
  console.log("get: " + url);
  const token = myStorage.getToken();
  return fetch(url, {
            method: "GET",
            headers: {
              "Accept": "application/json",
              "Authorization": (token && "bearer " + token),
            },
          });
            // .then(response => response.json());
            // .then(val => console.log(val));
}

/**
 * 
 * @param {string} url 
 * @param {object} data 
 */
function post(url, data) {
  // url = "https://" + localStorage.getItem("domain") + url;
  url = localStorage.getItem("domain") + url;
  console.log("post: " + url);
  console.log(JSON.stringify(data));
  const token = myStorage.getToken();
  return fetch(url, {
            method: "POST",
            headers: {
              "Accept": "application/json",
              "Content-type": "application/json; charset=utf-8",
              "Authorization": (token && "bearer " + token),
            },
            body: JSON.stringify(data),
          })
          .catch(err => console.log(err));
}

function put(url, data) {
  // url = "https://" + localStorage.getItem("domain") + url;
  url = localStorage.getItem("domain") + url;
  console.log("put: " + url);
  console.log(JSON.stringify(data));
  const token = myStorage.getToken();
  return fetch(url, {
            method: "PUT",
            headers: {
              "Accept": "application/json",
              "Content-type": "application/json; charset=utf-8",
              "Authorization": (token && "bearer " + token),
            },
            body: JSON.stringify(data),
          })
          .catch(err => console.log(err));
}

function deleteMethod(url, data = null) {
  // url = "https://" + localStorage.getItem("domain") + url;
  url = localStorage.getItem("domain") + url;
  console.log("delete: " + url);
  console.log(JSON.stringify(data));
  const token = myStorage.getToken();
  data = data || "";
  return fetch(url, {
            method: "DELETE",
            headers: {
              "Accept": "application/json",
              "Content-type": "application/json; charset=utf-8",
              "Authorization": (token && "bearer " + token),
            },
            body: JSON.stringify(data),
          })
          .catch(err => console.log(err));
}