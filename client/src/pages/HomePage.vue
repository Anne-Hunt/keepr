divsectiondiv<script setup>
import { computed, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { keepService } from '../services/KeepService.js';

const keeps = computed(()=>AppState.keeps)
const brick = Math.random()*80
const brickHeight = `${brick}dvh`

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
    <div class="row ">
      <div class="col-12 masonry-with-columns">
        <section v-for="keep in keeps" :key="keep.id">
          <ActiveKeep :keep="keep"></ActiveKeep>
      </section>
    </div>
  </div>
  </div>
</template>

<style scoped lang="scss">
// .masonry {
//   columns: 200px;
//   column-gap: 2rem;
// }

.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    height: v-bind(brickHeight);
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  } 
  @for $i from 1 through 36 { 
    div:nth-child(#{$i}) {
      $h: (random(400) + 100) + px;
      height: $h;
      line-height: $h;
    }
  }
}
</style>
