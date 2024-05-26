<script setup>
import { computed, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { keepService } from '../services/KeepService.js';
import KeepCard from '../components/KeepCard.vue';

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

onMounted(()=>
getKeeps())
</script>

<template>
  <div class="container">
    <section class="row">
      <div class="col-12 masonry">
        <div v-for="keep in keeps" :key="keep.id" class="mb-3">
          <KeepCard :keep="keep"/>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">
.masonry {
  columns: 250px;
  column-gap: 2rem;
}
</style>
