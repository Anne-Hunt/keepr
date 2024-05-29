<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { keepService } from '../services/KeepService.js';
import { useRoute, useRouter } from 'vue-router';
import { vaultService } from '../services/VaultService.js';
import { vaultKeepService } from '../services/VaultKeepService.js';

const vault = computed(()=>AppState.activeVault)
const keeps = computed(()=>AppState.keepsInVault)
const route = useRoute()
const router = useRouter()
const vaultImg = computed(()=>`url(${AppState.activeVault?.img})`)
const keepsTotal = computed(()=> AppState.keepsInVault.length)


async function getVaultById(){
  try {
    const vaultId = route.params.vaultId
    await vaultService.getVaultById(vaultId)
  }
  catch (error){
    router.push({name: 'Home'})
    Pop.error("Unable to get this vault", 'error');
  logger.log("unable to get vault", error)
  }
}
async function getKeeps(){
try {
    const vaultId = route.params.vaultId
    await keepService.getKeepsByVault(vaultId)
}
catch (error){
  Pop.error("Unable to get keeps for this vault", 'error');
  logger.log("unable to get keeps for vault", error)
}
}

async function getKeptKeepsByVault(){
  try {
    const vaultId = route.params.vaultId
    await vaultKeepService.getKeepsAndVaultKeepsByVault(vaultId)
  }
  catch (error){
    Pop.error("Unable to get vaultkeeps for this vault", 'error');
  logger.log("unable to get vaultkeeps for vault", error)
  }
}

onMounted(()=>{
  getVaultById()
    getKeeps()
    getKeptKeepsByVault()
})
</script>


<template>
  <div class="row mb-3 justify-content-center">
    <div class="col-6 vault rounded" :style="{backgroundImage: `url(${vault?.img})`}">
      <div class="bottom-center text-light">
        <h4 class="marko text-uppercase">{{ vault?.name }}</h4>
        <h5 class="marko">by {{ vault?.creator.name }}</h5>
      </div>
    </div>
  </div>
  <div class="row ">
    <div class="col">
      <p v-if="keeps.length > 0">{{ keepsTotal }} Keeps</p>
    </div>
    <div class="col-12 masonry-with-columns">
      <section v-for="keep in keeps" :key="keep.id">
        <ActiveKeep :keep="keep"></ActiveKeep>
      </section>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.vault{
    height: 30dvh;
    background-image: v-bind(vaultImg);
    background-size: cover;
    background-position: center;
    position: fixed;
    background-color: black;
    filter: contrast(.8);
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

.marko{
  font-family: Marko One, Verdana, serif;
}

.bottom-center{
    position: absolute;
    bottom: 8px;
}
</style>