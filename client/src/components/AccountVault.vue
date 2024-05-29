<script setup>
import { AppState } from '../AppState.js';
import { Vault } from '../models/Vault.js';
import { vaultService } from '../services/VaultService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

defineProps({vault: Vault})

function setActiveVault(vaultId){
    try {
      vaultService.setActiveVault(vaultId)
      logger.log(AppState.activeVault)
    }
    catch (error){
      Pop.error("Unable to set active vault", 'error');
      logger.log("unable to set active vault", error)
    }
}

async function trashVault(vaultId){
    try {
        const confirm = await Pop.confirm("Do you want to delete this vault? This cannot be undone.")
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
  <RouterLink :to="{name: 'Vault', params:{vaultId: vault.id}}">
    <div class="mb-3 rounded keepbox" @click="setActiveVault(vault.id)">
      <i class="mdi mdi-close-circle text-end text-danger fs-3 top-right index" @click="trashVault(vault.id)"></i> 
      <img class="imgView rounded" :src="vault.img" :alt="vault.name">
    <p class="text-light bottom-left text-uppercase m-0">{{ vault.name }}</p>
</div>
  </RouterLink>
</template>


<style lang="scss" scoped>
.imgView{
    width: 200px;
    height: 150px;
    background-color: black;
    filter: contrast(.8);
    z-index: 1;
}

.keepbox {
  position: relative;
  font-family: Marko, Quando, Verdana, serif;
}

.bottom-left{
  position: absolute;
  bottom: 8px;
  left: 16px;
  text-shadow: 0px 0px 10px black;
}
.index{
  z-index: 2;
}

.top-right{
    position: absolute;
  right: 15%;
}
</style>