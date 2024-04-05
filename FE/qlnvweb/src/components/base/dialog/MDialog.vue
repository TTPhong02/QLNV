<template>
    <div id="p-dialog"   pdialog class="p-dialog home-dialog">
        <div class="p-dialog__header">
            <div class="p-dialog__header--title">{{title}}</div>
            <button @click="setFalseResponse" class="p-dialog__button--close icon-button-close"></button>              
        </div>
        
        <div class="p-dialog__message">
            <div  class="p-dialog__message--icon ">
                <div :class="iconDialog"></div>
            </div>
            <div class="p-dialog__message--title">
                <div>{{text}}</div>
                <ul>
                    <li v-for="(item,index) in errors" :key="index">{{ errors[index]}}</li>
                </ul>
            </div>
            
        </div>
        <div class="p-dialog__button">
            <MButton @click="setFalseResponse" text="Hủy" className="p-button2" ></MButton>
            <MButton @click="setTrueResponse" text="Đồng ý" className="p-button1"></MButton>
        </div>
    </div>
</template>
<script>
    export default {
        name: "MDialog",
        components:[
        ],
        props:[
            "title",
            "errors",
            "closeDialog",
            "iconDialog",
            "dialogResTrue",
            "dialogResFalse",
            "text"
        ],
        data() {
            return {
                
            }
        },
        methods: {
            /**
             * Hàm gọi đến hàm set giá trị khi nhấn đồng ý
             * Author: TTPhong(22/01/2024)
             */
            setTrueResponse(){
                this.dialogResTrue();
            },
                /**
             * Hàm gọi đến hàm set giá trị khi nhấn hủy hoặc tắt
             * Author: TTPhong(22/01/2024)
             */
            setFalseResponse(){
                this.dialogResFalse();
            }
        },
    }
</script>
<style>
#p-dialog{
    z-index: 100;
}
.p-dialog{
    width: 400px;
    background-color: #fff;
    box-shadow:  0px 0px 50px 2px #888888;
    z-index: 1000;
    border-radius: 4px ;
}
.p-dialog__header{
    display: flex;
    padding:24px 24px;
    align-items: center;
    justify-content:space-between;
}
.p-dialog__header--title{
    font-size: 20px;
    font-weight: 700;
    text-align: left;
}
.p-dialog__message{
    display: flex;
    align-items: center;
    column-gap: 20px;
    padding:0px 0px 24px 24px;
    font-size: 14px;
}
.p-dialog__button{
    display: flex;
    justify-content: end ;
    padding: 0px 24px 24px 24px;
    column-gap: 12px;
}
.p-dialog__button--cancel{
    padding-right: 8px;
}
.p-dialog__header--icon{
    text-align: center;
    vertical-align: middle;    
    top:24px;
    right: 24px;
    width: 20px;
    height: 20px;
    cursor: pointer;
}
.p-dialog__message--title{
    padding-left: 25px;
}
</style>