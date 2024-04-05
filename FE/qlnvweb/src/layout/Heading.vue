<template>
        <div class="p-header">
            <div class="p-header__grid icon-header-grid"></div>
            <div class="p-header__logo icon-header-logo">             
            </div> 
            <div class="p-header__text icon-header">             
            </div> 
            
            <div class="p-header__avatar">
                <img src="../assets/img/Component 12 – 1/EmployeeImage.png" alt="employeeimage">
                {{this.user.FullName}}
            </div>  
            
            <div class="p-header__notification">
                <div class="p-header__notification--item icon-question">
                </div> 
                <div class="p-header__notification--item icon-notification-bell">
                </div> 
                <div @click="this.showOption = !this.showOption" class="p-header__notification--item icon-option">
                    <div v-if="this.showOption" class="p-option-menu">
                        <div  @click="this.showOption = !this.showOption" class="p-option-menu-item">
                            <div class="icon-profile"></div>
                            <div class="text">Thông tin cá nhân</div>
                        </div>
                        <div   @click="logout(); this.showOption = !this.showOption" class="p-option-menu-item">
                            <div class="icon-logout"></div>
                            <div class="text">Đăng xuất</div>
                        </div>
                    </div>
                </div> 
            </div>
        </div>                        
</template>
<script>
export default {
    name: "MHeading",
    mounted() {
        this.user = JSON.parse(localStorage.getItem("User"));
    },
    data() {
        return {
            user:{},
            showOption:false,
        }
    },
    methods: {
        /**
         * Hàm thực hiện đăng xuất thông tin người dùng
         * Author by : TTPhong(22/02/2024)
         */
        logout(){
            localStorage.removeItem("Token");
            localStorage.removeItem("User");
            location.reload();
        }
    },
}
</script>
<style>
    .p-header{
        height: 56px;
        background-color: #F5F7F8;
        width: 100%;
        box-shadow: 0px 1px 10px 2px #ccc;
    }
    .p-header__grid{
        margin:0px 0px 0px 24px;
    }
    .p-header__text,
    .p-header__logo{
        margin: 0 16px;
    }
    .p-header__avatar img{
        padding-right: 16px;
    }
    .p-header__avatar{
        float: right;
        display: flex;
        font-weight: bold;
        align-items: center;
        height: 56px;
        padding-right:24px ;
    }
    .p-header__notification{
        display: flex;
        column-gap: 20px;
        float: right;
        margin-right: 20px;
    }
    .p-header__notification--item{
        display: flex;
        align-items: center;
        position: relative;
        padding: 16px 0px;
    }
    .p-option-menu{
        position: absolute;
        top: 110%;
        right: -100%;
        background-color: #fff;
        min-width: 200px;
        min-height: 48px;
        padding: 10px 10px;
        z-index: 99999;
        border-radius: 4px;
        box-shadow: 0px 0px 10px 0px #ccc;
    }
    .p-option-menu-item{
        display: flex;
        align-items: center;
        padding: 10px 10px;
    }
    .p-option-menu-item:hover{
        background-color: #F5F7F8;
    }
    .p-option-menu-item .text{
        padding-left: 12px;
    }
</style>