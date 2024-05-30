<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '../AppState.js';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

function clearActive(){
  AppState.activeKeep = null
  AppState.activeVault = null
}

</script>

<template>
  <div class="container bottomNav sticky-bottom">
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
      <div class="container-fluid">
        <div class="collapse navbar-collapse" id="navbarNavDropdown">
          <ul class="navbar-nav">
            <li class="nav-item dropdown">
              <button class="btn nav-link dropdown-toggle purple" href="#" data-bs-toggle="dropdown"
                aria-expanded="false">
                Create
              </button>
              <ul class="dropdown-menu rounded-bottom ltpurple border border-dark">
                <li><a class="dropdown-item quando" data-bs-target="#vaultForm" data-bs-toggle="modal"
                    @click="clearActive()">Create Vault</a></li>
                <hr />
                <li><a class="dropdown-item quando" data-bs-target="#keepForm" data-bs-toggle="modal"
                    @click="clearActive()">Create Keep</a></li>
              </ul>
            </li>
            <li class="nav-item">
              <RouterLink :to="{ name: 'Home' }">
                <span class="navbar-text quando text-dark home rounded p-1"><strong>Home</strong></span>
              </RouterLink>
            </li>
            <li>
              <RouterLink :to="{ name: 'About'}">
                <div class="navbar-text text-center p-1">
                  <button class="button button-dark-outline rounded quando px-2 py-0 my-2">
                    <p class="m-0 p-0">the</p>
                    <p class="m-0 p-0">keepr</p>
                    <p class="m-0 p-0">co</p>
                  </button>
                </div>
              </RouterLink>
            </li>
            <li>
              <button class="btn text-tertiary" @click="toggleTheme"
                :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
                <i class="mdi"
                  :class="theme == 'light' ? 'mdi-toggle-switch-variant' : 'mdi-toggle-switch-variant-off'"></i>
              </button>
            </li>
            <li>
              <Login />
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </div>

</template>

<style scoped>
button{
  background-color: var(--bg-sandy);
  p{
    font-size: small;
    font-family: Quando;
  }
}

.quando{
  font-family: Quando, Verdana, serif;
}

a:hover {
  text-decoration: none;
}

.ltpurple{
  background-color: var(--bg-ltpurple);
}

.purple{
    background-color: var(--bg-dkpurple);
}

.home{
  background-color: var(--bg-dksandy);
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
@media screen and (min-width: 577px){
    .bottomNav{
        display: none;
    }
}
@media screen and (max-width: 576px) {
  .bottomNav {
    display:contents;
  }
}
</style>
