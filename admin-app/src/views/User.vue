<template>
  <div class="top-component" v-loading="loading">
    <h4>Thông tin người dùng</h4>
    <div class="info"> 
      <label class="top-10">Username</label>
      <el-input disabled=true v-model="username" class="top-10"></el-input>
      <label class="top-10">Full Name</label>
      <el-input v-model="fullName" class="top-10"></el-input>
      <label class="top-10">Phone</label>
      <el-input v-model="phone" class="top-10"></el-input>
      <label class="top-10">Email</label>
      <el-input v-model="email" class="top-10"></el-input>
      <div class="save-button">
        <el-button type="success" v-on:click="updateUserInfo">Save</el-button>
      </div>
      <div class="save-button">
        <el-button type="danger" v-on:click="deleteUser">Delete</el-button>
      </div>
    </div>
    <el-divider></el-divider>
    <div class="module">
      <h4>Danh sách thiết bị</h4>
      <div v-for="device in devices"
          v-bind:key="device.id">
        <div class="device my-box top-20">
          <div>Device name #id:{{device.id}}</div>
          <el-input class="my-input" placeholder="Device name" v-model="device.name" disabled></el-input>
          <div class="options">
            <el-checkbox v-model="device.hasLight" disabled>Has light module</el-checkbox>
            <el-checkbox v-model="device.hasTemp" disabled>Has temperature module</el-checkbox>
            <el-checkbox v-model="device.hasHum" disabled>Has humidity module</el-checkbox>
            <el-checkbox v-model="device.hasPH" disabled>Has PH module</el-checkbox>
            <div class="my-button">
              <el-button type="danger" v-on:click="removeDevice(device.id)">Delete</el-button>
            </div>
          </div>
        </div>
      </div>
      <el-divider></el-divider>
      <div class="device my-box">
          <div>Device name</div>
          <el-input placeholder="Device name" v-model="newDevice.name"></el-input>
          <div class="options">
            <el-checkbox v-model="newDevice.hasLight">Has light module</el-checkbox>
            <el-checkbox v-model="newDevice.hasTemp">Has temperature module</el-checkbox>
            <el-checkbox v-model="newDevice.hasHum">Has humidity module</el-checkbox>
            <el-checkbox v-model="newDevice.hasPH">Has PH module</el-checkbox>
            <div class="my-button">
              <el-button type="primary" v-on:click="addDevice">Add</el-button>
            </div>
          </div>
      </div>
    </div>
  </div>
</template>

<script>
import {device} from "../model/device.js";
import adminApi from "../services/adminApi.js";
export default {
  created() {
    this.init();
    this.$message({
          message: 'User: ' + this.$route.params.username,
          type: 'success'
        });
  },
  data() {
    return {
      loading: false,
      username: "",
      fullName: "",
      phone: "",
      email: "",
      devices: [
        new device(0, "test device", false, false, false, false, false),
        new device(1, "test 1", false, false, false, false, false),
        new device(2, "test hai", false, false, false, false, false),
        new device(3, "test thrê", false, false, false, false, false),
        new device(4, "test foủ", false, false, false, false, false),
        new device(5, "test five", false, false, false, false, false),
        new device(6, "test device", false, false, false, false, false),
        new device(7, "test device", false, false, false, false, false),
      ],
      newDevice: new device(7, "test device", false, false, false, false, true),
    }
  },
  methods: {
    init() {
      this.loading = true;
      this.username = this.$route.params.username;
      adminApi.getUserInfo(this.username).then(res => {
        // res.text().then(
        //   data => console.log(data)
        // )
        res.json().then(data => {      
          if(res.ok) {
            this.fullName = data["fullName"];
            this.phone = data["phone"];
            this.email = data["email"];
            this.devices = data["devices"].map(e => {
              return new device(e.id, e.name, e.haveLightModule, e.haveTempModule, e.haveHumidityModule, e.havePHModule)
            });
          } else {
            this.$message({
              message: data["error"] || res.statusText,
              type: 'error',
            })
          }
          this.loading = false;
        }).catch(e => console.log(e));
      })
    },
    updateUserInfo() {
      this.loading = true;
      adminApi.updateUser(this.username, this.fullName, this.phone, this.email)
        .then(res => {
          if(res.ok) {
            this.$message({
              message: "User updated",
              type: 'success',
            })
          } else {
            res.json().then(data => {
              this.$message({
                message: data["error"] || res.statusText,
                type: 'error',
              })
            })
          }
        }).finally(this.loading = false)
    },
    deleteUser() {
      this.loading = true;
      adminApi.deleteUser(this.username).then(res => {
        if(res.ok) {
          this.$router.push("/admin-app/user-list");
        } else {
          res.json().then(data => {
            this.$message({
              message: data["error"] || res.statusText,
              type: 'error',
            })
          })
        }
      })
    },
    removeDevice(deviceId) {
      this.loading = true;
      adminApi.removeDevice(this.username, deviceId).then(res => {
        if(res.ok) {
          // this.init();
          let index = this.devices.findIndex(e => e.id = deviceId);
          this.devices.splice(index, 1);
        } else {
          res.json().then(data => {
            this.$message({
              message: data["error"] || res.statusText,
              type: 'error',
            })
          })
        }
      }).finally(() => this.loading = false)
    },
    addDevice() {
      this.loading = true;
      adminApi.addDevice(
        this.username,
        this.newDevice.name,
        this.newDevice.hasLight,
        this.newDevice.hasTemp,
        this.newDevice.hasHum,
        this.newDevice.hasPH,
      ).then(res => {
        if(res.ok) {
          this.init();
        } else {
          res.json().then(data => {
            this.$message({
              message: data["error"] || res.statusText,
              type: 'error',
            })
          })
          this.loading = false;
        }
      })
    }
  }

}
</script>

<style scoped>
.save-button {
  align-self: center;
  margin-top: 10px;
}
.top-component {
  padding: 30px;
  display: flex;
  flex-direction: column;
  overflow: auto;
  align-items: center;
}
.info {
  width: 500px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
.device {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  padding: 10px;
}
.options {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-flow: nowrap;
}
.my-input {
  
}
.my-button {
  margin: 20px;
}
</style>