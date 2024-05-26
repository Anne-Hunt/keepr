<script setup>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { profileService } from '../services/ProfileService.js';
import { vaultKeepService } from '../services/VaultKeepService.js';


const keep = computed(()=> AppState.activeKeep)
const account = computed(()=> AppState.account)
const userVaults = computed (()=>AppState.userVaults)
const vaultKeepForm = ref({
    vaultId: '',
    keepId: keep?.value.id,
    creatorId: account?.value.id
})

async function setActiveProfile(profileId){
    try {
      await profileService.getProfile(profileId)
    }
    catch (error){
      Pop.error("Unable to find profile", 'error');
      logger.log("Unable to find profile", error)
    }
}

async function createVaultKeep(){
try {
    const formData = vaultKeepForm.value
    await vaultKeepService.createVaultKeep(formData)
}
catch (error){
    Pop.error("Unable to keep in vault", 'error');
    logger.log("Unable to add to vault", error)
}}
</script>


<template>
    <div class="col-6 rounded-start" :style="{backgroundImage: `url(${keep?.img})`}"></div>
    <div class="col-6">
        <div class="row">
            <div class="col-11 text-center">
                <i class="mdi mdi-eye"></i>{{ keep?.views }} | <i class="mdi mdi-alpha-k-box-outline"></i>
                {{ keep?.kept }}
            </div>
            <div class="col-1 text-end">
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
        </div>
        <div class="row">
        </div>
        <div class="row justify-content-between">
            <form @submit.prevent="createVaultKeep()">
                <div class="input-group">
                    <select class="form-select" id="inputGroupSelect04" aria-label="Example select with button addon">
                        <option v-for="userVault in userVaults" :key="userVault?.id" :value="userVault?.id">
                            {{userVault?.name}}</option>
                        <option v-if="!userVaults"><a href="">Add a Vault</a></option>
                    </select>
                    <button class="btn btn-outline-secondary" type="button">Button</button>
                </div>
            </form>
            <RouterLink v-if="keep.creatorId = account.id" :to="{name: 'Account'}">
                <div class="profileImg rounded-circle" :style="{backgroundImage: `url(${keep?.creator.picture})`}">
                </div>
            </RouterLink>
            <RouterLink v-if="keep.creatorId != account.id" :to="{name: 'Profile', params: {profileId: keep.creatorId}}"
                @click="setActiveProfile(keep?.creatorId)">
                <div class="profileImg rounded-circle" :style="{backgroundImage: `url(${keep?.creator.picture})`}">
                </div>
            </RouterLink>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.profileImg{
    height: 50px;
    width: 50px;
    background-position: center;
    background-size: cover;
}
</style>