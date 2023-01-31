<template>
  <!--The side nav bar should be fixed. It should cover the screen height. It should allow space for the top nav .-->
  
  <div class="fixed w-72 bg-white  rounded-3xl m-2 top-16 bottom-2 left-2 shadow p-2.5 flex flex-col gap-1 text-slate-600 ">
    <div class="flex items-center gap-2  rounded-xl px-2 py-1 hover:bg-sky-100 cursor-pointer hover:text-sky-500">
        <i class='bx bxs-home text-2xl'></i>
        <span>Home</span>
    </div>
  </div>

</template>

<script setup lang="ts">

//Since we have installed auto import, we dont' have to import ref,onmounted and other methods from vue . They will be loaded when built.
</script>

<style scoped></style>
