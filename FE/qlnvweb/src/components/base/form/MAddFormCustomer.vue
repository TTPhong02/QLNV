<template>
<div>

    <div id="popup-sign"  pdialog class="p-popup-sign">
        <div class="p-popup__header">Thông tin khách hàng
            <button @click="CloseAddFormCustomer" class="p-dialog__button--close icon-button-close"></button>  
            <!-- <img id="btnCloseAddForm" class="close-popup-sign"  src="../assets/img/dialog_design_guideline/Layer 2.png" alt="closeicon"> -->
        </div>
        <form >
            <div class="p-popup__form">
                <div class="item-id">
                    <MTextfield typeInput="text" textPlaceHolder="Mã khách hàng" name="Mã khách hàng" textLabel="Mã khách hàng" v-model="customer.customerCode" :message="error.errors.customerCode" :required ="isRequired"> </MTextfield>
                </div>
                <div class="item-name">
                    <MTextfield typeInput="text" textPlaceHolder="Họ và tên đầy đủ" name="Họ và tên" idInput="txtCustomerFullName" textLabel="Họ và tên" v-model="customer.fullName" :message="error.errors.fullName" :required ="isRequired"> </MTextfield>
                </div>
                <div class="item-date">
                    <MTextfieldDate idInput="txtCustomerDateOfBirth" textLabel="Ngày sinh" v-model="customer.dateOfBirth"></MTextfieldDate>
                </div>
                <div class="item-gender">
                    <MCombobox textLabel="Giới tính"  :dataCombobox="dataCombobox.gender.genders"  @selected="onGenderSelected"></MCombobox>
                </div>
                <div class="item-phone">
                    <MTextfield typeInput="text" textPlaceHolder="Số điện thoại"  idInput="txtCustomerPhoneNumber" textLabel="Số điện thoại" v-model="customer.phoneNumber"> </MTextfield>
                </div>
                <div class="item-card">
                    <MTextfield typeInput="number" textPlaceHolder="Số CMTND"  idInput="txtCustomerCard" textLabel="Số CMTND" v-model="customer.card"> </MTextfield>
                </div>
                
                <div class="item-date">
                    <MTextfieldDate idInput="txtCustomerDateCard" textLabel="Ngày cấp" v-model="customer.dateCard"></MTextfieldDate>
                </div>
                <div class="item-email" >
                    <MTextfield typeInput="email" textPlaceHolder="example@gmail.com"  idInput="txtCustomerEmail" textLabel="Email" v-model="customer.email" :message="error.errors.email" > </MTextfield >
                </div>
                <div class="item-place">
                    <MTextfield typeInput="text" textPlaceHolder="Nơi cấp"  idInput="txtCustomerPlace" textLabel="Nơi cấp" v-model="customer.place"> </MTextfield>
                </div>
                <div class="item-money">
                    <MTextfield typeInput="number" textPlaceHolder="Sợ tiền nợ"  idInput="txtCustomerAmountDebit" textLabel="Số tiền nợ" v-model="customer.debitAmount"> </MTextfield>
                </div>
                <div class="item-company">
                    <MTextfield typeInput="text" textPlaceHolder="Công ty"  idInput="txtCustomerCompany" textLabel="Công ty" v-model="customer.companyName"> </MTextfield>
                </div>
                <div class="item-address">
                    <MTextfield typeInput="text" textPlaceHolder="Địa chỉ"  idInput="txtCustomerAddress" textLabel="Địa chỉ" v-model="customer.address"> </MTextfield>
                </div>
                <div class="p-popup-sign__button">
                    <div class="p-popup__button--left" >
                        <MButton className="p-button2" text="Hủy" @click="CloseAddFormCustomer"></MButton>          
                    </div>
                    <div class="p-popup__button--right" >
                        <MButton className="p-button1" text="Cất" @click="checkInvalid"></MButton> 
                        <MButton className="p-button1" text="Cất và thêm" @click="btnShowToast" ></MButton> 
                    </div>
                </div>

            </div>
            
        </form>
    
    <!-- <MToastDone :title="toast.title" :text="toast.text"></MToastDone> -->
    </div>
    <MDialog v-show="showDialog" :closeDialog="btnCloseDialog" :title="error.title" :errors="error.errors" ></MDialog>
    <MToastMessage v-model="toast.showToast" :title="toast.title" :text="toast.text"></MToastMessage>
</div>
</template>
<script>
import MCombobox from '../input/MCombobox.vue';
import MTextfield from '../input/MTextfield.vue';
import MTextfieldDate from '../input/MTextfieldDate.vue';  
 
export default {
    name:"MAddFormCustomer",
    props:[       
        "closeAddForm"
    ],
    components:{
        MTextfield,MTextfieldDate,MCombobox
    },
    data() {
        return {
            isRequired:true,
            showDialog : false,
            toast:{
                showToast: false,
                title:'',
                text:'',
            },
            dataCombobox : {
                gender:{
                    genders: ['Nam', 'Nữ', "Khác"],
                    selectedItem: '',
                }

            },
            error:{
                title : "Dữ liệu không hợp ",
                errors :{
                }
            },
            customer:{
                createdDate: null,
                createdBy: null,
                modifiedDate: null,
                modifiedBy: null,
                firstName: null,
                lastName: null,
                customerGroupId: null,
                memberCardCode: null,
                companyTaxCode: null,
                isStopFollow: null,
                customerCode: null,
                fullName: null,
                gender: null,
                address: null,
                dateOfBirth: null,
                email: null,
                phoneNumber: '',
                debitAmount: null,
                companyName: null,
            }
        }
    },
    methods: {

        onGenderSelected(selectedGender) {
            if(selectedGender === "Nam"){
                this.dataCombobox.gender.selectedItem = 0;
                
            }else if(selectedGender === "Nữ"){
                this.dataCombobox.gender.selectedItem = 1;
            }else if(selectedGender === "Khác"){
                this.dataCombobox.gender.selectedItem = 2;
            }else{
                this.dataCombobox.gender.selectedItem = null;
            }
            this.customer.gender = this.dataCombobox.gender.selectedItem;
            console.log(this.customer.gender );
        },
        updateSelectedItem(value) {
            this.dataCombobox.gender.selectedItem = value;
            console.log(this.dataCombobox.gender.selectedItem);
            console.log("abc");
        },
        /**
         * Hàm thực hiện hiển thị toast
         * Author: TTPhong(06/12/2023)
         */
        btnShowToast(){
            this.toast.showToast = true;
        },
        /**
         * Hàm thực hiện đóng Form thêm mới
         * Author: TTPhong (06/12/2023)
         */
        CloseAddFormCustomer(){
            this.closeAddForm();
        },
        /**
         * Hàm thực hiện hiển thị dialog cảnh báo
         * Author: TTPhong(06/12/2023)
         */
        btnShowDialog(){
            this.showDialog = true;
        },
        /**
         * Hàm thực hiện ẩn dialog cảnh báo
         * Author: TTPhong(06/12/2023)
         */
        btnCloseDialog(){
            this.showDialog = false;
        },

        /**
         * Click đẻ cất dữ liệu 
         * Author: TTPhong (06/12/2023)
         */
        btnSaveCustomer(){
            this.ValidateDataCustomer();
        },
        /**
         * Hàm kiểm tra dữ liệu
         * Author: TTPhong (26/12/2023)
         */
        checkInvalid(){
            try {
                if(this.customer.customerCode === "" ||this.customer.customerCode === null || this.customer.customerCode === undefined){
                    this.error.errors.customerCode = this.MISAResource["VN"].CustomerCodeNotEmpty;
                }else{
                   delete(this.error.errors.customerCode);
                }
                if(this.customer.fullName === "" ||this.customer.fullName === null || this.customer.fullName === undefined){
                    this.error.errors.fullName = this.MISAResource["VN"].FullNameNotEmpty;
                }else{
                    delete(this.error.errors.fullName);
                }
                if(this.customer.email === "" ||this.customer.email === null || this.customer.email === undefined){
                    this.error.errors.email = this.MISAResource["VN"].EmailNotEmpty;
                }else{
                    delete(this.error.errors.email);
                }
                if(this.error.errors.customerCode || this.error.errors.fullName || this.error.errors.email){
                    this.showDialog = true;
                }else{
                    this.addCustomer();
                }
            } catch (error) {
                console.error(error);
            }
        },
        /**
         * Hàm thực hiện thêm dữ liệu khách hàng
         * Author: TTPhong (06/12/2023)
         */
        addCustomer(){
            const customerData = this.customer;
            const headers = {
                'Content-Type': 'application/json',
                'accept': "*/*",
            }
            this.api.post(`${this.URLRequest}Customers`,customerData,headers)
            .then(response => {
                if(response.data === 1){
                    this.toast.title = this.MISAResource["VN"].Success;
                    this.toast.text = this.MISAResource["VN"].AddSuccess;
                    this.btnShowToast();
                    // this.CloseAddFormCustomer();
                }
            })
            .catch(error=>{
                const response = error.response;
                const status = response.status;
                switch(status){
                    case 404 :
                        this.toast.title= this.MISAResource["VN"].Failed;
                        this.toast.text = this.MISAResource["VN"].InvalidLink;
                        break;
                    default:
                        break;
                }
                this.btnShowToast();
            })

        },
    },
}
</script>
<style scoped>
#popup-sign {
    z-index: 10;
}
.p-popup{
    background-color: #fff;
    width: 348px;
    box-shadow:  0px 1px 30px 2px #888888;
}
.p-popup__header{
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 24px 24px;
    font-size: 24px;
    font-weight: 700;
}
.p-popup__header img{
    cursor: pointer;
    width: 24px;
    height: 24px;
    vertical-align: middle;
}
.p-popup__text{
    font-size: 14px;
    text-align: left;
    padding-left: 24px;
}
.p-popup__textfield{
    padding: 16px 24px 16px 24px;
}
.p-popup__combobox{
    padding: 0px 24px 24px 24px;
}
.p-popup-sign__button{
    grid-column-start: 1;
    grid-column-end: 5;
    border-top: 1px solid #E0E0E0;
    display: flex;
    justify-content: space-between;
    border-radius: 0px 0px 4px 4px;
}
.p-popup__button--left{
    padding: 12px 24px ;
}
.p-popup__button--right{
    padding: 12px 24px;
    text-align: right;
    display: flex;
    column-gap: 12px;
}
.p-popup-sign{
    /* display: none; */
    position: absolute;
    left: 11vw;
    top: 5vh;
    width: 1212px;
    border-radius: 4px;
    background-color: #fff;
    box-shadow:  0px 1px 30px 2px #888888;
}
.p-popup__form{
    padding: 0px 24px;
    display: grid;
    column-gap: 20px;
    row-gap: 18px;
    grid-template-columns: 250px 350px 250px 250px;
    grid-template-rows: 66px 66px 66px 66px 66px 66px 66px;
}
.item-phone{
    grid-column-start: 1;
    grid-column-end: 3;
}
.item-email{
    grid-column-start: 1;
    grid-column-end: 3;
}
.item-place{
    grid-column-start: 3;
    grid-column-end: 5;
}
.item-company,.item-address,.item-money{
    grid-column-start: 1;
    grid-column-end: 5;
}
.p-radiobutton2__option--item{
    display: flex;
    align-items: center;
    column-gap: 8px;
}
</style>