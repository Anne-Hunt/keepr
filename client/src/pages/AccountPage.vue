<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import VaultCard from '../components/VaultCard.vue';
import KeepCard from '../components/KeepCard.vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { accountService } from '../services/AccountService.js';

const account = computed(()=> AppState.account)
const vaults = computed(()=>AppState.vaults)
const keeps = computed(()=>AppState.keeps)
const vaultcount = computed(()=>AppState.vaults.length)
const keepcount = computed(()=>AppState.keeps.length)

async function getMyVaults(){
  try {
    const vaults = await accountService.getMyVaults()
    logger.log(vaults)
  }
  catch (error){
    Pop.error("Unable to get your vaults!", 'error');
    logger.log("Unable to get vaults", error)
  }
}

async function getMyKeeps(){
  try {
    const keeps = await accountService.getMyKeeps()
    logger.log(keeps)
  }
  catch (error){
    Pop.error("Unable to get your keeps!");
    logger.log("Unable to get keeps", error)
  }
}

// async function getMyVaultKeeps(){
//   try {
//     const vaultkeeps = await accountService.getMyVaultKeeps()
//     logger.log(vaultkeeps)
//   }
//   catch (error){
//     Pop.error("Unable to get your kept content!");
//     logger.log("Unable to get vaultkeeps", error)
//   }
// }

onMounted(()=>{
  getMyVaults()
  getMyKeeps()
  // getMyVaultKeeps()
})
</script>


<template>
<div class="container">
<div class="row mb-3 coverImg justify-content-center align-items-end shadow" :style="{backgroundImage: `url(${account?.coverImg})`}">
  <img class="coverImg rounded-circle shadow" :src="account?.picture"/>
</div>
<div class="row justify-content-center mb-3">
<h2>{{ account?.name }}</h2>
<div v-if="vaults" class="col-6">
  <span>{{ vaultcount }} Vaults</span>
</div>
<span v-if="vaults && keeps">|</span>
<div v-if="keeps" class="col-6">
  <span>{{ keepcount }} Keeps</span>
</div>
</div>
<div class="row">
    <h3 class="mb-3">Vaults</h3>
    <div class="col-4 mb-3" v-for="vault in vaults" :key="vault?.id">
        <VaultCard :vault="vault"/>
    </div>
</div>
<div class="row">
    <h3 class="mb-3">Keeps</h3>
    <div class="col-4 mb-3" v-for="keep in keeps" :key="keep?.id">
        <KeepCard :keep="keep"/>
    </div>
</div>
</div>
</template>


<style lang="scss" scoped>
.coverImg{
  height: 40dvh;
}
.profileImg{
  height: 20dvh;
  width: 20dvh;
  object-fit: cover;
  object-position: center;
}
</style>