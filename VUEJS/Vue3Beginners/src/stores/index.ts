import {defineStore} from 'pinia';
import {ref} from 'vue';

// const datastore = defineStore("data",()=>{
// const salary = ref<number>(8500);

// return {salary};
// });


const datastore = defineStore('data', {
    state: () => ({
        salary: Number(localStorage.getItem('salary')?.toString()) ?? 8500, 
        }),
        actions:{
            storeinLocal(){
                localStorage.setItem('salary', JSON.stringify(this.salary));
            },
            clearLocal(){
                localStorage.removeItem('salary');
            }
        }
  })
  
export {datastore}
