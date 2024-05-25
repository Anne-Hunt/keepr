<script setup>
import { Vault } from '../models/Vault.js';


defineProps({vault: Vault})
</script>


<template>
<div class="card vault" :style="{backgroundImage: `url(${vault?.img})`}">

</div>
</template>


<style lang="scss" scoped>
.vault{
    height: 30dvh;
}
</style>