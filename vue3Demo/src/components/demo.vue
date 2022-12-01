<template>
    <div>
        <!-- <img  src="../assets/dragon.png" alt="Dragon"/> -->
        <h2 class="textcls" id="textid">{{name}}</h2>
        <h3 class="textcls" >Age: {{age}} | Sleeping: {{ isSleeping ? 'yes':'nope' }}</h3>
        <h3 class="textcls" >Salary: $ {{salary}}</h3>
        <button v-on:click="increaseSalary">Increase Salary</button>
    </div>
</template>

<script setup lang="ts">
import { onUpdated,onBeforeUpdate, onBeforeMount , onMounted, ref } from 'vue';

//Functions
//exposed/exported Properties (to be filled from outside)
//Internal attributes/field
const eprop = defineProps({
    name:String,
    age:Number,
    isSleeping:Boolean,
});

const salary = ref(8500);
// let salary = "5600";

function increaseSalary() {
    console.log("Current : ", salary.value);
    salary.value = salary.value + 1000;
    console.log("Updated : ", salary.value);
}

function handleMount(){
    console.log("I'm Mounted 2");
}

onMounted(()=>{
console.log("I'm Mounted");

});

onBeforeMount(()=>{
    console.log("I'm about to be Mounted");
});

onUpdated(()=>{
    // salary.value = salary.value - 500;
    console.log("I'm Updated");

});

onBeforeUpdate(()=>{
    console.log("I'm about to be Updated",salary.value);
    salary.value = salary.value - 1500;
});



console.log(eprop.name);
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