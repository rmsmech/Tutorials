<script setup lang="ts">
import { ref } from 'vue'

defineProps<{ msg: string }>()

const count = ref(0)
</script>

<template>
  <h1>{{ msg }}</h1>

  <div class="card flex flex-col items-center gap-8">
    <button type="button" class="bg-slate-600 p-3 rounded-2xl text-white shadow-lg shadow-blue-400 hover:border-2" @click="count++">Test count is <span class="text-yellow-300">{{ count }}</span> </button>

    <img class="w-24" alt="testimage" src="@/assets/avtr_boy_02.jpg"/>
  </div>

</template>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>
