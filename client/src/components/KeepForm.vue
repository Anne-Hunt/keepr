<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { AppState } from '../AppState.js';
import { keepService } from '../services/KeepService.js';

const activeKeep = computed(()=>AppState.activeKeep)

const keepData = ref({
    name: '',
    img: '',
    // tags: [],
    description: ''
})

async function createKeep(){
    try {
        const newKeep = {}
        newKeep.name = keepData.value.name
        newKeep.description = keepData.value.description
        newKeep.img = keepData.value.img
      await keepService.createKeep(newKeep)
      resetForm()
    }
    catch (error){
      Pop.error("Unable to update Keep at this time", 'error');
      logger.log("failed to update keep", error)
    }
}

async function updateKeep(){
    try {
        const updatedKeep = {}
        updatedKeep.name = keepData.value.name
        updatedKeep.description = keepData.value.description
        updatedKeep.img = keepData.value.img
      await keepService.updateKeep(updatedKeep)
      resetForm()
    }
    catch (error){
      Pop.error("Unable to update Keep at this time", 'error');
      logger.log("failed to update keep", error)
    }
}

function resetForm(){
    keepData.value.name = ''
    keepData.value.img = ''
    // keepData.value.tags = []
    keepData.value.description = ''
}

function values(){
    if(AppState.activeKeep){
        keepData.value.name = AppState.activeKeep?.name
        keepData.value.img = AppState.activeKeep?.img
        // keepData.value.tags = AppState.activeKeep.tags
        keepData.value.description = AppState.activeKeep?.description
    }
}

onMounted(()=>{
    values()
})
</script>


<template>
<form v-if="!activeKeep" @submit.prevent="createKeep()">
    <div class="mb-3">
        <input type="text" v-model="keepData.name" class="w-100" placeholder="Title ...">
    </div>
    <div class="mb-3">
        <input type="text" v-model="keepData.img" class="w-100" placeholder="Image URL ...">
    </div>
        <div class="form-check mb-3 w-100">
            <textarea class="form-control w-100" type="textarea" v-model="keepData.description" value="" id="description" rows="5" placeholder="Description ..."></textarea>
        </div>
    <div class="d-flex justify-content-end">
        <button class="btn btn-dark text-light text-end" type="submit">Create Keep</button>
    </div>
</form>

<form v-else @submit.prevent="updateKeep()">
    <div class="mb-3">
        <input type="text" v-model="keepData.name" class="w-100" placeholder="Title ...">
    </div>
    <div class="mb-3">
        <input type="text" v-model="keepData.img" class="w-100" placeholder="Image URL ...">
    </div>
        <div class="form-check mb-3 w-100">
            <textarea class="form-control w-100" type="textarea" v-model="keepData.description" value="" id="description" rows="5" placeholder="Description ..."></textarea>
        </div>
    <div class="d-flex justify-content-end">
        <button class="btn btn-dark text-light text-end" type="submit">Update Keep</button>
    </div>
</form>
</template>


<style lang="scss" scoped>

</style>