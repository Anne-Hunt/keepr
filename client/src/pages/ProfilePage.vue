<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import VaultCard from '../components/VaultCard.vue';
import KeepCard from '../components/KeepCard.vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { accountService } from '../services/AccountService.js';
import { profileService } from '../services/ProfileService.js';



const profile = computed(()=> AppState.activeProfile)
const vaults = computed(()=>AppState.vaults)
const keeps = computed(()=>AppState.keeps)
const vaultcount = computed(()=>AppState.vaults.length)
const keepcount = computed(()=>AppState.keeps.length)

async function getProfileContent(){
  try {
    const vaults = await profileService.getVaultsByProfile()
    logger.log(vaults)
    const keeps = await profileService.getKeepsByProfile()
    logger.log(keeps)
  }
  catch (error){
    Pop.error("Unable to get stuff!");
    logger.log("Unable to get profile's vaults and keeps", error)
  }
}

onMounted(()=>{
  getProfileContent()
})
</script>


<template>
<div class="container">
<div class="row mb-3 coverImg justify-content-center align-items-end shadow" :style="{backgroundImage: `url(${profile?.coverImg})`}">
  <img class="coverImg rounded-circle shadow" :src="profile?.picture"/>
</div>
<div class="row justify-content-center mb-3">
<h2>{{ profile?.name }}</h2>
<div v-if="vaults" class="col-6">
  <h5>{{ vaultcount }} Vaults</h5>
</div>
<span v-if="vaults && keeps">|</span>
<div v-if="keeps" class="col-6">
  <h5>{{ keepcount }} Keeps</h5>
</div>
</div>
<div class="row">
    <h3 class="mb-3">Vaults</h3>
    <div class="col-4 mb-3" v-for="vault in vaults" :key="vault.id">
        <VaultCard/>
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

</style>