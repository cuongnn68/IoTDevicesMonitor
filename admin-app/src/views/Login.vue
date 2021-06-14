<template>
  <div class="top">
    <div class="login-form">
      <h3>Admin</h3>
      <el-input placeholder="Username" v-model="username" type="text"></el-input>
      <el-input placeholder="Password" v-model="password" type="password"></el-input>
      <el-button v-on:click="login">Login</el-button>
    </div>
  </div>
</template>

<script>
// import globalVar from "../services/globalVar.js"
import * as myStorage from "../services/storage.js"
import {getAdminToken} from "../services/authApi.js"
export default {
  name: "Login",
  // TODO show error
  data() {
    return {
      username: "",
      password:"",
    }
  },
  methods: {
    login() {
      console.log(myStorage.getAdmin());
      getAdminToken(this.username, this.password).then(
        response => {
          if(response.ok) {
            response.json().then(
              data => {
                myStorage.setInfo(this.username, data["adminToken"]);
                console.log(myStorage.getAdmin());
                this.$router.push("/admin-app/user-list");
              }
            )
          } else {
            response.json().then(
              data => console.log(data)
            )
          }
        }
      );
    },
    getAdminAccount() {
      return myStorage.getAdmin();
    }
  }
}
</script>

<style scoped>
.top {
  height: 100;
  display: flex;
  justify-content: center;
  align-items: center;
}

.login-form {
  max-width: 400px;
  display: flex;
  flex-direction: column;
}
</style>