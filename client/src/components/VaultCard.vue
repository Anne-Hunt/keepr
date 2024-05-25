<script setup>
import { Vault } from '../models/Vault.js';


defineProps({vault: Vault})
</script>


<template>
<div class="card vault d-flex align-items-bottom p-1" :style="{backgroundImage: `url(${vault?.img})`}">
    <h4 class="text-light">{{ vault.name }}</h4>
</div>
</template>


<style lang="scss" scoped>
.vault{
    height: 30dvh;
}
</style>