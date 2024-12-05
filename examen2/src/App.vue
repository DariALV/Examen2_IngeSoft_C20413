<template>
  <MainPage></MainPage>
</template>

<script>
import MainPage from './components/MainPage.vue';

export default {
  name: 'App',
  components: {
    MainPage
  }
}
</script>

<style>
#app {
  background-color: #F3F4F6;
  height: 100vh;
  overflow: hidden; 
}

*{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-family: 'poppins', sans-serif;
  }
</style>
