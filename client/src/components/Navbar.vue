<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>
  <nav class="navbar navbar-expand-sm navbar-dark px-3 shadow">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <span class="btn btn-dark-outline rounded d-flex align-items-center"><i class="mdi mdi-lock-open fs-3 text-dark"></i>HOME</span>
      </div>
    </router-link>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
    <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle text-dark d-flex align-items-center" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            <strong>CREATE</strong>
          </a>
          <ul class="dropdown-menu">
            <li><a class="dropdown-item" href="#">Create Vault</a></li>
            <li><a class="dropdown-item" href="#">Create Keep</a></li>
          </ul>
        </li>
        <router-link :to="{ name: 'About' }" class="btn text-dark btn btn-dark-outline rounded">
            ABOUT
          </router-link>
        </ul>
      </div>
      <!-- LOGIN COMPONENT HERE -->
      <div>
        <button class="btn text-warning" @click="toggleTheme"
          :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
          <i class="mdi" :class="theme == 'light' ? 'mdi-toggle-switch-variant' : 'mdi-toggle-switch-variant-off'"></i>
        </button>
      </div>
      <Login />
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
