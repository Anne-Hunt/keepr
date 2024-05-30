<script setup>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepService } from '../services/KeepService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { computed } from 'vue';

defineProps({keep: Keep})

const account = computed(()=> AppState.account)
const route = useRoute()
const owner = route.path.includes('Account')

function setActiveKeep(keepId){
    try {
      keepService.setActiveKeep(keepId)
      logger.log(AppState.activeKeep)
    }
    catch (error){
      Pop.error("Unable to set active keep", 'error');
      logger.log("unable to set active keep", error)
    }
}

async function trashKeep(keepId){
    try {
        const confirm = await Pop.confirm("Do you want to delete this keep? This cannot be undone.")
        if(!confirm){
            return
        }
        await keepService.trashKeep(keepId)
    }
    catch (error){
      Pop.error("Unable to remove keep", 'error');
      logger.log("unable to remove keep", error)
    }
}
</script>


<template>
          <div class="mb-3 rounded keepbox" data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep.id)">
            <i class="mdi mdi-close-circle text-end text-danger fs-3 top-right index" @click="trashKeep(keep.id)"></i> 
            <img class="imgView rounded" :src="keep.img" :alt="keep.name">
          <p class="text-light bottom-left m-0 keepName">{{ keep.name }}</p>
      </div>
</template>


<style lang="scss" scoped>
.imgView{
    width: 200px;
    height: auto;
    background-color: black;
    filter: contrast(.8);
    z-index: 1;
}

.index{
  z-index: 2;
}

.keepName{
  font-family: Quando, Verdana, serif;
}

.keepbox {
  position: relative;
}

.bottom-left{
  position: absolute;
  bottom: 8px;
  left: 16px;
  text-shadow: 0px 0px 10px black;
}

.quando{
  font-family: Quando, Verdana, serif;
}

.top-right{
    position: absolute;
  right: 15%;
}

</style>