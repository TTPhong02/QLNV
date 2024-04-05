<template>
    <div class="p-textfield3">
        <label for="name">{{textLabel}}</label>
        <input class="p-textfield3__input" id="{{idInput}}" type="date" name="name"  v-model="inputChild">
    </div>
</template>
<script>
export default {
    name:"MTextfieldDate",
    props:{
        textLabel :String,
        idInput: String ,
        modelValue:String
    },
    data() {
        return {
            inputChild:null,
        }
    },
    created() {
        this.inputChild = this.modelValue;
    },
    watch: {
        /**
     * Hàm lắng nghe sự kiện khi biến input child thây đổi
     *  Author: TTPhong(22/01/2024)
     */
        inputChild(newValue){
            this.$emit("update:modelValue",newValue );
        },
        /**
     * Hàm lắng nghe sự kiện khi modelvalue thay đổi và gán lại cho inputchild
     *  Author: TTPhong(22/01/2024)
     */
        modelValue(newValue){
            this.inputChild = this.formatDate(newValue);
        }
    },
    methods: {
        /**
     * Hàm thực hiện format lại giá trị với định dạng date
     *  Author: TTPhong(22/01/2024)
     */
        formatDate(date){
            date = String(date).substring(0,10);
            return date;    
        }
    },
}
</script>
<style>

</style>