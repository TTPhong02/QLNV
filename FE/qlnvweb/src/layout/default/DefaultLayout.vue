<template>
    <div>
        <MHeading></MHeading>
        <MSidebar></MSidebar>
        <MMain></MMain>
    </div>
</template>

<script>
 import MHeading from '../Heading.vue';
 import MSidebar from '../Sidebar.vue';
 import MMain from '../Main.vue';
export default {
    name:"DefaultLayout",
    components:{
        MHeading,MSidebar,MMain
    }
}
</script>

<style>

</style>