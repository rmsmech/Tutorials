<template>
    <div>
        <!-- <img  src="../assets/dragon.png" alt="Dragon"/> -->
        <h2 class="textcls" id="textid">{{name}}</h2>
        <h3 class="textcls" >Age: {{age}} | Sleeping: {{ isSleeping ? 'yes':'nope' }}</h3>
        <h3 class="textcls" >Salary: $ {{dstore.salary}}</h3>
        <button id="incrbtn" v-on:click="increaseSalary">Increase Salary</button>
        <button id="incrbtn" v-on:click="storeinLocal">Store Salary</button>
        <button id="incrbtn" v-on:click="clearninLocal">Clear Local</button>
    </div>
    <!-- <imap/> -->
</template>

<script setup lang="ts">
import { onUpdated,onBeforeUpdate, onBeforeMount , onMounted, ref } from 'vue';
import func from '../../vue-temp/vue-editor-bridge';
import {datastore} from '../stores';
// import imap from './IndiaMap.vue';

//Functions
//exposed/exported Properties (to be filled from outside)
//Internal attributes/field
const eprop = defineProps({
    name:String,
    age:Number,
    isSleeping:Boolean,
});

const dstore = datastore();

const emit = defineEmits(["sincreased"]);

// const salary = ref(8500);
const salary = dstore.salary;
// let salary = "5600";

function increaseSalary(eve:any) {
    console.log("Current : ", salary,eve,eve.target.id);
    dstore.salary = dstore.salary + 1000;
    console.log("Updated : ", salary);
    // this.$emit('event', […args]); //'this' is not available inside script setup because it is in a different scope.
    emit('sincreased');
}

function storeinLocal(){
    dstore.storeinLocal();
}

function clearninLocal(){
    dstore.clearLocal();
}

function handleMount(){
    console.log("I'm Mounted 2");
}

// onMounted(()=>{
// console.log("I'm Mounted");

// });

// onBeforeMount(()=>{
//     console.log("I'm about to be Mounted");
// });

// onUpdated(()=>{
//     // salary.value = salary.value - 500;
//     console.log("I'm Updated");

// });

// onBeforeUpdate(()=>{
//     console.log("I'm about to be Updated",dstore.salary);
//     dstore.salary = dstore.salary - 1500;
// });

// console.log(eprop.name);
</script>

<style scoped>
.textcls{
    color:indianred;
    font-size: 2em;
}

#textid{
    color:yellow;
    font-size:3em;
}

h2{
    color:palegreen;
    font-size:1em;
}

</style>

<!-- Order of precedence:
1. TargetElement-inline css (top priority)
2. Element Id
3. Element Class
4. HTML Element Identifier -->