<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import KeepModal from './KeepModal.vue';
import KeepForm from './KeepForm.vue';
import VaultForm from './VaultForm.vue';
import { Keep } from '../models/Keep.js';
import { Vault } from '../models/Vault.js';
import { VaultKeep } from '../models/VaultKeep.js';
import { Account } from '../models/Account.js';
import { Profile } from '../models/Profile.js';
import AccountForm from './AccountForm.vue';

defineProps({keep: Keep, vault: Vault, vaultKeep: VaultKeep, account: Account, profile: Profile})

const activeKeep = computed(()=> AppState.activeKeep)
const activeVaultKeep = computed(()=> AppState.activeVaultKeep)

</script>


<template>
    <!-- VIEW MODAL /// Keep-->
    <div class="modal fade rounded" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
      <h1 class="modal-title d-none" id="keepModalLabel">Keep</h1>
      <div class="modal-body p-0 row">
        <div><KeepModal></KeepModal></div>
      </div>
    </div>
  </div>
  </div>

<!--VIEW MODAL /// VaultKeep-->
<div class="modal fade rounded" id="vaultkeepModal" tabindex="-1" aria-labelledby="VaultModal" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
      <h1 class="modal-title d-none" id="VaultModal">Vault</h1>
        <div class="modal-body">
            <div class="row">
                <div v-if="activeVaultKeep"><KeepModal></KeepModal></div>
            </div>
      </div>
    </div>
  </div>
</div>
<!--FORM MODALS /// Keep-->
<div class="modal fade rounded" id="keepForm" tabindex="-1" aria-labelledby="keep-form" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="keep-form">Create A Keep</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <KeepForm></KeepForm>
      </div>
    </div>
  </div>
  </div>
  <!--FORM MODAL /// Vault-->
  <div class="modal fade rounded" id="vaultForm" tabindex="-1" aria-labelledby="vault-form" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="vault-form">Create A Vault</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <VaultForm></VaultForm>
      </div>
    </div>
  </div>
  </div>

  <!-- FORM MODAL /// Account -->

  <div class="modal fade rounded" id="accountForm" tabindex="-1" aria-labelledby="account-form" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="account-form">Edit Account</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <AccountForm></AccountForm>
      </div>
    </div>
  </div>
  </div>
</template>


<style lang="scss" scoped>

</style>