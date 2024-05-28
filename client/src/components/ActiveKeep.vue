<script setup>
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepService } from '../services/KeepService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

defineProps({keep: Keep})

function setActiveKeep(keepId){
    try {
      keepService.setActiveKeep(keepId)
      logger.log(AppState.activeKeep)
    }
    catch (error){
      Pop.error("Unable to set active vault", 'error');
      logger.log("unable to set active vault", error)
    }
}
</script>


<template>
          <div class="mb-3 rounded keepbox" data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep.id)">
            <img class="imgView rounded" :src="keep.img" :alt="keep.name">
          <p class="text-light bottom-left fs-3 m-0">{{ keep.name }}</p>
      </div>
</template>


<style lang="scss" scoped>
.imgView{
    width: 200px;
    height: auto;
    background-color: black;
    filter: contrast(.8);
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
</style>