<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { keepService } from '../services/KeepService.js';


const vault = computed(()=>AppState.activeVault)
const keeps = computed(()=>AppState.keeps)

// async function getKeeps(){
// try {
//     const vaultId = AppState.activeVault.id
//     await keepService.getKeepsByVault(vaultId)
// }
// catch (error){
//   Pop.error("Unable to get keeps for this vault", 'error');
//   logger.log("unable to get keeps for vault", error)
// }
// }

// onMounted(()=>{
//     getKeeps()
// })
</script>


<template>
<div class="container">
<div class="row mb-3">
    <div class="col-6 vault" :style="{backgroundImage: `url(${vault?.img})`}">
        <h4>{{ vault.name }}</h4>
        <h5>{{ vault.creator.name }}</h5>
    </div>
</div>
<div class="row">
    <h3 class="mb-3">Keeps</h3>
    <div class="col-4 mb-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard/>
    </div>
</div>
</div>
</template>


<style lang="scss" scoped>
.vault{
    height: 30dvh;
}
</style>