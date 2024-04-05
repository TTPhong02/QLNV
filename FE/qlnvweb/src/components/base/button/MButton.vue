<template>
    <button type="button" v-bind:class="className">{{text}}</button>
</template> 
<script>
export default {
    name:"MButton",
    props:[
        "text",
        "className",
    ],
}
</script>
<style >
    
</style>