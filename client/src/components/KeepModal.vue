<script setup>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { profileService } from '../services/ProfileService.js';
import { vaultKeepService } from '../services/VaultKeepService.js';
import { keepService } from '../services/KeepService.js';


const keep = computed(()=> AppState.activeKeep)
const account = computed(()=> AppState.account)
const userVaults = computed (()=>AppState.userVaults)
const vaultKeepForm = ref({
    vaultId: '',
    keepId: 0,
    creatorId: ''
})

const owner = computed(()=> {if(account.value)AppState.account.id = AppState.activeKeep?.creatorId; return true})

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

async function createVaultKeep(){
try {
    const formData = {}
    formData.creatorId = account.value.id
    formData.keepId = vaultKeepForm.value.keepId
    formData.vaultId = vaultKeepForm.value.vaultId
    await vaultKeepService.createVaultKeep(formData)
}
catch (error){
    Pop.error("Unable to keep in vault", 'error');
    logger.log("Unable to add to vault", error)
}}
</script>


<template>
    <div class="row keepInfo">
        <div class="col-md-6 col-12 rounded-start">
            <img :src="keep?.img" alt="Keep image" class="keepImg">
        </div>
        <div class="col-6">
            <div class="row ">
                <div class="col-11 text-center">
                    <i class="mdi mdi-eye"></i>{{ keep?.views }} | <i class="mdi mdi-alpha-k-box-outline"></i>
                    {{ keep?.kept }}
                </div>
                <div class="col-1">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" @click="unsetActiveKeep()"></button>
                </div>
            </div>
            <div class="row contentInfo align-content-center">
                <h2 id="keepModalLabel" class="text-center">{{ keep?.name }}</h2>
                <p>{{ keep?.description }}</p>
            </div>
            <div class="modal-footer row justify-content-between align-items-end">
                <div class="col-8">
                    <!-- <form @submit.prevent="createVaultKeep()">
                        <div class="input-group">
                            <select class="form-select" id="inputGroupSelect04" aria-label="Example select with button addon" v-model="vaultKeepForm.vaultId">
                                <option v-for="userVault in userVaults" :key="userVault?.id" :value="userVault?.id">
                                    {{userVault?.name}}</option>
                                    <option v-if="!userVaults"><a href="">Add a Vault</a></option>
                                </select>
                                <input type="number" class="d-none" :v-model="vaultKeepForm.keepId" :value="keep?.id">
                                <button class="btn btn-outline-secondary" type="button">Button</button>
                            </div>
                        </form> -->
                </div>
                <div class="col-3">

                    <RouterLink v-if="owner" :to="{name: 'Account'}">
                        <div class="profileImg rounded-circle" :style="{backgroundImage: `url(${keep?.creator.picture})`}">
                        </div>
                    </RouterLink>
                    <RouterLink v-if="!owner" :to="{name: 'Profile', params: {profileId: keep?.creatorId}}"
                    @click="setActiveProfile(keep?.creatorId)">
                <div class="profileImg rounded-circle" :style="{backgroundImage: `url(${keep?.creator.picture})`}">
                </div>
            </RouterLink>
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
}

.keepInfo{
    height: 80dvh;
}

.contentInfo{
    height: 50dvh;
}
</style>