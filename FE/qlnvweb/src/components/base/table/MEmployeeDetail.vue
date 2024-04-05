<template> 
<div>
    <div>
        <table id="p-tblCustomer" class="p-table small-first-col">
            <thead>
                <tr class="p-table__row ">
                    <th>
                        <input type="checkbox"  @click="selectAll" v-model="allSelected" class="checkAll">
                    </th>
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
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in employees" :key="item.EmployeeId" class="p-table__row">
                    <td ><input v-model="checkedIDs" @click="select" :value="item.EmployeeId" type="checkbox"></td>
                    <td >{{item.EmployeeCode}}</td>
                    <td>{{item.FullName}}</td>
                    <td>{{formatGender(item.Gender)}}</td>
                    <td class="date">{{formatDate(item.DateOfBirth)}}</td>
                    <td>{{item.IdentityNumber}}</td>
                    <td>{{takeDepartmentName(item.DepartmentId)}}</td>
                    <td>{{takePositionsName(item.PositionsId)}}</td>
                    <td>{{item.BankAccount}}</td>
                    <td>{{item.BankName}}</td>
                    <td>{{item.IdentityPlace}}</td>
                    <div class="p-tbl-option-fixed">
                        <div class="p-tbl-option-fixed-position">
                            <span @click="btnShowUpdateForm(item.EmployeeId)"  class="p-button-table-option icon-button-update">                      
                            </span>
                            <span v-if="showOptionAction" @click="this.emitter.emit('deleteEmployee',item.EmployeeId)"  class="p-button-table-option p-button-table-option-none icon-button-delete">                      
                            </span>
                            <span v-if="showOptionAction" @click="btnShowDuplicateForm(item.EmployeeId)" class="p-button-table-option p-button-table-option-none icon-button-duplicate">                      
                            </span>
                            <span v-on:click="showOptionAction = !showOptionAction"  class="p-button-table-option rotate icon-button-expand">                   
                            </span>
                        </div>
                    </div>
                </tr>
            </tbody>
            
        </table> 
        <!-- <MDialog v-if="dialog.showDialog" :text="button.delete.text" :dialogResFalse="btnSetFalseResponseDialog" :dialogResTrue="btnSetTrueResponseDialog"  :iconDialog="button.delete.icon" :title="button.delete.title" ></MDialog> -->
        <!-- <MToastDone v-model="toast.showToast" :color="toast.colorText" :icon="toast.icon" :title="toast.title" :text="toast.text"></MToastDone> -->
    </div>
</div>
</template>

<script>

export default {
    name: 'MTableEmployee',
    props:[
        "showFormUpdate","showFormDuplicate","dialogData","filterParent","nextPageNumber","previousPageNumber"
    ]
    ,
    data() {
        return{
            token:null,
            employees:[],

            selected: [],
            allSelected: false,
            checkedIDs:[],
            department:{},
            positions:{},
            idEmployee:null,
            showUpdate: false,
            showDuplicate:false,
            showOptionAction : false,
            dialog: {
                showDialog: false,
                response: false,
                errors:[],
                title: "",
                icon: "",
                text: ""
            },
            
            totalPage:0,
            totalRecord:0,
            filter:{
                paging:{
                    pageSize:0,
                    pageNumber: 0,
                    searchString:""
                }
            },
            toast:{
                showToast: false,
                title:'',
                text:'',
                colorText:''
            },

        }
    },
    watch:{
            /**
     * Hàm lắng nghe biến pageSize thay đổi và thực hiện gọi lại hàm phân trang
     *  Author: TTPhong(22/01/2024)
     */
        'filter.paging.pageSize':function(){
            this.loadDataPagingEmployee();
        },
               /**
     * Hàm lắng nghe biến searchString thay đổi và thực hiện gọi lại hàm phân trang
     *  Author: TTPhong(22/01/2024)
     */
        'filter.paging.searchString':function(newValue){
            if(newValue === ""){

                this.loadDataPagingEmployee();
            }
        },
            /**
     * Hàm gửi lại đến cha khi biến totalRecord thay đổi
     *  Author: TTPhong(22/01/2024)
     */
        totalRecord(newValue){
            this.$emit('totalRecord',newValue)
        },
            /**
     * Hàm gửi lại lên cha khi biến TotalPage thay đổi
     *  Author: TTPhong(22/01/2024)
     */
        totalPage(newValue){
            this.$emit('totalPage',newValue)
        },
               /**
     * Hàm gửi lại lên cha khi biến checkedIDs thay đổi
     *  Author: TTPhong(22/01/2024)
     */
        checkedIDs(newValue){
            this.$emit('checkedIDs',newValue)
        },
               /**
     * Hàm gửi lại lên cha khi biến employees thay đổi
     *  Author: TTPhong(22/01/2024)
     */
        employees(newValue){
            this.$emit('employees',newValue)
        },
    },
    created() {
        this.token = localStorage.getItem("Token");
        this.dialog = this.dialogData;
        this.filter = this.filterParent;
        this.loadDataDepartment();
        this.loadDataPositions();
        this.emitter.on("loadDataPagingEmployee",this.loadDataPagingEmployee);
        this.emitter.on("deselectAll",this.deselectAll);  
        this.emitter.on("nextPageEmployee",this.nextPageEmployee);
        this.emitter.on("previousPageEmpolyee",this.previousPageEmpolyee);  
    },
    methods: {
        /**
         * Hàm thực hiện hiển thị toast
         * Author: TTPhong(06/12/2023)
         */
        btnShowToast(){
            this.toast.showToast = true;
        },
            /**
     * Hàm thực hiện khi nhấn sang trang và gọi hàm hàm phân trang 
     *  Author: TTPhong(22/01/2024)
     */
        nextPageEmployee(){
            this.filter.paging.pageNumber +=1
            if(this.filter.paging.pageNumber > this.totalPage){
                this.filter.paging.pageNumber = this.totalPage;
            }else{
                this.loadDataPagingEmployee();
            }
        },
        /**
         * Hàm thực hiện khi nhấn quay lại trang trước và gọi hàm hàm phân trang 
         *  Author: TTPhong(22/01/2024)
         */
        previousPageEmpolyee(){
            this.filter.paging.pageNumber -= 1
            if(this.filter.paging.pageNumber == 0){
                this.filter.paging.pageNumber = 1;
            }
            else{
                this.loadDataPagingEmployee();
            }
        },
        /**
         * Hàm thực hiện loadData
         * Author:TTPhong(23/01/2024)
         */
          loadDataPagingEmployee(){
            var Token = JSON.parse(localStorage.getItem("Token")).AccessToken
            this.emitter.emit("showLoading");
            const pageSize = this.filter.paging.pageSize;
            const pageNumber = this.filter.paging.pageNumber;
            const searchString = this.filter.paging.searchString;
             this.api.get(`/api/v1/Employees/Filter`,{
                    params: {
                        pageSize: pageSize,
                        pageNumber: pageNumber,
                        searchString: searchString
                    },
                    headers: {
                        Authorization: `Bearer ${Token}`,
                        "Content-Type": "application/json"
                    }
                }  )
            .then(res=>{
                if(res.data){
                    this.emitter.emit("hiddenLoading")
                } 
                this.totalPage = res.data.TotalPage 
                this.totalRecord = res.data.TotalRecord 
                this.employees = res.data.Data;
            })
            .catch(error=>{
                console.log(error);
                this.emitter.emit("handleApiError",error);
                setTimeout(() => {
                    this.emitter.emit("hiddenLoading")
                }, 1000);
            })
        },
        /**
         * Hàm lấy tên thông tin datacombobox đơn vị
         * Author: TTPhong(22/01/2024)
         */
        async loadDataDepartment(){
            await this.api.get(`${this.URLRequest}Departments`)
            .then(response=>{
                this.department = response.data;
                //  this.department.departmentId = response.data.DepartmentId
                //  this.department.departmentName = response.data.DepartmentName
            })
            .catch(error=>{
                console.log(error);
                this.emitter.emit("handleApiError",error);
            })
        },
         /**
         * Hàm lấy thông tin datacombobox Chức danh 
         * Author: TTPhong(22/01/2024)
         */
        async loadDataPositions(){
            try{
                await this.api.get(`${this.URLRequest}Positions`)
                .then(response=>{
                     this.positions = response.data;
                })
                .catch(error=>{
                    this.emitter.emit("handleApiError",error);
                })

            }catch(error){
                console.error(error);
            }
        },
         /**
         * Hàm lấy tên đơn vị 
         * Author: TTPhong(22/01/2024)
         */
        takeDepartmentName(id){
            for (let i = 0; i < this.department.length; i++) {
                if(id === this.department[i].DepartmentId){
                    return this.department[i].DepartmentName;
                }
            }
        }, 
        /**
         * Hàm lấy tên chức danh
         * Author: TTPhong(22/01/2024)
         */
        takePositionsName(id){
            for (let i = 0; i < this.positions.length; i++) {
                if(id === this.positions[i].PositionsId){
                    return this.positions[i].PositionsName;
                }
            }
        },
        /**
         * Hàm xóa CheckAll 
         * Author: TTPhong(22/01/2024)
         */
        deselectAll(){
            this.checkedIDs = [];
            this.allSelected = false;
        },
        /**
         * Hàm CheckAll 
         * Author: TTPhong(22/01/2024)
         */
        selectAll(){
            this.checkedIDs = [];
            if (!this.allSelected) {
                // this.employees.forEach(element => {
                //     this.checkedIDs.push(element.EmployeeId);
                // });
                for (let i = 0 ; i <  this.employees.length; i++) {
                    this.checkedIDs.push(this.employees[i].EmployeeId)
                }
            }
        },
        /**
         * Hàm thưc hiện khi chọn 1 trong data combobox
         * Author: TTPhong(22/01/2024)
         */
        select(){
            this.allSelected = false;
            // console.log(this.checkedIDs);
        },

        /**
         * Hàm set respone dialog
         *  Author: TTPhong(22/01/2024)
         */
        btnSetTrueResponseDialog(){
            this.dialog.response = true;
            this.dialog.showDialog = false;
        },
        btnSetFalseResponseDialog(){
            this.dialog.response = false;
            this.dialog.showDialog = false;
        },
        /**
         * Hàm show form update
         *  Author: TTPhong(22/01/2024)
         */
        btnShowUpdateForm(id){
            this.showFormUpdate(id);
        },
        /**
         * Hàm show form update
         *  Author: TTPhong(22/01/2024)
         */
        btnShowDuplicateForm(id){
            this.showFormDuplicate(id);
        },
        
        
        /**
        * Hàm thực hiện Format dữ liệu ngày tháng năm
        * Author: TTPhong (06/12/2023)
        */
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

    }
}
</script>
<style scoped>
    .p-button-table-option{
        padding: 10px 10px;
        width: 15px;
        height: 15px;
        border-radius: 50%;
        background-color: #fff;
        margin: 5px;
        box-shadow: #ccc 0px 0px 8px 1px;
    }
    .p-tbl-option-fixed{
        display: none;
        width: 0;
        position: sticky;
        right: 0;
        height: 44px;
    }
    .p-table__row:hover .p-tbl-option-fixed{
        display: block;
    }
    .p-tbl-option-fixed-position{
        position: absolute;
        height: 44px;
        right: 0;
        display: flex;
        align-items: center;
    }
    .rotate {
        rotate: 90deg;
    }
</style>

