<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import KeepModal from './KeepModal.vue';
import KeepForm from './KeepForm.vue';
import VaultForm from './VaultForm.vue';

const activeKeep = computed(()=> AppState.activeKeep)
const activeVaultKeep = computed(()=> AppState.activeVaultKeep)

</script>


<template>
    <!-- VIEW MODAL /// Keep-->
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
        <div class="modal-body p-0 row">
            <div v-if="activeKeep"><KeepModal></KeepModal></div>
      </div>
    </div>
  </div>
</div>

<!--VIEW MODAL /// VaultKeep-->
<div class="modal fade" id="vaultkeepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
        <div class="modal-body">
            <div class="row">
                <div v-if="activeVaultKeep"><KeepModal></KeepModal></div>
            </div>
      </div>
    </div>
  </div>
</div>
<!--FORM MODALS /// Keep-->
<div class="modal fade" id="keepForm" tabindex="-1" aria-labelledby="Create Keep" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="Create Keep">Create A Keep</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <KeepForm></KeepForm>
      </div>
    </div>
  </div>
  </div>
  <!--FORM MODAL /// Vault-->
  <div class="modal fade" id="vaultForm" tabindex="-1" aria-labelledby="Create Vault" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="Create Vault">Create A Vault</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <VaultForm></VaultForm>
      </div>
    </div>
  </div>
  </div>
</template>


<style lang="scss" scoped>

</style>