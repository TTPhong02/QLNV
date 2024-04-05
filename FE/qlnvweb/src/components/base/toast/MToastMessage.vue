<template>
    <span id="toast1" class="p-message1">
        <div class="p-message1__icon">
            <div :class="icon">
                </div>
            <!-- <img src="./assets/img/Combo-spec-normal/Layer 2.png" alt="icongreen"> -->
        </div>
        <div :class="{'p-message1__title':true, 'red' : isFailed}">{{title}}</div>
        <div class="p-message1__text">{{text}}</div>
        <!-- <div class="p-message1__action">Hoàn tác</div> -->
        <div class="p-message1__closeicon">
            <img src="../../../assets/img/dialog_design_guideline/Layer 2.png" alt="iconclose">
        </div>
    </span>
</template>

<script>
export default {
    name:"MToastMessage",
    props:[
        "title",
        "text",
        "modelValue",
        "icon",
        "color"
    ],
    updated() {
       if(this.color != ""){
            this.isFailed = true;
            console.log(this.color);
        } 
    },
    data() {
        return {
            showToast: '',
            isFailed:false
        }
    },
    methods: {
        
    },
    watch:{
        
        // modelValue(newValue){
        //     if(newValue === true){
        //         this.showToast = this.modelValue;
        //         setTimeout(() => {
        //             this.showToast = false;
        //             this.$emit("update:modelValue", false);
        //         }, 2900);
        //     }else{
        //         this.showToast = false;
        //     }
        // },

    }
    
}
</script>

<style>
#toast1,
#toast2{
    z-index: 1000;
}
.red{
    color: red !important;
}
.p-message1{
    display: flex;
    width: 450px;
    max-width: 650px;
    align-items: center;
    justify-content: space-between;
    background-color: #fff;
    height: 56px;
    border-radius: 4px;
    box-shadow: 0px 0px 20px 1px #ccc;
}
.p-message1__icon{
    padding-left: 16px;
}
.p-message1__icon i{
    vertical-align: middle;
    font-size: 24px;
    border-radius: 50%;
    color: #50B83C;
}
.p-message2__icon i{
    vertical-align: middle;
    font-size: 24px;
    border-radius: 50%;
    color: #ff0000;
}
.p-message1__title{
    padding-left: 8px;
    color: #50B83C;
    font-weight: 700;
}
.p-message1__text{
   padding-left: 8px;
}
.p-message1__action{

    font-weight: 700;
    text-decoration:underline;
    cursor: pointer;
    
}
.p-message1__closeicon{
    padding: 0 16px;
    cursor: pointer;
}
.p-message1__closeicon img{
    width: 24px;
    height: 24px;
    vertical-align: middle;
}
.p-message1__left, .p-message1__right{
    display: flex;
}
#toast1{
    position: fixed;
    top:10%;
    right: 30px;
    -webkit-animation: fadein 0.5s , fadeout 0.5s 2.5s;
    animation:fadein 0.5s , fadeout 0.5s 2.5s ; 
}


@-webkit-keyframes fadein {
    from {right: 0; opacity: 0;}
    to {right: 24px; opacity: 1;}
  }
  
  @keyframes fadein {
    from {right: 0; opacity: 0;}
    to {right: 24px; opacity: 1;}
  }
  
  @-webkit-keyframes fadeout {
    from {right: 24px; opacity: 1;}
    to {right: 0; opacity: 0;}
  }
  
  @keyframes fadeout {
    from {right: 24px; opacity: 1;}
    to {right: 0; opacity: 0;}
  }
</style>