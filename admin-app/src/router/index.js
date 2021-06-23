import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
// import Login from '../views/Login.vue'
// import UserList from '../views/UserList.vue'
// import NewUserInfo from '../views/NewUserInfo.vue'
import * as myStorage from '../services/storage.js'
// import UserInfo from '../views/User.vue'

const routes = [
  {
    path: '/admin-app',
    name: 'Home',
    component: Home,
    alias: '/admin-app/home'
  },
  {
    path: '/admin-app/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import('../views/About.vue')
  },
  {
    path: '/admin-app/login',
    name: 'Login',
    // component: Login,
    component: () => import('../views/Login.vue'),
  },
  {
    path: '/admin-app/user-list',
    name: 'UserList',
    // component: UserList,
    component: () => import('../views/UserList.vue'),
  },
  {
    path: '/admin-app/new-user',
    name: 'NewUserInfo',
    // component: NewUserInfo,
    component: () => import('../views/NewUserInfo.vue'),
  },
  {
    path: '/admin-app/user/:username',
    name: 'UserInfo',
    // component: UserInfo,
    component: () => import('../views/User.vue'),
  },
] // default 404 not found ...

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.beforeEach((to, from, next) => {
  // TODO Login
  let isLogin = myStorage.getAdmin();
  const privatePage = [
    "/admin-app/user-list",
    "/admin-app/new-user",
    '/admin-app/user',
  ];
  if(!isLogin && privatePage.includes(to.path)) {
    next("/admin-app/login");
    return;
  }
  if(isLogin && to.path === "/admin-app/login") {
    next("/admin-app/home");
    return;
  }
  next();
});

export default router
