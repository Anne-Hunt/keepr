<script setup>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { profileService } from '../services/ProfileService.js';
import { vaultKeepService } from '../services/VaultKeepService.js';
import { keepService } from '../services/KeepService.js';
import { accountService } from '../services/AccountService.js';
import { Modal } from 'bootstrap';


const keep = computed(()=> AppState.activeKeep)
const account = computed(()=> AppState.account)
const userVaults = computed (()=>AppState.userVaults)
const vaultKeepForm = ref({
    vaultId: '',
    keepId: ''
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

function unsetActiveKeep(){
    try {
      keepService.unsetActiveKeep()
    }
    catch (error){
      Pop.error("Unable to forget keep", 'error');
      logger.log("unable to unset active Keep", error)
    }
}

async function createVaultKeep(keepId){
try {
    vaultKeepForm.value.keepId = keepId
    await vaultKeepService.createVaultKeep(vaultKeepForm.value)
    Modal.getOrCreateInstance('#keepModal').hide
}
catch (error){
    Pop.error("Unable to keep in vault", 'error');
    logger.log("Unable to add to vault", error)
}}

async function getUserVaults(){
    try {
      if(userVaults.value){
        return
      }
      await accountService.getMyVaults()
    }
    catch (error){
        Pop.error("Unable to get your vaults", 'error');
    logger.log("Unable to get vaults", error)
    }
}

onMounted(()=>{
    getUserVaults()
})
</script>

<template>
    <div class="row keepInfo rounded">
        <div class="col-md-6 col-12 order-1 rounded-start keepImg" :style="{backgroundImage:`url(${keep?.img})`}">
        </div>
        <div class="col-md-6 col-12 order-2 bg-light rounded-end">
            <div class="row py-2">
                <div class="row">
                    <div class="col-10 text-center">
                        <i class="mdi mdi-eye"></i>{{ keep?.views }} | <i class="mdi mdi-alpha-k-box-outline"></i>
                        {{ keep?.kept }}
                    </div>
                    <div class="col-1 text-center">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" @click="unsetActiveKeep()"></button>
                    </div>
                </div>
            </div>
            <div class="contentInfo row align-items-center">
                <div class="p-3">
                    <h2 id="keepModalLabel" class="text-center">{{ keep?.name }}</h2>
                    <p>{{ keep?.description }}</p>
                </div>
            </div>
            <div class="row justify-content-between align-items-center">
                <div class="col-8">
                    <form @submit.prevent="createVaultKeep(keep?.id)">
                        <div class="input-group">
                            <select class="form-select" id="vaultSelect" aria-label="Example select with button addon" v-model="vaultKeepForm.vaultId">
                                <option v-for="userVault in userVaults" :key="userVault?.id" :value="userVault?.id">
                                    {{userVault?.name}}</option>
                                    <option v-if="!userVaults"><a href="">Add a Vault</a></option>
                                </select>
                                <button class="btn btn-outline-secondary" type="submit"><i class="mdi mdi-folder"></i></button>
                            </div>
                        </form>
                </div>
                <div class="col-3">
                    <RouterLink v-if="!AppState.account || AppState.account.id != AppState.activeKeep?.creatorId" :to="{name: 'Profile', params: {profileId: `${keep?.creatorId}`}}"
                    @click="setActiveProfile(keep?.creatorId)">
                    <div class="profileImg rounded-circle" data-bs-dismiss="modal" :style="{backgroundImage: `url(${keep?.creator.picture})`}">
                    </div>
                </RouterLink>
                <RouterLink v-else :to="{name: 'Account'}">
                    <div class="profileImg rounded-circle" data-bs-dismiss="modal" :style="{backgroundImage: `url(${keep?.creator.picture})`}">
                    </div>
                </RouterLink>
                {{ keep?.creator.name }}
                </div>
        </div>
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
.keepImg{
    height: 80dvh;
    background-size: cover;
    background-position: center;
}

.keepInfo{
    height: 80dvh;
}

.contentInfo{
    height: 60dvh;
}
</style>