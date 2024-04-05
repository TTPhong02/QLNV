<template>
    <div class="p-radiobutton2">
        <label>Giới tính</label>
        <div class="p-radiobutton2__option">
            <div class="p-radiobutton2__option--item">
                <input v-model="inputChild" type="radio" name="radioCustomerGender" id="radioCustomerGender" value="1">
                <label for="radioCustomerGender" >Nam</label>
            </div>
            <div class="p-radiobutton2__option--item">
                <input v-model="inputChild" type="radio" name="radioCustomerGender" id="radioCustomerGender" value="0">
            <label for="radioCustomerGender">Nữ</label>
            </div>
            <div class="p-radiobutton2__option--item">
                <input v-model="inputChild" type="radio" name="radioCustomerGender" id="radioCustomerGender" value="2">
                <label for="radioCustomerGender">Khác</label>
            </div>
        </div>
    </div>
</template>
<script>

export default({
    name:"MRadioGender",
    props:[
        "modelValue"
    ],
    data() {
        return {
            inputChild:null,
        }
    },
    watch:{
        /**
         * Hàm lắng nghe sự thay đổi của inputchild
         *  Author: TTPhong(22/01/2024)
         */
        inputChild(newValue) {
            this.$emit("update:modelValue", newValue);
        }, 
        /**
         * Hàm lắng nghe sự thay đổi dữ liệu từ cha là gán lại cho inputchild
         *  Author: TTPhong(22/01/2024)
         */
        modelValue(newValue){
            this.inputChild = newValue;
        }
    }
    
})
</script>
<style >
.p-radiobutton2{
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}
.p-radiobutton2 input{
    width: 20px;
    height: 20px;
}
.p-radiobutton2__option{
    display: flex;
    align-items: center;
    justify-content: space-around;
 
 margin-bottom: 10px;
}
.p-radiobutton2__option--item{
    display: flex;
    align-items: center;
    column-gap: 8px;
}
</style>
