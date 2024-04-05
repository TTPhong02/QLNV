<template>
    <div>
        <div class="import-third">
        <h2>Kết quả nhập khẩu</h2>
        <div class="import-third__title">
            Tải về tập tin chú kết quả <a href="">tại đây</a>
        </div>
        <div class="import-third__title">
            Số dòng nhập khẩu thành công: {{importSuccess}}
        </div>
        <div class="import-third__title">
            Số dòng nhập khẩu không thành công: {{importFaild}} 
        </div>
    </div>
    </div>
</template>

<script>
export default {
  name:"MImportResult",
  props:[
    "checkList"
  ],
  created() {
    this.employeeCheckList = this.checkList;
  },
  data() {
    return {
      employeeCheckList:[],
      importSuccess: 0,
      importFaild:0,
    }
  },
  watch:{
    employeeCheckList(newValue){
        this.countImport(newValue);
    }
  },
  methods: {
    countImport(list){
        list.forEach(item => {
            if(item.IsImported === true){
                this.importSuccess++;
            }else{
                this.importFaild++;
            }
        });
    }
  },
}
</script>

<style>
.import-third__title{
    padding: 5px 0px;
}
.import-third{
    margin: 10px;
}
</style>