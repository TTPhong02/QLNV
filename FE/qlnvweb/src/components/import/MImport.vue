<template>
<div class="p-import">
    <div class="p-import-heading">
        <h2>Nhập khẩu nhân viên</h2>
        <button @click="btnCloseImport"  class=" icon-button-close"></button>
    </div>
    <div class="p-import-sidebar">
        <div class="p-sidebar">
            <div class="p-sidebar__list">
                <div @click="btnSelectFile" :class="{'p-sidebar__list--item':true , 'sidebar-selected' : isSelectFile} ">
                    1. Chọn tệp nguồn
                </div>
                <div @click="btnCheckList" :class="{'p-sidebar__list--item':true , 'sidebar-selected' : isCheckList} ">
                    2. Kiểm tra dữ liệu
                </div>
                <div @click="btnResult" :class="{'p-sidebar__list--item':true , 'sidebar-selected' : isResult} ">
                    3. Kết quả nhập khẩu
                </div>
            </div>
        </div>
    </div>
    <div class="import-main">
        <MImportSelectFile @checklist-update="handleTakeCheckList" v-if="isSelectFile"></MImportSelectFile>
        <MImportCheckList :checkList="listCheckImport" v-if="isCheckList"></MImportCheckList>
        <MImportResult :checkList="listCheckImport" v-if="isResult"></MImportResult>
    </div>
</div>
</template>

<script>
import MImportSelectFile from "./MImportSelectFile.vue";
import MImportResult from "./MImportResult.vue";
import MImportCheckList from "./MImportCheckList.vue";

export default {
    name: 'MImport',
    components : {
        MImportSelectFile,MImportResult,MImportCheckList
    },
    props:[
        "closeFormImport"
    ],
    data() {
        return {
            isSelectFile:true,
            isCheckList:false,
            isResult:false,
            listCheckImport:[],
        }
    },
    methods: {
        /**
         * Hàm thực hiện lấy file từ input khi chọn
         * Author : TTPhong (29/01/2024)
         */
        handleTakeCheckList(checkList){
            this.listCheckImport = checkList;
        },
        /**
         * hàm thực hiện nhấn sidebar chọn file
         * Author : TTPhong (29/01/2024)
         */
        btnSelectFile(){
            this.isCheckList = false;
            this.isResult = false
            this.isSelectFile=true;
        },
        /**
         * hàm thực hiện nhấn sidebar chọn file
         * Author : TTPhong (29/01/2024)
         */
        btnCheckList(){
            this.isResult = false;
            this.isSelectFile=false;
            this.isCheckList = true;
        },
        /**
         * hàm thực hiện nhấn sidebar chọn file
         * Author : TTPhong (29/01/2024)
         */
        btnResult(){
            this.isCheckList = false;
            this.isSelectFile=false;
            this.isResult = true;
        },
        /**
         * Hàm thực hiện đóng form import
         * Author: TTPhong(29/01/2024)
         */
        btnCloseImport(){
            this.closeFormImport();
            this.emitter.emit("loadDataPagingEmployee");
        }
    }
}
</script>

<style scoped>
    @import url('../../css/main.css');
    a{
        text-decoration: none;
    }
    .sidebar-selected{
        background-color: #27395E;
        border-radius: 4px;
        color: #fff;
    }
    .p-sidebar__list--item{
        padding: 0px 12px;
    }
    .p-import{
        background-color: #FFF ;
        position: absolute;
        height: calc(100vh - 50px);
        width:calc(100vw - 100px);
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        z-index: 100;
        border: 1px solid #ccc;
        border-radius: 5px;
    }
    .p-import-heading h2,
    .p-import-heading button{
        padding: 0px 20px;
    }
    .p-import-heading{
        display: flex;
        justify-content: space-between;
        align-items: center;
        height: 56px;
        background-color: #F5F7F8;
        width: 100%;
        box-shadow: 0px 1px 10px 2px #ccc;
    }
    .p-import-sidebar{
        float: left;
        width: 200px;
        height: calc(100% - 56px);
        background-color: #e1e1e1;
    }
    .import-main{
        width: calc(100% - 200px);
        height: calc(100% - 56px);
        float: left;
    }
</style>