<script setup>
import { Keep } from '../models/Keep.js';
import { keepService } from '../services/KeepService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

defineProps({keep: Keep})

function setActiveKeep(keepId){
    try {
      keepService.setActiveKeep(keepId)
    }
    catch (error){
      Pop.error("Unable to set active vault", 'error');
      logger.log("unable to set active vault", error)
    }
}
</script>


<template>
    <div data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep?.id)">
<div class="card vault p-1 d-flex justify-content-end" :style="{backgroundImage: `url(${keep?.img})`}">
    <h4 class="text-light">{{ keep.name }}</h4>
</div>
</div>
</template>


<style lang="scss" scoped>
.vault{
    height: 30dvh;
}

//FIXME Add filter to css
</style>