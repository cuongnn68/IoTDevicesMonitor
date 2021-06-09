<template>
  <div class="top-component">
    <h3>New user</h3>
    <el-input placeholder="Username"></el-input>
    <el-input placeholder="Password"></el-input>
    <el-input placeholder="Repeat Password"></el-input>
    <el-input placeholder="Email"></el-input>
    <el-input placeholder="Phone Number"></el-input>
    <h1>TODO create device</h1>
    <el-button>Create</el-button>
  </div>
</template>

<script>
export default {

}
</script>

<style scoped>
.top-component {
  display: flex;
  flex-direction: column;
}
</style>