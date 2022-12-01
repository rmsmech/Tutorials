//1. Create a router and export it.
import hw from "./components/HelloWorld.vue";
import dem from "./components/demo.vue";
import pnf from "./components/PageNotFound.vue";
import { createRouter, createWebHashHistory, createWebHistory } from 'vue-router';

//Routes
const _routes = [{
    path:"/",
    name:"home",
    component: hw,
    props: {msg : "Hello World 3"}
},
{
    path:"/demo",
    name:"demopage",
    component:dem,
},
{
    path:"/:pathMatch(.*)*",
    name:"notFound",
    component:pnf
}
];

//Create router
const router = createRouter({
    history:createWebHistory(),
    routes: _routes,
});

//Export router
export default router;