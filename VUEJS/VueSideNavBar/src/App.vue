<template>
  <div class="relative h-screen bg-neutral-100">
    <!--Top Nav should be sticky to the top , SideNav should be fixed to the left side-->
    <topnav />
    <SideNav />
    <!--Since side Nav will either be fixed or absolute, the underlying element will not be affected. So we have a empty div with same size as the side nav or just apply the width of side nav to below element as left margin-->
    <div class="ml-72 bg-red-500">I'm Main content</div>
  </div>
</template>

<script setup lang="ts">
 //To avoid loading in each component, we will install one more plugin from vue
import SideNav from '@/components/SideNav.vue';

</script>

<style scoped></style>
