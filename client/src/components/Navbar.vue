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
  <nav class="navbar navbar-expand-sm px-3 shadow mb-3">
    <div class="container-fluid">
      <div  class="navbar-brand bg-tertiary rounded d-flex col-1">
        <RouterLink :to="{ name: 'Home' }">
          <span class="navbar-text"><strong>HOME</strong></span>
        </RouterLink>
      </div>
      <div class="collapse navbar-collapse col-1">
        <ul class="navbar-nav d-flex align-items-center justify-content-between">
          <li class="nav-item dropdown pe-5">
                <a class="nav-link dropdown-toggle text-dark d-flex flex-column align-items-center" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                  <strong>CREATE</strong>
                </a>
                <ul class="dropdown-menu">
                  <li><a class="dropdown-item" data-bs-target="#vaultForm" data-bs-toggle="modal">Create Vault</a></li>
                  <li><a class="dropdown-item" data-bs-target="#keepForm" data-bs-toggle="modal">Create Keep</a></li>
                </ul>
              </li>
              <li>
              </li>
            </ul>
          </div>
          <div class="col-8 text-center">
            <RouterLink :to="{ name: 'About'}">
              <span class="navbar-text border border-2 border-dark rounded p-2">The <strong>Keepr</strong> Co</span>
            </RouterLink>
          </div>
    </div>
      <!-- LOGIN COMPONENT HERE -->
      <div class="col-2">
        
        <div class="d-flex text-end">
          <button class="btn text-warning" @click="toggleTheme"
            :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
            <i class="mdi" :class="theme == 'light' ? 'mdi-toggle-switch-variant' : 'mdi-toggle-switch-variant-off'"></i>
          </button>
          <Login />
      </div>
        <div>
        </div>
      </div>
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
