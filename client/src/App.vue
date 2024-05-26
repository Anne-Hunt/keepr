<script setup>
import { AppState } from './AppState'
import ModalBox from './components/ModalBox.vue';
import Navbar from './components/Navbar.vue'


</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <ModalBox></ModalBox>
  </main>
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

</style>
