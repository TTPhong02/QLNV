<template>
    <div class="import-first">
        <div class="import-first__title">
            Chọn dữ liệu khách hàng đã chuẩn bị để nhập khẩu
        </div>
        <div class="import-first__form">
            <input @change="handleTakeFile" ref="inputImport" type="file">
        </div>
    </div>
</template>

<script>
export default {
    name: "MImportSelectFile",
    props:[
        "checkList"
    ],
    data() {
        return {
            fileImport:null,
            employeeCheckList:[]
        }
    },
    watch:{
        fileImport(newValue){
            this.importEmployee(newValue);
        }
    },
    methods: {
        /**
         * Hàm lấy file khi import
         * Author: TTPhong(29/01/2024)
         */
        handleTakeFile(){
            this.fileImport = this.$refs.inputImport.files[0];
        },
        /**
         * Hàm thực hiện import employee
         * Author: TTPhong(29/01/2024)
         */
        importEmployee(file){
            let formData = new FormData();
            formData.append("fileImport", file);
            this.api.post("https://localhost:7134/api/v1/Employees/Import",formData,{
            }).then(res=>{
                this.employeeCheckList = res.data;   
                this.$emit('checklist-update',this.employeeCheckList);  
                console.log(this.employeeCheckList);         
            })
            .catch(error=>{
                this.emitter.emit("handleApiError",error);
            })
        },
       

    },
}
</script>

<style>
.import-first__title{
    padding: 5px 0px;
    font-size: 17px;
}
.import-first{
    margin: 10px;
}
</style>