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
</script>


<template>
          <div class="mb-3 rounded keepbox" data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveVault(vault.id)">
            <img class="imgView rounded" :src="vault.img" :alt="vault.name">
          <p class="text-light bottom-left fs-3 m-0">{{ vault.name }}</p>
      </div>
</template>


<style lang="scss" scoped>
.imgView{
    width: 200px;
    height: auto;
    background-color: black;
    filter: contrast(.8);
}
.keepbox {
  position: relative;
}

.bottom-left{
  position: absolute;
  bottom: 8px;
  left: 16px;
  text-shadow: 0px 0px 10px black;
}
</style>