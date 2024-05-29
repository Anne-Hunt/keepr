<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepService } from '../services/KeepService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { useRoute } from 'vue-router';

defineProps({keep: Keep})

const account = computed(()=> AppState.account)
const route = useRoute()
const owner = route.path.includes('Account')

function setActiveKeep(){
    try {
        const keepId = route.params.keepId
      keepService.setActiveKeep(keepId)
    }
    catch (error){
      Pop.error("Unable to set active vault", 'error');
      logger.log("unable to set active vault", error)
    }
}

async function trashKeep(){
    try {
        const keepId = route.params.keepId
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
        <div class="parent" data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep()">
            <i v-if="owner" class="mdi mdi-close-circle text-end text-danger fs-1 child" @click="trashKeep()"></i>        
    <div class="card keepProfile p-1 d-flex justify-content-end" :style="{backgroundImage: `url(${keep?.img})`}">
        <!-- <img class="" :style="{backgroundImage: `url(${keep?.img})`}"/> -->
        <h4 class="text-light quando">{{ keep?.name }}</h4>
    </div>
    </div>
    </div>
</template>


<style lang="scss" scoped>
.keepProfile{
    min-height: 20dvh;
    width: 30dvh;
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
}

.parent{
    position: relative;
}

.child{
    position: absolute;
  top: 8px;
  right: 16px;
}

.quando{
  font-family: Quando, Verdana, serif;
}
</style>