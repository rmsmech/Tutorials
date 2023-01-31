<template>
  <div class="h-full w-full bg-green-200">
    <input
      class="m-1 cursor-pointer rounded-md bg-blue-300 p-1 hover:bg-blue-600 hover:text-white"
      type="button"
      value="Authorize Me"
      @click="authorize"
    />

    <span :class="auth.is_authorized ? 'text-green-600' : 'text-red-500'">{{
      auth.is_authorized
    }}</span>
  </div>
</template>

<script setup lang="ts">
//We can import ref,router,route and other items from vue,vue-router,pinia each time. Or use auto plugin to do it once.
//Now, we don't need to import ref keyword
import authStore from "@/@Stores/authStore";

const auth = authStore.useAuthStore();

// const isAuthorized = ref<boolean>(false); //this state remains only in this component. So store it in pini store to share with other comps

function getRandomString(): string {
  return Math.random().toString(36).substring(2, 10);
}

function authorize() {
  let uname = getRandomString();
  auth.authenticate(!auth.is_authorized, uname); //either give direct value or invert it.
}
</script>

<style scoped></style>
