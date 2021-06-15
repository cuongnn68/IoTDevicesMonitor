<template>
  <div class="device my-box">
    <el-input placeholder="Module name" v-model="moduleName"></el-input>
    <el-checkbox v-model="hasLight">Has light module</el-checkbox>
    <el-checkbox v-model="hasTemp">Has temperature module</el-checkbox>
    <el-checkbox v-model="hasHumi">Has humidity module</el-checkbox>
    <el-checkbox v-model="hasPH">Has PH module</el-checkbox>

  </div>
</template>

<script>
export default {
  data() {
    return {
      hasLight: false,
      hasTemp: false,
      hasHumi: false,
      hasPH: false,
      moduleName: "",
    }
  }
}
</script>

<style>
.device {
  display: flex;
  flex-direction: columns;
  align-items: center;
  padding: 10px;
}
</style>