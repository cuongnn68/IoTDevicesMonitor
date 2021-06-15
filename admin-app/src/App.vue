<template>
  <Nav class="nav"/>
  <router-view class="view"/>
</template>

<script>
import Nav from './components/Navigation.vue';
export default {
  name: 'App',
  components: {
    Nav
  }
}
</script>

<style>
body {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  /* default ^ */
  display: flex;
  flex-direction: row;
  height: 100%;
}

.view {
  flex-grow: 9;
}

.nav {
  min-width: 200px;
}

.my-box {
  box-shadow: 0 2px 4px rgba(0, 0, 0, .12), 0 0 6px rgba(0, 0, 0, .04);
  border-radius: 4px;
}

.top-20 {
  margin-top: 20px;
}
.top-10 {
  margin-top: 10px;
}
</style>
