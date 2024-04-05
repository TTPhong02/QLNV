<template>
  <div class="container">
    <div class="p-language">
        <div class="box">
            <div @click="this.menuLanguage = !this.menuLanguage" class="box-item">
                <div class="icon-flag-vietnam"></div>
                <div class="p-language-text">Tiếng Việt</div>
            </div>
            <div v-if="menuLanguage" class="p-language-menu">
                <div @click="this.menuLanguage = !this.menuLanguage" class="p-language-menu-item">Tiếng Anh</div>
                <div @click="this.menuLanguage = !this.menuLanguage" class="p-language-menu-item">Tiếng Việt</div>
            </div>
        </div>
    </div>
    <div class="p-login">
        <div class="p-login-header">
            <div class="icon-amis-platform2"></div>
        </div>
        <div class="p-login-form">
            <div class="p-login-form__input">
                <input
                @focus='handleFocus("username")'
                ref="username"
                class="input"
                :class="{'invalid': errors.phoneNumber  } " 
                type="text" 
                v-model="account.phoneNumber" 
                placeholder="Số điện thoại/email"
                @keydown.enter="validateForm()"
                >
                <span  class="message">{{this.errors.phoneNumber}}</span>
            </div>
            <div class="p-login-form__input">
                <div class="box-password">
                    <input 
                    @focus='handleFocus("password")'
                    ref="password"
                    class="input "
                    :class="{'invalid': errors.password} "  
                    :type="showPass ? 'text' : 'password'" 
                    v-model="account.password" 
                    placeholder="Mật khẩu"
                    @keydown.enter="validateForm()"
                    >
                    <div @click="this.showPass = !this.showPass" :class="showPass ? 'icon-show-pass' : 'icon-hide-pass'"></div>
                </div>
                <span class="message">{{this.errors.password}}</span>
            </div>
            <span class="message">{{this.errorMessageLogin}}</span>
            <!-- <MTextfield v-model="account.phoneNumber"  :required="true" textPlaceHolder="Số điện thoại/email"></MTextfield>
            <MTextfield v-model="account.password" :required="true" typeInput="password" textPlaceHolder="Mật khẩu"></MTextfield> -->
            <div class="p-login-form-forget">Quên mật khẩu?</div>
            <div class="p-login-form-button">
                <MButton  @click="validateForm()" text="Đăng nhập" className="p-button5"></MButton>
            </div>
            <div class="p-login-form-title">
                <div class="p-login-form-title-line"><hr></div>
                <div class="p-login-form-title-text">Hoặc đăng nhập với</div>
                <div class="p-login-form-title-line"><hr></div>
            </div>     
            <div class="p-login-form-methods">
                <div class="icon-google item ">
                </div>
                <div class="icon-apple item">
                </div>
                <div class="icon-microsoft item">
                </div>
            </div>       
        </div>
    </div>
  </div>
</template>

<script>
// import MTextfield from "../components/base/input/MTextfield.vue";
export default {
    name:'MLogin',
    components:{
        // MTextfield
    },
    data() {
        return {
            account:{},
            showPass: false,
            errors:{},
            errorMessageLogin:null,
            menuLanguage:false
        }
    },
    // mounted() {
    //     // Lắng nghe sự kiện focus vào input và thực hiện hành động tương ứng
    //     this.$refs.password.addEventListener('focus', this.handleFocus);
    //     this.$refs.password.addEventListener('blur', this.handleBlur);
        
    // },
    // beforeUnmount() {
    //     // Dọn dẹp và loại bỏ sự kiện lắng nghe khi component bị hủy
    //     this.$refs.password.removeEventListener('focus', this.handleFocus);
    //     this.$refs.password.removeEventListener('blur', this.handleBlur);
    // },
    methods: {

        handleFocus(input) {
            // Chỉ thực hiện hành động nếu input chưa được focus trước đó
            if (input === 'username') {
                delete(this.errors.phoneNumber);
                this.errorMessageLogin="";
            }else{
                delete(this.errors.password);
                this.errorMessageLogin="";
            }
        },
        /**
         * hàm thực hiện kiểm tra dữ liệu form đăng nhập 
         * Author by : TTPhong(22/02/2024)
         */
        validateForm(){
            if(this.account.phoneNumber === null || this.account.phoneNumber === undefined || this.account.phoneNumber === ""){
                this.errors.phoneNumber = this.MISAResource["VN"].UsernameNotEmpty;
            }else{
                delete(this.errors.phoneNumber);
            }
            if(this.account.password === null || this.account.password === undefined || this.account.password === ""){
                this.errors.password = this.MISAResource["VN"].PasswordNotEmpty;
            }else{
                delete(this.errors.password);
            }
            if(!this.errors.password && !this.errors.phoneNumber){
                this.login();
            }
        },
        /**
         * Hàm thực hiện đăng nhập vào hệ thống
         * Author by: TTPhong(22/02/2024)
         */
        login(){
            try{
                this.api.post(`${this.URLRequest}Account`,this.account)
                .then(res=>{
                    switch (res.status) {
                        case 200:
                            localStorage.setItem("Token",JSON.stringify(res.data.Token))
                            localStorage.setItem("User",JSON.stringify(res.data.User))
                            location.reload();
                            this.$router.push("/employee");  
                            break;
                    }
                })
                .catch(error=>{ 
                    this.errorMessageLogin = Object.values(error.response.data.Errors).join(""); 
                })
            }catch(error){
                this.emitter.emit("handleApiError",error);
            }
        }
    },
}
</script>

<style scoped>
    .p-login{
        width: 304px;
        border-radius: 8px;
        padding: 40px 48px 40px 48px;
        min-height: 340px;
        background: #fff;
        position: relative;
        box-shadow: 0 12px 20px rgba(0,0,0,.12);
    }
    .p-login-header{
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .p-login-header-title{
        font-size: 27px;
        font-weight: bold;
    }
    .p-login-form{
        padding: 20px 0px 0px 0px ;
    }
    .p-login-form-title{
        display: flex;
        justify-content: center;
        align-items: center;
        padding-top: 26px;
        margin-bottom: 16px;
    }
    .p-login-form-forget{
        color: #0073e6;
        padding: 15px 0px;
        cursor: pointer;
    }
    .p-login-form-button{
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .p-login-form-title-line{
        /* border: 1px solid #ccc; */
        width: 100%;
        flex: 1;
    }
     .p-login-form-title-text{
        color: #9ea1a5;
        padding: 0px 10px;
    }
    .p-login-form-methods{
        display: flex;
        justify-content: center;
        align-items: center;    
    }
    .p-login-form-methods .item{
        padding:0px 4px;   
    }
    .p-login-form__input{
        margin: 12px 0px;
    }
    .input{
        font-size: 16px;
        color: #212121;
        line-height: 1.2;
        width: calc(100% - 28px);
        height: 48px;
        border: 1px solid #e0e5e9;
        background: 0 0;
        padding: 0px 14px;
        border-radius: 3px;
    }
    .invalid{
        border: 1px solid red;
    }
    .message{
        text-align: inherit;
        font-size: 12px;
        color: #ff1d1d;
        height: 20px;
        line-height: 20px;
    }
    .container{
        width: 100vw;
        height: 100vh;
        background: url("../assets/img/background-image/bg1.jpg") no-repeat ;
        background-size: cover;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .p-language{
        position: absolute;
        background-color: rgba(255,255,255,.1);
        border: 1px solid #d9dde1;
        border-radius: 4px;
        z-index: 1;
        top: 3%;
        right: 3%;
        cursor: pointer;
    }
    .p-language:hover{
        background-color:#2680eb ;
        border: 1px solid #2680eb;
    }
     .p-language-text{
        color: #fff;
        padding: 0px 8px 0px 6px;
     }
     .box{
        position: relative;
        padding: 6px 10px;
     }
     .box-item{
        display: flex;
        justify-content: center;
        align-items: center;
     }
     .p-language-menu{
        position: absolute;
        top: 110%;
        left: 0%;
        width: 100%;
        box-shadow: 0 2px 8px rgba(0,0,0,.3);
        z-index: 9999;
        background-color: #fff;
        border-radius: 3px;
        max-height: 400px;
        padding-top: 8px;
        padding-bottom: 8px;
        overflow-x: hidden;
        overflow-y: hidden;
     }
     .p-language-menu-item{
        padding: 8px 16px;
     }
     .p-language-menu-item:hover{
        background-color: #f2f2f2;
     }
    .box-password{
        position: relative;
    }
    .icon-show-pass{
        position: absolute;
        top: 37%;
        right: 3%;
        cursor: pointer;
    }
    .icon-hide-pass{
        position: absolute;
        top: 33%;
        right: 3%;
        cursor: pointer;
    }
    .item{
        margin: 0px 4px;
    }
</style>