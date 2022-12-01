<script setup lang="ts">
// import HelloWorld from './components/HelloWorld.vue'
</script>

<template>
  <div style="position: absolute; top:1em;left:1em">
    <!-- <a href="/">Home </a>
    <a href="/demo">Demo </a>
    <a href="https://google.com">Google </a> -->
    <router-link to="/"> Home </router-link>
    <router-link to="/demo"> Demo </router-link>
    <router-link to="/asdfasdfasd"> Dummy </router-link>
  </div>
  <router-view/>
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
