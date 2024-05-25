<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import VaultCard from '../components/VaultCard.vue';
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
</script>


<template>
<div class="container">
<div class="row mb-3" :style="{backgroundImage: `url(${vault?.img})`}"></div>
<div class="row">
    <h3 class="mb-3">Keeps</h3>
    <div class="col-4 mb-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard/>
    </div>
</div>
</div>
</template>


<style lang="scss" scoped>

</style>