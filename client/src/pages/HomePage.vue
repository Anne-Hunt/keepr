divsectiondiv<script setup>
import { computed, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { keepService } from '../services/KeepService.js';
import { accountService } from '../services/AccountService.js';

const keeps = computed(()=>AppState.keeps)

async function getKeeps(){
  try {
    await keepService.getKeeps()
  }
  catch (error){
    Pop.error("Unable to load keeps!", 'error');
    logger.log("Unable to load keeps on home page", error)
  }
}

async function getUserVaults(){
    try {
      if(AppState.userVaults){
        return
      }
      await accountService.getMyVaults()
    }
    catch (error){
        Pop.error("Unable to get your vaults", 'error');
    logger.log("Unable to get vaults", error)
    }
}

onMounted(()=>{
getKeeps()
getUserVaults()})
</script>

<template>
  <div class="container">
    <div class="row ">
      <div class="col-12 masonry-with-columns">
        <section v-for="keep in keeps" :key="keep.id">
          <ActiveKeep :keep="keep"></ActiveKeep>
      </section>
    </div>
  </div>
  </div>
</template>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    height: auto;
    color: white;
    display: inline-block;
    width: 100%;
    text-align: left;
  } 
}
</style>
