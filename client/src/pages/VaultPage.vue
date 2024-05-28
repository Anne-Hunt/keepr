<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { keepService } from '../services/KeepService.js';


const vault = computed(()=>AppState.activeVault)
const keeps = computed(()=>AppState.keeps)

async function getKeeps(){
try {
    const vaultId = AppState.activeVault.id
    await keepService.getKeepsByVault(vaultId)
}
catch (error){
  Pop.error("Unable to get keeps for this vault", 'error');
  logger.log("unable to get keeps for vault", error)
}
}

onMounted(()=>{
    getKeeps()
})
</script>


<template>
<div class="container">
<div class="row mb-3 justify-content-center">
    <div class="col-6 vault" :style="{backgroundImage: `url(${vault?.img})`}">
        <h4>{{ vault?.name }}</h4>
        <h5>by {{ vault?.creator.name }}</h5>
    </div>
</div>
<div class="row ">
      <div class="col-12 masonry-with-columns">
        <section v-for="keep in keeps" :key="keep.id">
          <ActiveKeep :keep="keep"></ActiveKeep>
      </section>
    </div>
  </div>
</div>
</template>


<style lang="scss" scoped>
.vault{
    height: 30dvh;
}

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