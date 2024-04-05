<template>
<div>
    <!-- <MImport></MImport> -->
    <!-- <MHeading></MHeading>
    <MSidebar></MSidebar>
    <MMain></MMain> -->
    <router-view name="LayoutRouter"></router-view>
    <MToastMessage
      v-if="toast.isShowToast"
      :title="toast.title"
      :text="toast.text"
      :icon="toast.icon"
      :color="toast.color" 
    >
    </MToastMessage>
    <MLoading v-if="isLoading"></MLoading>
    
</div>
</template>
<script>
//  import MHeading from './layout/MHeading.vue';
//  import MSidebar from './layout/MSidebar.vue';
//  import MMain from './layout/MMain.vue';
 //import MImport from './layout/MImport.vue';

export default {
  name: 'App',
  components: {
    //  MHeading,MSidebar,MMain,
    //MImport
  },
  created() {
    this.emitter.on("showLoading",this.showLoading),
    this.emitter.on("hiddenLoading",this.hiddenLoading),
    this.emitter.on("onShowToastMessage",this.onShowToastMessage)
    this.emitter.on("handleApiError",this.handleApiError)
  },
  data() {
    return {
      isLoading: false,
      toast:{
        isShowToast:false,
        title:"",
        text:"",
        icon:'',
        color:''
      }
    }
  },
  methods: {
       /**
     * Hàm hiện toast message 
     * Author: TTPhong (29/01/2024)
     */
    onShowToastMessage(title, text, icon,color) {
      this.toast.isShowToast = true;
      this.toast.title = title;
      this.toast.text = text;
      this.toast.icon = icon;
      this.toast.color = color;
      setTimeout(() => {
        this.toast.isShowToast = false;
      }, 3000);
    },
     /**
     * Hàm bắt lỗi khi api trả về
     * Author: TTPhong (29/01/2024)
     */
    handleApiError(req) {
      try {
        switch (req.response.status) {
          //Lỗi từ người dùng nhập thông tin không hợp lệ
          case 400:
            this.$emitter.emit(
              "onShowToastMessage",
              this.MISAResource["VN"].Failed,
              this.MISAResource["VN"].InvalidData,
              this.MISAResource["VN"].IconWarningSmall,
              this.MISAResource["VN"].ColorRed
            );
            break;
          //Lỗi khi tải khoản không được xác thực
          case 401:
            this.refreshToken();
            // this.emitter.emit(
            //   "onShowToastMessage",
            //   this.MISAResource["VN"].Failed,
            //   this.MISAResource["VN"].InCorrectAccount,
            //   this.MISAResource["VN"].IconWarningSmall,
            //   this.MISAResource["VN"].ColorRed
            // );
            break;
          //Lỗi khi không có quyền truy cập
          case 403:
            this.emitter.emit(
              "onShowToastMessage",
              this.MISAResource["VN"].Failed,
              this.MISAResource["VN"].DeniedAccess,
              this.MISAResource["VN"].IconWarningSmall,
              this.MISAResource["VN"].ColorRed
            );
            break;
          //Lỗi khi đường dẫn gọi API lỗi
          case 404:
            this.$emitter.emit(
              "onShowToastMessage",
              this.MISAResource["VN"].Failed,
              this.MISAResource["VN"].InvalidLink,
              this.MISAResource["VN"].IconWarningSmall,
              this.MISAResource["VN"].ColorRed
            );
            break;
          //Lỗi từ phía backend
          case 500:
            this.$emitter.emit(
              "onShowToastMessage",
              this.MISAResource["VN"].Failed,
              this.MISAResource["VN"].BackendError,
              this.MISAResource["VN"].IconWarningSmall,
              this.MISAResource["VN"].ColorRed
            );
            break;
          default:
            break;
        }
      } catch (error) {
        this.emitter.emit(
          "onShowToastMessage",
          this.MISAResource["VN"].Failed,
          this.MISAResource["VN"].BackendError,
          this.MISAResource["VN"].IconWarningSmall,
          this.MISAResource["VN"].ColorRed
        );
      }
    },
    /**
     * refresh token
     * Author: TTPHong (13/3/2024)
     */
    refreshToken(){
      var tokenModel = JSON.parse(localStorage.getItem("Token")); 
      var user = JSON.parse(localStorage.getItem("User"));
      this.api.post(`/api/v1/Account/refresh-token`,tokenModel)
      .then(res =>{
          var data = res.data;
          tokenModel.AccessToken = data.AccessToken ;
          tokenModel.RefreshToken = data.RefreshToken ;
          user.RefreshToken = data.RefreshToken;
          localStorage.setItem("Token",JSON.stringify(tokenModel));
          localStorage.setItem("User",JSON.stringify(user));
                  location.reload();
      })
      .catch(error=>{
          this.emitter.emit("handleApiError",error);
      })
    },
    /**
     * thực hiện show loading
     */
    showLoading(){
      this.isLoading = true;
    },
    /**
     * thực hiện show loading
     */
    hiddenLoading(){
      this.isLoading = false;
    }
  },
}
</script>

<style>
@import url("./css/main.css");
#app {

  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

</style>
