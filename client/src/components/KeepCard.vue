<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepService } from '../services/KeepService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

defineProps({keep: Keep})

const account = computed(()=> AppState.account)

function setActiveKeep(keepId){
    try {
      keepService.setActiveKeep(keepId)
    }
    catch (error){
      Pop.error("Unable to set active vault", 'error');
      logger.log("unable to set active vault", error)
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
    <div>
        <!-- <i v-if="account.id = keep?.creatorId" class="mdi mdi-close-circle text-end text-danger" @click="trashKeep(keep?.id)"></i> -->
        <div data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep?.id)">
    <div class="card keep p-1 d-flex justify-content-end" :style="{backgroundImage: `url(${keep?.img})`}">
        <h4 class="text-light">{{ keep.name }}</h4>
    </div>
    </div>
    </div>
</template>


<style lang="scss" scoped>
// .keep{
//     max-height: 50dvh;
//     min-height: 30dvh;
// }
//FIXME Add filter to css
</style>