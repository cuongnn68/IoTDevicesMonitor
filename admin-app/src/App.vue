<template>
  <!-- <div class="app"> -->
    <div id="nav">
      <router-link to="/admin-app">Home</router-link> |
      <router-link to="/admin-app/about">About</router-link> |
      <router-link to="/admin-app/login">Login</router-link>
    </div>
    <router-view class="view"/>
  <!-- </div> -->
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  /* default ^ */
  display: flex;
  flex-basis: 1;
  flex-direction: column;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

.view {
  flex-basis: 1;
}
</style>
