<template>
  <div class="top-component">
    <h3>New user</h3>
    <el-input placeholder="Username"></el-input>
    <el-input placeholder="Password"></el-input>
    <el-input placeholder="Repeat Password"></el-input>
    <el-input placeholder="Email"></el-input>
    <el-input placeholder="Phone Number"></el-input>
    <h1>TODO create device</h1>
    <div style="box-shadow: 0 2px 4px rgba(0, 0, 0, .12), 0 0 6px rgba(0, 0, 0, .04);border-radius: 4px">
      
      <el-button>Add device</el-button>
      <el-button>Remove device</el-button>
    </div>
    <br>
    <el-button>Create</el-button>
  </div>
</template>

<script>

class device {
  constructor(id, name, haveTemp, haveHum, havePH) {
    this.id = id;
    this.name = name;
    this.haveTemp = haveTemp;
    this.haveHum = haveHum;
    this.havePH = havePH;
  }
}
export default {
  data() {
    return {
      devices: [
        new device(0, "test device 1", false, false, false),
      ]
    }
  }
}
</script>

<style scoped>
.top-component {
  display: flex;
  flex-direction: column;
}
</style>