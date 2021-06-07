<template>
  <div class="top">
    <div class="login-form">
      <div>Admin acount</div>
      <input type="text" name="username" >
      <div>Passowrd</div>
      <input type="password">
      <button>Login</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "Login"
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