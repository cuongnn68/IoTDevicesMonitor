import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import installElementPlus from './plugins/element'

const app = createApp(App)
installElementPlus(app)
app.use(router).mount('#app')

console.log(process.env.NODE_ENV);
if(process.env.NODE_ENV === "development") {
  localStorage.setItem("domain", "http://localhost:5000");
} else {
  localStorage.setItem("domain", "https://iot-app-nnc.herokuapp.com"); // TODO change to domain to heroku
}
document.title = "Admin app";

// app.config.globalProperties.admin = null;
// app.config.globalProperties.token = null;