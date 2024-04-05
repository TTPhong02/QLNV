<template>
    <div class="p-dropmenu">
        <div class="p-dropmenu__label">
            <label >{{textLabel}}<span v-if="required" class="required"> *</span></label>
        </div>
        <div class="p-dropmenu__box">
            <input 
                  ref="nameInputRef"
            :type="typeInput" 
            id="p-dropmenu__input" 
            :placeholder="textPlaceHolder"
            :class="classInvalid"
            @input='filtersItem' 
            @keydown.down="highlightNext"
            @keydown.up="highlightPrev"
            @keydown.enter="selectItem"  
            v-model="selectedItem"      
            >
            <div @click="showAllList"  class="p-dropmenu__box--icon">
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div v-if="isShowListDataCombobox" :class="{'p-dropmenu__drop':positionDrop, 'p-dropmenu__above': positionAbove}"  >
                <div
                    v-for="(item, index) in filteredItems"
                    :key="item" 
                    :class="{ 'p-dropmenu__drop--item': true, 'selected': index === highlightedIndex || selectedItem === item }"
                    @click="selectItemFromList(item)"
                >
                {{item}}
                <span v-if="item===selectedItem" class="icon-combobox-selected"></span>
                </div>
            </div>
        </div> 
        <span v-if="required">
            <span  class="p-input__validate--error" v-if="isInputValid">{{ messageInvalid }}</span>
        </span>                  
    </div>
</template>

<script>
export default {
    name:"MCombobox",
    props:{
        nameCobobox : String,
        textPlaceHolder: String,
        textLabel: String,
        required:Boolean,
        dataCombobox: Array,
        modelValue: [Number,String],
        messageInvalid:String,
        typeInput:String,
        positionList:String,
    },
    created() {
        this.checkListDropOrAbove();
        this.selectedItem = this.modelValue;
    },
    data() {
        return {
            selectedItem: null,
            filteredItems: [],
            highlightedIndex: -1,
            selectedIndex : -1,
            classInvalid: "",
            isInputValid: false,
            isSelected: false,
            isShowListDataCombobox: false,
            positionDrop:true,
            positionAbove:false
        }
    },
    methods: {
        /**
         * Hàm focus vào input
         *  Author: TTPhong(22/01/2024)
         */
        focusToInput(){
            this.$refs.nameInputRef.focus();
        }
        /**
         * Hàm kiểm tra xem vị trí của dropmenu
         *  Author: TTPhong(22/01/2024)
         */
            ,
        checkListDropOrAbove(){
            if(this.positionList=== "above"){
                this.positionAbove = true;
                this.positionDrop = false;
            }
        },
        /**
         * Hàm thực hiện hiện thị danh sách combobox 
         *  Author: TTPhong(22/01/2024)
         */
        showAllList(){         
            this.filteredItems = this.dataCombobox;
            if(this.isShowListDataCombobox === true){
                this.isShowListDataCombobox = false;
            }else{
                this.isShowListDataCombobox = true
            }
            
        }, 
        /**
         * Hàm thực hiện lọc thông tin nhập từ ô input 
         *  Author: TTPhong(22/01/2024)
         */
        filtersItem(){
            this.isShowListDataCombobox = true;
            this.filteredItems = this.dataCombobox.filter(item =>
            item.toLowerCase().includes(this.selectedItem.toLowerCase()));
            this.highlightedIndex = 0;
        },
        /**
         * Hàm thực hiện set giá trị khi chọn dữ liệu trong combobox
         *  Author: TTPhong(22/01/2024)
         */
        selectItem() {
            if (this.highlightedIndex !== -1) {
                this.$emit('selected', this.filteredItems[this.highlightedIndex]);
                this.selectedItem = this.filteredItems[this.highlightedIndex];
                this.filteredItems = [];
                this.isShowListDataCombobox = false;
            }
        },
        /**
         * Hàm thực hiện chọn dữ liệu từ danh sách combobox
         *  Author: TTPhong(22/01/2024)
         */
        selectItemFromList(item) {
            this.$emit('selected', item);
            this.selectedItem = item;
            this.isShowListDataCombobox = false;    
        },
        /**
         * Hàm thực hiện di chuyển vị trí chọn khi nhấn nút mũi tên xuống
         *  Author: TTPhong(22/01/2024)
         */
        highlightNext() {
            this.highlightedIndex = (this.highlightedIndex + 1) % this.filteredItems.length;
        },
        /**
         * Hàm thực hiện di chuyển vị trí chọn khi khi nhấn nút mũi tên lên
         *  Author: TTPhong(22/01/2024)
         */
        highlightPrev() {
            this.highlightedIndex = (this.highlightedIndex - 1 + this.filteredItems.length) % this.filteredItems.length;
        },
    },
    watch: {
        isInputValid(newValue){
            if(newValue === true && this.required === true){
                this.classInvalid ="p-input__invalid";
            }else{
                this.classInvalid ="";
            }
        },
        messageInvalid(newValue){
            if (newValue) {
                this.isInputValid = true;
            } else {
                this.isInputValid = false;
            }
        },
        selectedItem(newValue){
            this.$emit("update:modelValue", newValue);
        },
        modelValue(newValue){
            this.selectedItem = newValue;
            if (newValue === "" || newValue === undefined || newValue === null) {
                this.isInputValid = true;              
            } else {
                this.isInputValid = false;
            }
        }
    }
}
</script>

<style scoped>
input:focus{
    outline: none;
}
input{
    border: unset;
}
label{
    font-weight: 700;
    font-size: 14px;
}
.p-dropmenu__label{
    margin-bottom: 8px;
}
.p-dropmenu__box{
    width: 100%;
    position: relative;
    /* margin-top: 8px; */

}
.p-dropmenu__box input{
    border-radius: 4px 0px 0px 4px;
    height: 36px;
    width: calc(100% - 36px);
    border: 1px solid #ccc;
    padding-left: 12px;
}
.p-dropmenu__box--icon {
    border: 1px solid #ccc;
    position: absolute;
    right: 0;
    top: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 36px;
    height: 36px;
    background-color: #fff;
    border-radius: 0px 4px 4px 0px;
}
.p-dropmenu__above{
    bottom: 100%;
    width: 100%; 
    position: absolute;
    background-color: #fff;
    border-radius: 4px;
    max-height: 184px;
    overflow-y: scroll;
    scrollbar-width: thin;
    scroll-behavior: smooth;
    border: 1px solid #ccc;
    z-index: 1000;
    /* box-shadow:1px 1px 12px 2px #ccc; */
}
.p-dropmenu__drop{
    top: 100%;
    width: 100%; 
    position: absolute;
    background-color: #fff;
    border-radius: 4px;
    max-height: 184px;
    overflow-y: scroll;
    scrollbar-width: thin;
    scroll-behavior: smooth;
    border: 1px solid #ccc;
    z-index: 1000;
    /* box-shadow:1px 1px 12px 2px #ccc; */
}
.p-dropmenu__drop--item{
    padding-left: 8px;
    min-height: 36px;
    display: flex;
    align-items: center;
    border-radius: 4px;
    margin: 8px 8px 8px 8px;
    cursor: pointer;
    justify-content: space-between;
}
.p-dropmenu__drop--item.selected,
.p-dropmenu__drop--item:hover{
    background-color: rgba(80,184,60,0.1);
}
::-webkit-scrollbar {
    width: 8px;
    padding-right: 8px;
}
::-webkit-scrollbar-thumb {
    background: #888; 
    border-radius: 6px;
  }
.p-input__invalid{
    border:1px solid red !important;
}
.p-input__validate--error {
  color: red;
  font-size: 12px;
  font-style: italic;
  margin-bottom: 20px;
}
</style>