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

</script>


<template>
          <div class="mb-3 rounded keepbox" data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep.id)"> 
            <img class="imgView rounded" :src="keep.img" :alt="keep.name">
          <p class="text-light bottom-left fs-5 m-0 keepName">{{ keep.name }}</p>
      </div>
</template>


<style lang="scss" scoped>
.imgView{
    width: 200px;
    height: auto;
    background-color: black;
    filter: contrast(.8);
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
  top: 8px;
  right: 16px;
}

</style>