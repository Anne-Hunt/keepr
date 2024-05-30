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
    <!-- <NavbarMobile/> -->
  </main>
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
  --bg-white: #F9F6FA;
  --bg-ltpurple: #DED6E9;
  --bg-dkpurple: #877A8F;
  --bg-sandy: #FEF6F0;
  --bg-dksandy: #E9D8D6;
  --bg-brown: #2D2D2D;
}
header,main,footer{
  background-color: var(--bg-sandy);
}

</style>
