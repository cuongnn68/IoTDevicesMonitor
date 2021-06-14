<template>
    <el-menu 
      router=true
      class="el-menu-vertical">
      <div class="for-fun">
        <i class="material-icons" style="font-size: 169px">account_box</i>
        <h2>ADMIN</h2>
      </div>
      <el-menu-item index="/admin-app/home" class="option">
        <i class="material-icons option-icon">home</i>
        <span class="option-name">Home</span>
      </el-menu-item>
      <el-menu-item index="/admin-app/about" class="option">
        <i class="material-icons option-icon">dashboard</i>
        <span class="option-name">About</span>
      </el-menu-item>
      <el-menu-item index="/admin-app/user-list" class="option">
        <i class="material-icons option-icon">list_alt</i>
        <span class="option-name">User List</span>
      </el-menu-item>
      <el-menu-item index="/admin-app/new-user" class="option">
        <i class="material-icons option-icon">person_add</i>
        <span class="option-name">Create User</span>
      </el-menu-item>
      <el-menu-item index="/admin-app/login" class="option">
        <i class="material-icons option-icon">login</i>
        <span class="option-name">Login</span>
      </el-menu-item>
      <el-menu-item index="" class="option" v-on:click="logout">
        <i class="material-icons option-icon">logout</i>
        <span class="option-name">Logout</span>
      </el-menu-item>
    </el-menu>
</template>

<script>
// import global from "../services/globalVar.js"
// import router from "../router/index.js"
import * as myStorage from "../services/storage.js";
export default {
  methods: {
    logout() {
      console.log(myStorage.getAdmin());
      myStorage.removeInfo();
      console.log(myStorage.getAdmin());
      console.log("logout");
      this.$router.push("home");
      // vm.$forceUpdate();
    },

  },
  computed: {
    logined() {
      return myStorage.getAdmin();
    }
  }
}
</script>

<style scoped>
.option {
  display: flex;
  justify-content: center; /* align horizontal */
  align-items: center; /* align vertical */
}
.option-icon {
  flex-grow: 1;
}
.option-name {
  flex-grow: 2;
}
.for-fun {
  display: flex;
  flex-direction: column;
}
</style>