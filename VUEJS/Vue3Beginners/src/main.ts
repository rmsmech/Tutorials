import { createApp } from 'vue'
import router from './routes';
import {createPinia} from 'pinia';
import './style.css'
import App from './App.vue'
// import Hw from './components/HelloWorld.vue'

const _pinia = createPinia(); //a store
createApp(App)
    .use(router)
    .use(_pinia)
    .mount('#app')
    
