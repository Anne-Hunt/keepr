<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import KeepModal from './KeepModal.vue';

const activeKeep = computed(()=> AppState.activeKeep)
const activeVaultKeep = computed(()=> AppState.activeVaultKeep)

</script>


<template>
    <!-- VIEW MODAL /// Keep-->
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
        <div class="modal-body row">
            <div v-if="activeKeep"><KeepModal></KeepModal></div>
      </div>
    </div>
  </div>
</div>

<!--VIEW MODAL /// Vault-->
<div class="modal fade" id="vaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
<div class="modal fade" id="keepForm" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
  </div>
  <!--FORM MODAL /// Vault-->
  <div class="modal fade" id="vaultForm" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
  </div>
</template>


<style lang="scss" scoped>

</style>