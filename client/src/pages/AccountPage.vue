<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { accountService } from '../services/AccountService.js';
import AccountVault from '../components/AccountVault.vue';

const account = computed(()=> AppState.account)
const vaults = computed(()=>AppState.userVaults)
const keeps = computed(()=>AppState.keeps)
const vaultcount = computed(()=>AppState.userVaults.length)
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

onMounted(()=>{
  getMyVaults()
  getMyKeeps()
  // getMyVaultKeeps()
})
</script>


<template>
  <div class="container">
    <div class="row mb-3 coverImg justify-content-center align-items-end shadow"
      :style="{backgroundImage: `url(${account?.coverImg})`}">
      <img class="profileImg rounded-circle shadow p-0 border border-dark" :src="account?.picture" />
    </div>
    <div class="text-end" role="button" data-bs-target="#accountForm" data-bs-toggle="modal"><i class="mdi mdi-dots-horizontal fs-3"></i></div>
    <div class="row text-center mb-3">
      <h2>{{ account?.name }}</h2>
      <div class="text-center">
        <span>{{ vaultcount }} Vaults | {{ keepcount }} Keeps</span>
      </div>
    </div>
    <div class="row">
      <h3 class="mb-3">Vaults</h3>
      <div v-if="!vaults"><strong>Add a Vault</strong></div>
      <div class="col-12 masonry-with-columns">
        <section v-for="vault in vaults" :key="vault?.id">
          <AccountVault :vault="vault"></AccountVault>
        </section>
      </div>
      <div class="row ">
        <h3 class="my-3">Keeps </h3>
        <div v-if="!keeps"><strong>Add a Keep</strong></div>
        <div class="col-12 masonry-with-columns">
          <section v-for="keep in keeps" :key="keep?.id">
            <AccountKeep :keep="keep"></AccountKeep>
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
  background-color: black;
    filter: contrast(.8);
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