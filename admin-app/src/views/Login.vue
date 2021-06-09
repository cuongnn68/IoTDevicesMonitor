<template>
  <div class="top">
    <div class="login-form">
      <h3>Admin</h3>
      <el-input placeholder="Username" v-model="username" type="text"></el-input>
      <el-input placeholder="Password" v-model="password" type="password"></el-input>
      <el-button>Login</el-button>
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
  data() {
    return {
      username:"",
      password:"",
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