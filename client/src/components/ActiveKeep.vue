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
          <div class="mb-3 rounded" data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep.id)" :style=" {backgroundImage: `url(${keep.img})`}">
          <h4 class="text-light">{{ keep.name }}</h4>
      </div>
</template>


<style lang="scss" scoped>

</style>