import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import UserList from '../views/UserList.vue'
import NewUserInfo from '../views/NewUserInfo.vue'

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
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/admin-app/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/admin-app/user-list',
    name: 'UserList',
    component: UserList,
  },
  {
    path: '/admin-app/new-user',
    name: 'NewUserInfo',
    component: NewUserInfo,
  },
] // default 404 not found ...

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
