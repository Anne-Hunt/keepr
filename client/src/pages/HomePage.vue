<script setup>
import { computed, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { vaultService } from '../services/VaultService.js';

const vaults = computed(()=>AppState.vaults)

async function getVaults(){
  try {
    await vaultService.getVaults()
  }
  catch (error){
    Pop.error("Unable to load vaults!", 'error');
    logger.log("Unable to load vaults on home page", error)
  }
}

onMounted(()=>
getVaults())
</script>

<template>
  <div class="container">
    <section class="row">
      <div class="col-4">
        <div v-for="vault in vaults" :key="vault.id">

          <VaultCard :vault="vault"/>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">

</style>
