<script setup>
import { Keep } from '../models/Keep.js';



defineProps({keep: Keep})
</script>


<template>
<img :src="keep?.img" alt="" class="rounded sized">
</template>


<style lang="scss" scoped>
.sized{
    max-height: 30%;
    width: 300px;
}
</style>