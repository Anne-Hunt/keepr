<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState';
import { AuthService } from '../services/AuthService';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { accountService } from '../services/AccountService.js';

const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)
async function login() {
  AuthService.loginWithPopup()
  if(AppState.account){
    try {
      accountService.getMyVaults()
    }
    catch (error){
      Pop.toast("Unable to get vaults", 'error');
      logger.log(error)
    }
  }
}
async function logout() {
  AuthService.logout()
}

</script>

<template>
  <span class="navbar-text quando">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login" v-if="!identity">
      Login
    </button>
    <div v-else>
      <div class="collapse navbar-collapse">
        <ul class="navbar-nav d-flex align-items-center justify-content-between">
          <li class="nav-item dropdown">
            <div type="button" class="border-0 selectable no-select" data-bs-toggle="dropdown" aria-expanded="false">
              <div v-if="account?.picture || identity?.picture">
                <img :src="account?.picture || identity?.picture" alt="account photo" height="40"
                  class="rounded-circle loginImg shadow border border-dark" />
              </div>
            </div>
            <ul
              class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start rounded-bottom ltpurple border border-dark"
              aria-labelledby="authDropdown">
              <li><a class="dropdown-item quando">
                  <router-link :to="{ name: 'Account' }">
                    Your Account
                  </router-link>
                </a></li>
              <hr />
              <li><a class="dropdown-item quando" @click="logout"><i class="mdi mdi-logout"></i>Log Out</a></li>
            </ul>
          </li>
          <li>
          </li>
        </ul>
      </div>
    </div>
  </span>
</template>

<style lang="scss" scoped>
.loginImg{
  height: 40px;
  width: 40px;
  object-fit: cover;
  object-position: center;
}

.ltpurple{
  background-color: var(--bg-ltpurple);
}
.quando{
  font-family: Quando, Verdana, serif;
}
</style>
