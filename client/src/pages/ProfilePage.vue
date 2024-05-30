<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { profileService } from '../services/ProfileService.js';
import { useRoute } from 'vue-router';


const profile = computed(()=> AppState.activeProfile)
const vaults = computed(()=>AppState.profileVaults)
const keeps = computed(()=>AppState.keeps)
const vaultcount = computed(()=>AppState.profileVaults.length)
const keepcount = computed(()=>AppState.keeps.length)
const route = useRoute()

async function getProfileContent(){
  try {
    const profileId = route.params.profileId
    const vaults = await profileService.getVaultsByProfile(profileId)
    logger.log(vaults)
    const keeps = await profileService.getKeepsByProfile(profileId)
    logger.log(keeps)
  }
  catch (error){
    Pop.error("Unable to get stuff!");
    logger.log("Unable to get profile's vaults and keeps", error)
  }
}

async function getProfile(){
    try {
      const profileId = route.params.profileId
      await profileService.getProfile(profileId)
    }
    catch (error){
      Pop.error("Unable to find profile", 'error');
      logger.log("Unable to find profile", error)
    }
}

onMounted(()=>{
  getProfileContent()
  getProfile()
})
</script>


<template>
  <div class="container">
    <div class="row mb-3 coverImg justify-content-center align-items-end shadow"
      :style="{backgroundImage: `url(${profile?.coverImg})`}">
      <img class="profileImg rounded-circle shadow p-0 border border-dark mb-3" :src="profile?.picture" />
    </div>
    <div class="text-end"><a href="/home"><i class="mdi mdi-home"></i></a></div>
    <div class="row text-center mb-3">
      <h2>{{ profile?.name }}</h2>
      <div class="text-center">
        <span>{{ vaultcount }} Vaults | {{ keepcount }} Keeps</span>
      </div>
    </div>
    <div class="row">
      <h3 class="mb-3">Vaults</h3>
      <div v-if="!AppState.userVaults"><strong>Add a Vault</strong></div>
      <div class="col-12 masonry-with-columns">
        <section v-for="vault in vaults" :key="vault?.id">
          <ActiveVault :vault="vault"></ActiveVault>
        </section>
      </div>
      <div class="row ">
        <h3 class="mb-3">Keeps</h3>
        <div class="col-12 masonry-with-columns">
          <section v-for="keep in keeps" :key="keep?.id">
            <ActiveKeep :keep="keep"></ActiveKeep>
          </section>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.coverImg{
  height: 40dvh;
  background-position: center;
  background-size: cover;
  position:relative;
}
.profileImg{
  height: 100px;
  width: 100px;
  object-fit: cover;
  object-position: center;
  position:absolute;
  bottom: -50px;
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