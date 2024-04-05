<template>
  <div  class="p-loading">
    <img src="../../../assets/img/loading.svg" alt="">
  </div>
</template>

<script>
export default {
    name:'MLoading',

}
</script>

<style>
@keyframes rotate {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

.p-loading img{
    animation: rotate 2s linear infinite;
    scale: 1.8;
}
.p-loading{
    position: absolute;
    top: 0;
    z-index: 1000000;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    width: 100vw;
    background-color: rgba(92, 92, 92, 0.4);
}
</style>