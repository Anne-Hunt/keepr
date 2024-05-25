<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import VaultCard from '../components/VaultCard.vue';
import KeepCard from '../components/KeepCard.vue';



const account = computed(()=> AppState.account)
const vaults = computed(()=>AppState.vaults)
const keeps = computed(()=>AppState.keeps)
</script>


<template>
<div class="container">
<div class="row mb-3" :style="{backgroundImage: `url(${account?.coverImg})`}"></div>
<div class="row">
    <h3 class="mb-3">Vaults</h3>
    <div class="col-4 mb-3" v-for="vault in vaults" :key="vault.id">
        <VaultCard/>
    </div>
</div>
<div class="row">
    <h3 class="mb-3">Keeps</h3>
    <div class="col-4 mb-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard/>
    </div>
</div>
</div>
</template>


<style lang="scss" scoped>

</style>