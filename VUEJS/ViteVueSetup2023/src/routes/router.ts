import * as vR from "vue-router";
import DashboardVue from "@pages/Dashboard.vue";
import Pnf from "@pages/NotFoundPage.vue";

//Create a router and then export it

const _routes:Array<vR.RouteRecordRaw> = [
//Path & Component are mandatory.
{
    path:"/",
    component:DashboardVue,
    name:"home"
},
{
    path:"/demo",
    component:()=> import("@pages/DemoPage.vue"),// Lazy loading to defer loading only when required
    name:"home.demo" //Use defined. Keep dot notation to have elegant design
},
{
    path:"/login",
    component:()=> import("@pages/AuthPage.vue"),// Lazy loading to defer loading only when required
    name:"home.auth" //Use defined. Keep dot notation to have elegant design
},
{
    path:"/nf", //Need to change with regex to catch all non-defined
    component:Pnf,
    name:"home.missing"
},
];

const router = vR.createRouter({
  history: vR.createWebHistory(), //WebHash history will add hash before every route
  routes: _routes, //Send the routes here.
});

export default router;
