<script setup>
import { computed, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { keepService } from '../services/KeepService.js';
import KeepHomeCard from '../components/KeepCard.vue';

const keeps = computed(()=>AppState.keeps)

async function getKeeps(){
  try {
    await keepService.getKeeps()
  }
  catch (error){
    Pop.error("Unable to load keeps!", 'error');
    logger.log("Unable to load keeps on home page", error)
  }
}

onMounted(()=>
getKeeps())
</script>

<template>
  <div class="container">
    <section class="row ">
      <div class="col-12 masonry">
        <div v-for="keep in keeps" :key="keep.id" class="mb-3 d-flex">
          <KeepHomeCard :keep="keep"/>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">
.masonry {
  columns: 200px;
  column-gap: 2rem;
}

// .masonry-with-columns {
//   columns: 5 200px;
//   column-gap: 1rem;
//   div {
//     width: 150px;
//     background: #EC985A;
//     color: white;
//     margin: 0 1rem 1rem 0;
//     display: inline-block;
//     width: 100%;
//     text-align: center;
//     font-family: system-ui;
//     font-weight: 900;
//     font-size: 2rem;
//   } 
//   @for $i from 1 through 36 { 
//     div:nth-child(#{$i}) {
//       $h: (random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
//   }
// }
</style>
