<script setup>
import { Vault } from '../models/Vault.js';
import { keepService } from '../services/KeepService.js';
import { vaultService } from '../services/VaultService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

defineProps({vault: Vault})

async function setActiveVault(vaultId){
    try {
      vaultService.setActiveVault(vaultId)
      await keepService.getKeepsByVault(vaultId)
    }
    catch (error){
      Pop.error("Unable to set active vault", 'error');
      logger.log("unable to set active vault", error)
    }
}
</script>


<template>
    <RouterLink :to="{name: 'Vault', params: {vaultId: vault.id}}" @click="setActiveVault(vault?.id)">
<div class="card vault p-1 d-flex justify-content-end" :style="{backgroundImage: `url(${vault?.img})`}">
    <h4 class="text-light">{{ vault.name }}</h4>
</div>
</RouterLink>
</template>

<style lang="scss" scoped>
.vault{
    height: 30dvh;
}

//FIXME Add filter to css
</style>