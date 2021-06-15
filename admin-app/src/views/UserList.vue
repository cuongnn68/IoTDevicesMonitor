<template>
  <div class="top-component">
    <h1>Danh sách người dùng</h1>
    <p style="align-self:flex-start;">Tìm kiếm theo tài khoản, tên, số điện thoại hoặc email.</p>
    <div style="display:flex;flex-direction:row;">
      <el-input
        style="max-width: 300px"
        placeholder="Type something"
        prefix-icon="el-icon-search"
        v-model="keywordInput">
      </el-input>
      <el-button v-on:click="search">Search</el-button>
    </div>
    <el-table
      v-loading="loading"
      v-on:row-click="rowClickHanlder"
      class="table top-20"
      border=true
      :data="userList"
      >
      <el-table-column
        prop="username"
        label="Username">
      </el-table-column>
      <el-table-column
        prop="fullName"
        label="Name">
      </el-table-column>
      <el-table-column
        prop="phone"
        label="Phone">
      </el-table-column>
      <el-table-column
        prop="email"
        label="Email">
      </el-table-column>
    </el-table>
    <el-pagination
      background
      layout="prev, pager, next"
      :total="total"
      :page-size="row"
      :current-page="curruntPage"
      v-on:current-change="curruntPageChange">
    </el-pagination>
  </div>
</template>

<script>
import adminApi from "../services/adminApi.js"
export default {
  created() {
    this.getUsers(null, 1, this.row);
  },
  data() {
    return {
      loading: false,
      curruntPage: 1,
      total: 0,
      row: 5,
      keyword: null,
      keywordInput: "",
      userList: [
 
      ]
    }
  },
  methods: {
    getUsers(keyword, page) {
      this.loading = true;
      adminApi.searchUser(keyword, page, this.row)
        .then(response => {
          if(response.ok) {
            console.log(response);
            response.json().then(data => {
              console.log(data)
              this.userList = data["userList"];
              this.total = data["total"]
              this.loading = false;
            });
            console.log(this.userList);
            console.log(this.total)
          }
        });
    },
    search() {
      this.keyword = this.keywordInput;
      this.curruntPage = 1;
      this.getUsers(this.keyword, 1, this.row);
    },
    curruntPageChange(page) {
      this.getUsers(this.keyword, page, this.row);
    },
    rowClickHanlder(row, column, event) {
      console.log(row);
      console.log(column);
      console.log(event);
      this.$router.push("/admin-app/user/" + row.username)
    }
  }
}
</script>

<style scoped>
.top-component {
  display: flex;
  flex-direction:column;
  margin: 40px;
}
.table {
  
}
</style>