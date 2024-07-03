<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { keepService } from '../services/KeepService.js';
import { useRoute, useRouter } from 'vue-router';
import { vaultService } from '../services/VaultService.js';
import VaultKeep from '../components/VaultKeep.vue';

const vault = computed(()=>AppState.activeVault)
const keeps = computed(()=>AppState.keepsInVault)
const route = useRoute()
const router = useRouter()
const vaultImg = computed(()=>`url(${AppState.activeVault?.img})`)
const keepsTotal = computed(()=> AppState.keepsInVault.length)
const owner = computed(()=> ((AppState.activeVault?.creatorId == AppState.account?.id) ? 'yes' : 'no'))

async function getVaultById(){
  try {
    const vaultId = route.params.vaultId
    await vaultService.getVaultById(vaultId)
  }
  catch (error){
    Pop.error("Unable to get this vault", 'error');
    logger.log("unable to get vault", error)
    router.push({name: 'Home'})
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

onMounted(()=>{
  getVaultById()
  getKeeps()
})
</script>


<template>
  <div class="container">

    <div class="row mb-3 justify-content-center">
      <div class="col-6 vault rounded" :style="{backgroundImage: `url(${vault?.img})`}">
        <div class="bottom-center text-light">
          <h4 class="marko text-uppercase">{{ vault?.name }}</h4>
          <h5 class="marko">by {{ vault?.creator.name }}</h5>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="text-center">
        <p v-if="keeps.length > 0">{{ keepsTotal }} Keeps</p>
      </div>
      <div v-if="owner == 'yes'" class="col-12 masonry-with-columns">
        <section v-for="keep in keeps" :key="keep.id">
          <AccountVaultKeep :keep="keep"></AccountVaultKeep>
        </section>
      </div>
      <div v-else class="col-12 masonry-with-columns">
        <section v-for="keep in keeps" :key="keep.id">
          <VaultKeep :keep="keep"></VaultKeep>
        </section>
      </div>
    </div>
  </div>
  </template>


<style lang="scss" scoped>
.vault{
    height: 30dvh;
    background-image: v-bind(vaultImg);
    background-size: cover;
    background-position: center;
    position: relative;
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