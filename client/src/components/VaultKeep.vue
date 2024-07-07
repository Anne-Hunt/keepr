<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepService } from '../services/KeepService.js';
import { vaultKeepService } from '../services/VaultKeepService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

defineProps({keep: Keep})

const vault = computed(()=> AppState.activeVault)
const account = computed(()=> AppState.account)

function setActiveKeep(keepId){
    try {
      keepService.setActiveKeep(keepId)
      logger.log(AppState.activeKeep)
      Pop.success("Added to your vault!")
    }
    catch (error){
      Pop.error("Unable to set active keep", 'error');
      logger.log("unable to set active keep", error)
    }
}

async function trashKeep(vaultkeepId){
    try {
        const confirm = await Pop.confirm("Do you want to delete this keep? This cannot be undone.")
        if(!confirm){
            return
        }
        await vaultKeepService.trashVaultKeep(vaultkeepId)
    }
    catch (error){
      Pop.error("Unable to remove keep", 'error');
      logger.log("unable to remove keep", error)
    }
}
</script>


<template>
          <div class="mb-3 rounded keepbox" data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep.id)">
            <img class="imgView rounded" :src="keep.img" :alt="keep.name">
          <p class="text-light bottom-left fs-5 m-0 keepName">{{ keep.name }}</p>
      </div>
</template>


<style lang="scss" scoped>
.imgView{
    width: 200px;
    height: auto;
    background-color: black;
    filter: contrast(.8);
    z-index: 1;
}

.index{
  z-index: 2;
}

.keepName{
  font-family: Quando, Verdana, serif;
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

.quando{
  font-family: Quando, Verdana, serif;
}

.top-right{
    position: absolute;
  right: 15%;
}

</style>