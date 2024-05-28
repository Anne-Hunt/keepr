<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { vaultService } from '../services/VaultService.js';
import { AppState } from '../AppState.js';
import { Modal } from 'bootstrap';

const activeVault = computed(()=>AppState.activeVault)

const vaultData = ref({
    name: '',
    img: '',
    isPrivate: false
})

async function createVault(){
    try {
      await vaultService.createVault(vaultData.value)
      Modal.getOrCreateInstance('#vaultForm').hide
      resetForm()
    }
    catch (error){
      Pop.error("Unable to create Vault at this time", 'error');
      logger.log("failed to make vault", error)
    }
}

async function updateVault(){
    try {
      await vaultService.updateVault(vaultData.value)
      Modal.getOrCreateInstance('#vaultForm').hide
      resetForm()
    }
    catch (error){
      Pop.error("Unable to update Vault at this time", 'error');
      logger.log("failed to update vault", error)
    }
}

function resetForm(){
    vaultData.value.name = ''
    vaultData.value.img = ''
    vaultData.value.isPrivate = false
}

function values(){
    vaultData.value.name = AppState.activeVault?.name
    vaultData.value.img = AppState.activeVault?.img
    vaultData.value.isPrivate = AppState.activeVault?.isPrivate

}

onMounted(()=>{
    values()
})
</script>


<template>
<form v-if="!activeVault" @submit.prevent="createVault()">
    <div class="mb-3">
        <input type="text" v-model="vaultData.name" class="w-100" placeholder="Title ...">
    </div>
    <div class="mb-3">
        <input type="text" v-model="vaultData.img" class="w-100" placeholder="Image URL ...">
    </div>
    <div class="text-end">
        <p class="p-0"><small>Private vaults can only be seen by you</small></p>
    </div>
    <div class="text-end d-flex justify-content-end text-end">

        <div class="form-check mb-3">
            <input class="form-check-input" type="checkbox" v-model="vaultData.isPrivate" value="" id="privateVault">
            <label class="form-check-label" for="flexCheckDefault">
                Make Vault Private?
            </label>
        </div>
    </div>
    <div class="d-flex justify-content-end">
        <button class="btn btn-dark text-light text-end" type="submit">Create Vault</button>
    </div>
</form>

<form v-else @submit.prevent="updateVault()">
    <div class="mb-3">
        <input type="text" v-model="vaultData.name" class="w-100" >
    </div>
    <div class="mb-3">
        <input type="text" v-model="vaultData.img" class="w-100">
    </div>
    <div class="text-end">
        <p class="p-0"><small>Private vaults can only be seen by you</small></p>
    </div>
    <div class="text-end d-flex justify-content-end text-end">

        <div class="form-check mb-3">
            <input class="form-check-input" type="checkbox" v-model="vaultData.isPrivate" value="" id="privateVault">
            <label class="form-check-label" for="flexCheckDefault">
                Make Vault Private?
            </label>
        </div>
    </div>
    <div class="d-flex justify-content-end">
        <button class="btn btn-dark text-light text-end" type="submit">Update Vault</button>
    </div>
</form>
</template>


<style lang="scss" scoped>

</style>