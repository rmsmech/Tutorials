import { createApp } from "vue";
import { createPinia } from "pinia";
import "./style.css";
import App from "./App.vue";
import router from "./routes/router";

const _pinia = createPinia();

createApp(App)
.use(_pinia)
.use(router)
.mount("#app");
