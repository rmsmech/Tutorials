import * as pin from "pinia";
import { ref } from "vue";

const useAuthStore = pin.defineStore("auth", () => {
  const is_authorized = ref<boolean>(false);
  const username = ref<string>();

  function authenticate(status: boolean,user_name?:string) {
    is_authorized.value = status;
    username.value = user_name;
    console.log(is_authorized,user_name);
  }

  return {
    //State
    is_authorized,
    username,

    //Actions
    authenticate,
  };
});

export default {
  useAuthStore,
};
