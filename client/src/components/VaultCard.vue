<script setup>
import { computed } from 'vue';
import { Vault } from '../models/Vault.js';
import { keepService } from '../services/KeepService.js';
import { vaultService } from '../services/VaultService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { useRoute } from 'vue-router';

defineProps({vault: Vault})
const account = computed(()=> AppState.account)
const route = useRoute()

async function setActiveVault(){
    try {
      const vaultId = route.params.vaultId
      vaultService.setActiveVault(vaultId)
      await keepService.getKeepsByVault(vaultId)
    }
    catch (error){
      Pop.error("Unable to set active vault", 'error');
      logger.log("unable to set active vault", error)
    }
}

async function trashVault(){
  try {
      const vaultId = route.params.vaultId
    const confirm = await Pop.confirm("Do you really want to remove this vault and its contents? This cannot be undone.")
    if(!confirm){
      return
    }
    await vaultService.TrashVault(vaultId)
  }
  catch (error){
      Pop.error("Unable to remove vault", 'error');
      logger.log("unable to remove vault", error)
    }
}
</script>

<template>
    <RouterLink :to="{name: 'Vault', params: {vaultId: vault.id}}" @click="setActiveVault()">
      <i v-if="account.id = vault?.creatorId" class="mdi mdi-close-circle text-end text-danger" @click="trashVault()"></i>
<div class="card vault p-1 d-flex justify-content-end" :style="{backgroundImage: `url(${vault?.img})`}">
   <div class="d-flex justify-content-between text-light">
    <h4 class="">{{ vault?.name }}</h4> <i class="mdi mdi-lock" @pointerover.stop=""></i>
   </div>
</div>
</RouterLink>
</template>

<style lang="scss" scoped>
.vault{
    height: 30dvh;
    background-position: center;
    background-size: cover;
}

//FIXME Add filter to css
</style>