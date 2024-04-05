<template>
  <h1>Setting</h1>
</template>

<script>
export default {
    name: 'MSettingPage',
        mounted() {
        document.title="Cài đặt | MISA CukCuk"
    },
    beforeUnmount() {
    
        document.title = "MISA CukCuk"
    },
}
</script>

<style>

</style>