<template>
  <h1>Notice</h1>
</template>

<script>
export default {
    name: 'MNoticePage',
        mounted() {
        document.title="Thông báo | MISA CukCuk"
    },
    beforeUnmount() {
    
        document.title = "MISA CukCuk"
    },
}
</script>

<style>

</style>