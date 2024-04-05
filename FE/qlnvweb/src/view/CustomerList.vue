<template>
<div>
    <div class="p-home__main--header">
        <div class="p-home__main--header--title">
            Khách hàng
            <img src="../assets/img/popup_design_guideline/Layer 2.png" alt="icondown">
        </div>
        <div class="p-home__main--header--button">
            <button @click="btnShowFormAddCustomer" id="showAddForm" class="p-button1-plus show-popup-sign">
                <i class="fa-solid fa-plus"></i>
                Thêm mới
            </button>
            <button class="p-button3" >
                <i class="fa-solid fa-ellipsis"></i>
            </button>
        </div>
    </div>
        <div class="p-home__main--action">
            <div class="left p-home__main--action--button">
                <div class="p-table__number">Đã chọn <strong id="selected-count"></strong></div>
                <div class="p-table__unselect">Bỏ chọn</div>
                <button  class="p-table__deleteall p-button2 show-home-dialog">
                    <i class="fa-solid fa-trash-can"></i>
                    Xóa tất cả
                </button>
            </div>
            <div class="p-home__main--action--button">
                <button class=" p-button3 ">
                    <i class="fa-solid fa-file-arrow-down"></i>
                </button>
                <button id="p-btnLoadData"  class="p-button3 ">
                    <i class="fa-solid fa-arrows-rotate"></i>
                </button>
            </div>
        </div>
        <div class="p-home__main--table">
            <div class="p-grid">
                   <MTableCustomer :loadData ="btnLoadDataCustomers"></MTableCustomer>
            </div>
            <div class="p-footertable">
                <div class="left p-footertable__total">
                    Total: <span id="numberrow"></span> records
                </div>
                <div class="right p-footertable__action">
                    <div class="p-footertable__action--page">Record/Page</div>
                    <div class="p-footertable__action--numbercombobox">50 </div>
                    <div class="p-footertable__action--numberrecord"><strong>1-8</strong> records</div>
                    <div class="p-footertable__action--button">
                        <i class="fa-solid fa-chevron-left"></i>
                        <i class="fa-solid fa-chevron-right"></i>
                    </div>
                </div>
            </div>            
        </div>
    <MAddFormCustomer v-show="showForm" :closeAddForm="btnCloseFormAddCustomer" ></MAddFormCustomer>
    <!-- <MToastMessage v-if="showToast"></MToastMessage> -->
</div>
</template>closeFormAddCustomer
<script>
import MTableCustomer from "../components/base/table/MCustomerDetail.vue";
import  MAddFormCustomer  from "../components/base/form/MAddFormCustomer.vue";

export default {
    name: "MCustomerPage",
    components: {
        MAddFormCustomer,
        MTableCustomer
    },
        mounted() {
        document.title="Khách hàng | MISA CukCuk"
    },
    beforeUnmount() {
    
        document.title = "MISA CukCuk"
    },
    methods :{
        /**
         * Hàm thực hiện hiển thị form thêm mới
         * Author: TTPhong (06/12/2023) 
         */
        btnShowFormAddCustomer(){
            this.showForm = true;
        },
        /**
         * Hàm thực hiện ẩn form thêm mới
         * Author: TTPhong (06/12/2023) 
         */
        btnCloseFormAddCustomer(){
            this.showForm = false;
        },
    /**
             * Hàm thực hiện load dữ liệu khách hàng
             * Author: TTPhong(22/01/2024)
             */
        btnLoadDataCustomers(){
            fetch(`${this.URLRequest}/Customers`)
            .then(res => res.json())
            .then(data => {
                this.customers = data;
            })
        }
    },
    data(){
        return{
            showForm : false,
            showToast: false,
            customers:[]
        }
    }
}
</script>
<style>
    @import url('../css/layout/home.css');
</style>