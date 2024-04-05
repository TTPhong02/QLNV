<template>
    <div class="import-list">
        <table id="p-tblCustomer" class="p-table">
            <thead>
                <tr class="p-table__row ">
                    <th>Mã nhân viên</th>
                    <th>Tên nhân viên</th>
                    <th>Giới tính</th>
                    <th>Ngày sinh</th>
                    <th>Số CMND</th>
                    <th>Chức danh</th>
                    <th>Tên đơn vị</th>
                    <th>Số tài khoản</th>
                    <th>Tên ngân hàng</th>
                    <th>Chi nhánh tk ngân hàng</th>
                    <th>Tình trạng</th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in employeeCheckList" :key="item.EmployeeId" >
                    <td >{{item.EmployeeCode}}</td>
                    <td>{{item.FullName}}</td>
                    <td>{{item.Gender}}</td>
                    <td class="date">{{formatDate(item.DateOfBirth)}}</td>
                    <td>{{item.IdentityNumber}}</td>
                    <td>{{item.DepartmentName}}</td>
                    <td>{{item.PositionsName}}</td>
                    <td>{{item.BankAccount}}</td>
                    <td>{{item.BankName}}</td>
                    <td>{{item.IdentityPlace}}</td>
                    <td>
                      <div v-if="item.IsImported">
                          Hợp lệ
                      </div>
                      <div v-for="err in item.ImportErrors" :key="err.index" class="red">
                          {{err}}
                      </div>
                    </td>
                </tr>
            </tbody>
        </table> 
    </div>
</template>

<script>
export default {
  name:"MImportCheckList",
  props:[
    "checkList"
  ],
  created() {
    this.employeeCheckList = this.checkList;
  },
  data() {
    return {
      employeeCheckList:[],
    }
  },
  methods: {
     
      btnCloseImportList(){
        this.closeImportList();
      },
      formatDate(string){
            try{
                let date = new Date(string);
                let day = date.getDate();
                let month = date.getMonth() + 1;
                let year = date.getFullYear();
                let formattedDay = (day < 10) ? `0${day}` : `${day}`;
                let formattedMonth = (month < 10) ? `0${month}` : `${month}`;
                return`${formattedDay}/${formattedMonth}/${year}`;
            }catch(error){
                console.error(error);
            }
        },
        /**
        * Hàm thực hiện Format dữ liệu tiền
        * Author: TTPhong (06/12/2023)
        */
       formatMoney(string) {
            try{
                    const moneyValue = parseFloat(string);
                    return Math.round(moneyValue).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
            }catch(error){
                console.error(error);
            }
        },
        /**
         * Hàm thực hiện Format giới tính 
         * Author: TTPhong (06/12/2023)
         */
        formatGender(value){
            try{
                if(value === 1){
                    return "Nam";
                }else if(value === 0) 
                {
                    return "Nữ";
                }
                 return "Khác";   
                    
            }catch(error){
                console.error(error);
            }
        }
  },
}
</script>

<style scoped>
th{
  border: 1px solid #ccc;
  cursor: pointer;
}
.import-list{
  width: 100%;
  height: 100%;
  overflow: scroll;
}
.p-table tbody tr:hover{
  background-color: #E0E0E0;
}
.red{
  color: red;
}
</style>