<template>
  <div class="top-component">
    <h3>New user</h3>
    <div class="info">
      <label class="top-10">Username</label>
      <el-input placeholder="Username" v-model="username"></el-input>
      <label class="top-10">Password</label>
      <el-input placeholder="Password" v-model="password" show-password></el-input>
      <label class="top-10">Repeat Password</label>
      <el-input placeholder="Repeat Password" v-model="password2" show-password></el-input>
      <label class="top-10">Full name</label>
      <el-input placeholder="Full name" v-model="fullName"></el-input>
      <label class="top-10">Email</label>
      <el-input placeholder="Email" v-model="email"></el-input>
      <label class="top-10">Phone number</label>
      <el-input placeholder="Phone Number" v-model="phone"></el-input>    
    </div>
    <div class="module">
      <h4>Danh sách thiết bị</h4>
      <div v-for="device in devices"
          v-bind:key="device.id">
        <div class="device my-box top-20">
          <div>Device name</div>
          <el-input class="my-input" placeholder="Device name" v-model="device.name"></el-input>
          <div class="options">
            <el-checkbox v-model="device.hasLight">Has light module</el-checkbox>
            <el-checkbox v-model="device.hasTemp">Has temperature module</el-checkbox>
            <el-checkbox v-model="device.hasHum">Has humidity module</el-checkbox>
            <el-checkbox v-model="device.hasPH">Has PH module</el-checkbox>
            <div class="my-button">
              <el-button type="danger" v-on:click="remove(device.id)">Remove</el-button>
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
            <el-button type="primary" v-on:click="addNewDevice">Add</el-button>
          </div>
        </div>
      </div>
    </div>
    <div class="top-20">
      <el-button type="success" v-on:click="createUser">Create User</el-button>
    </div>
  </div>
</template>

<script>
import adminApi from "../services/adminApi.js"
import {simpleClone} from "../Utils/util.js"
import {device} from "../model/device.js"
// import validator from "../../node_modules/validator/index.js"
import validator from 'validator';
export default {
  methods: {
    showList() {
      console.log(this.devices);
    },
    remove(id) {
      for(let i = 0; i < this.devices.length; i++) {
        if(this.devices[i].id === id) {
          this.devices.splice(i, 1);
          break;
        }
      }
    },
    addNewDevice() {
      this.newDevice.id = parseInt(performance.now());
      this.devices.push(simpleClone(this.newDevice));
    },
    validate() {
      // TODO validate
      let message = null;
      if(validator.isEmpty(this.username)){
        message = "Username is empty"
      } else if(!validator.isAlpha(this.username)) {
        message = "Username only contains a->z"
      } else if(validator.isEmpty(this.password)) {
        message = "Password is empty"
      } else if(this.password !== this.password2) {
        message= 'Password not matched';
      } else if(!validator.isEmpty(this.email) && !validator.isEmail(this.email)) {
        message = 'Input valid email';
      }
      if(message !== null) {
        this.$message({
          message,
          type: 'error'
        });
        return false;
      }
      return true;
    },
    createUser() {
      let devices;
      if(this.validate()) {
        devices = this.devices.map(d => {
          return {
            name: d.name,
            "hasLight": d.hasLight,
            "hasTemp": d.hasTemp,
            "hasHumi": d.hasHum,
            "hasPH": d.hasPH,
          }
        });
        adminApi.createUser({
          "username": this.username,
          "password": this.password,
          "fullName": this.fullName,
          "phoneNumber": this.phone,
          "email": this.email,
          "devices": devices
        }).then(res => {
          if(res.ok) {
            this.$router.push("/admin-app/user-list");
          } else {
            res.json().then(data => {
              this.$message({
                message: data["error"] || res.statusText,
                type: "error"
              })
            });
          }
        })
      }
    }
  },
  components: {
    
  },
  data: function() {
    return {
      username: "",
      password: "",
      password2: "",
      fullName: "",
      email: "",
      phone:"",
      devices: [
        // new device(0, "test device", false, false, false, false, false),
        // new device(1, "test 1", false, false, false, false, false),
        // new device(2, "test hai", false, false, false, false, false),
        // new device(3, "test thrê", false, false, false, false, false),
        // new device(4, "test foủ", false, false, false, false, false),
        // new device(5, "test five", false, false, false, false, false),
        // new device(6, "test device", false, false, false, false, false),
        // new device(7, "test device", false, false, false, false, false),
      ],
      newDevice: new device(7, "test device", false, false, false, false, true),
    }
  }
}
</script>

<style scoped>
.top-component {
  display: flex;
  flex-direction: column;
  align-items: center;
  overflow: auto;
}
.info {
  width: 500px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
.createButton {
  width: 100px;
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