<template>
<div>
    <div id="popup-sign"  pdialog class="p-popup-sign">
        <div class="p-popup__header">Thông tin nhân viên cập nhật
            <button @click="CloseUpdateFormEmployee" class="p-dialog__button--close icon-button-close"></button>  
            <!-- <img id="btnCloseAddForm" class="close-popup-sign"  src="../assets/img/dialog_design_guideline/Layer 2.png" alt="closeicon"> -->
        </div>
        <form>
            <div class="p-popup__form">
                <div class="item-id">
                    <MTextfield  ref="employeeCode" typeInput="text" textPlaceHolder="Mã nhân viên" name="Mã nhân viên" textLabel="Mã nhân viên" v-model="employee.EmployeeCode" :message="dialog.errors.employeeCode" :required ="true"> </MTextfield>
                </div>
                <div class="item-name">
                    <MTextfield  ref="employeeFullName" typeInput="text" textPlaceHolder="Họ và tên đầy đủ" name="Họ và tên" idInput="txtEmployeeFullName" textLabel="Họ và tên" v-model="employee.FullName" :message="dialog.errors.fullName" :required ="true"> </MTextfield>
                </div>
                <div class="item-date">
                    <MTextfieldDate idInput="txtEmployeeDateOfBirth" textLabel="Ngày sinh" v-model="employee.DateOfBirth"></MTextfieldDate>
                </div>
                <div class="item-gender">
                    <MRadioGender v-model="employee.Gender"></MRadioGender>
                </div>
                <div class="item-department">
                    <MCombobox ref="employeeDepartment" textLabel="Đơn vị" :messageInvalid="dialog.errors.department" :required="true"  :dataCombobox="dataCombobox.department.departments.departmentName"  @selected="onComboboxDepartmentSelected" v-model="dataCombobox.department.selectedItem"></MCombobox>
                </div>   
                <div class="item-card">
                    <MTextfield  ref="employeeIdentityNumber" :message="dialog.errors.identityNumber"  typeInput="text" textPlaceHolder="Số CMTND"  idInput="txtEmployeeCard" textLabel="Số CMTND" v-model="employee.IdentityNumber"> </MTextfield>
                </div>
                <div class="item-date">
                    <MTextfieldDate idInput="txtEmployeeDateCard" textLabel="Ngày cấp" v-model="employee.IdentityDate"></MTextfieldDate>
                </div>
                <div class="item-position">
                    <MCombobox  textLabel="Chức danh"  :dataCombobox="dataCombobox.position.positions.positionsName"  @selected="onComboboxPositionSelected" v-model="dataCombobox.position.selectedItem"></MCombobox>
                </div>
                <div class="item-place">
                    <MTextfield typeInput="text" textPlaceHolder="Nơi cấp"  idInput="txtEmployeePlace" textLabel="Nơi cấp" v-model="employee.IdentityPlace"> </MTextfield>
                </div>
                 <div class="item-address">
                    <MTextfield typeInput="text" textPlaceHolder="Địa chỉ"  idInput="txtEmployeeAddress" textLabel="Địa chỉ" v-model="employee.Address"> </MTextfield>
                </div>
                <div class="item-phone">
                    <MTextfield typeInput="text" textPlaceHolder="Số điện thoại"  idInput="txtEmployeePhoneNumber" textLabel="Số điện thoại" v-model="employee.PhoneNumber"> </MTextfield>
                </div>  
                <div class="item-phone">
                    <MTextfield typeInput="text" textPlaceHolder="Số điện thoại cố định"  idInput="txtEmployeePhoneNumber" textLabel="Số điện thoại cố định"  v-model="employee.PhoneNumberFixed"> </MTextfield>
                </div> 
                <div class="item-email" >
                    <MTextfield  ref="employeeEmail" typeInput="email" textPlaceHolder="example@gmail.com"  idInput="txtEmployeeEmail" textLabel="Email" v-model="employee.Email" :message="dialog.errors.email" > </MTextfield >
                </div>    
                <div class="item-bankid">
                    <MTextfield typeInput="text" textPlaceHolder="Số tài khoản"  idInput="txtEmployeeAddress" textLabel="Số tài khoản ngân hàng" v-model="employee.BankAccount"> </MTextfield>
                </div>
                <div class="item-bankname">
                    <MTextfield typeInput="text" textPlaceHolder="Tên ngân hàng"  idInput="txtEmployeeAddress" textLabel="Tên ngân hàng" v-model="employee.BankName"> </MTextfield>
                </div>
                <div class="item-bankplace">
                    <MTextfield typeInput="text" textPlaceHolder="Chi nhánh ngân hàng"  idInput="txtEmployeeAddress" textLabel="Chi nhánh ngân hàng" v-model="employee.BankAddress"> </MTextfield>
                </div>
                <div class="p-popup-sign__button">
                    <div class="p-popup__button--left" >
                        <MButton className="p-button2" text="Hủy" @click="CloseUpdateFormEmployee"></MButton>          
                    </div>
                    <div class="p-popup__button--right" >
                        <MButton className="p-button2" text="Cất" @click="checkInvalid(true)"></MButton> 
                        <MButton className="p-button1" text="Cất và thêm" @click="checkInvalid(false)" ></MButton> 
                    </div>
                </div>

            </div>
            
        </form>
    
    <!-- <MToastDone :title="toast.title" :text="toast.text"></MToastDone> -->
    </div>
    <MDialog :text="dialog.text" :iconDialog="dialog.icon" v-if="dialog.showDialog" :dialogResFalse="btnSetFalseResponseDialog" :dialogResTrue="btnSetTrueResponseDialog" :title="dialog.title" :errors="dialog.errors" ></MDialog>
    <!-- <MToastMessage v-model="toast.showToast" :icon="toast.icon" :color="toast.textColor" :title="toast.title" :text="toast.text"></MToastMessage> -->
</div>
</template>
<script>
import MRadioGender from '../input/MRadioGender.vue';
import MCombobox from '../input/MCombobox.vue';
import MTextfield from '../input/MTextfield.vue';
import MTextfieldDate from '../input/MTextfieldDate.vue';  
 
export default {
    name:"MUpdateFormEmployee",
    props:[       
        "closeFormUpdate","idEmployee",
    ],
    components:{
        MTextfield,MTextfieldDate,MCombobox,MRadioGender
    },
    created() {
        this.takeDataDepartment();
        this.takeDataPosition();
        this.employee.EmployeeId = this.idEmployee;
        this.takeDataEmployeeUpdate();
        this.takeDataEmployeeCloneUpdate();
    },
    data() {
        return {
            isChangeForm:false,
            inputInValid:[],  
            isRequired:true,
            dialog:{
                showDialog : false,
                response:null,
                title: "",
                icon: "",
                text: "",
                errors:{}
            },
            toast:{
                showToast: false,
                title:'',
                text:'',
                icon:"",
                textColor:'',
            },
            employeeClone:{},
            employees:[],
            dataCombobox : {
                gender:{
                    genders: ['Nam', 'Nữ', "Khác"],
                    selectedItem: '',
                },
                department:{                   
                    departments:{
                        departmentId:[],
                        departmentName:[],
                    },
                    selectedItem: "",
                },
                position:{
                    positions:{
                        positionsId:[],
                        positionsName:[]
                    },
                    selectedItem:"",
                }
            },
            employee:{
                CreatedDate: null,
                CreatedBy: null,
                ModifiedDate: null,
                ModifiedBy: null,
                EmployeeCode: null,
                FullName: null,
                DateOfBirth: null,
                Gender: null,
                IdentityNumber: null,
                IdentityDate: null,
                IdentityPlace: null,
                Email: null,
                PhoneNumber: null,
                PhoneNumberFixed: null,
                Salary: null,
                JoinDate: null,
                WorkStatus: null,
                Address: null,
                BankAccount:null,
                BankName: null,
                BankAddress: null,
                DepartmentId: null,
                PositionsId: null,
            }
        }
    },
    watch:{
        // employee: {
        //     handler: 'watchChangeForm',
        //     deep: true
        // },
        'employee.DepartmentId':function(newValue){
            this.updateSelectedItemDepartment(newValue);
        },
        'employee.PositionsId':function(newValue){
            this.updateSelectedItemPosition(newValue);
        },
    },
    methods: {
        /**
         * Hàm thực hiện láy dữ liệu employee cập nhật
         * Author: TTPhong (22/01/2024)
         */
        async takeDataEmployeeUpdate(){
            await this.api.get(`${this.URLRequest}Employees/${this.employee.EmployeeId}`)
            .then(res=>{  
                console.log(res.data);
                this.employee = res.data; 
                this.takeNewEmployeeCode();   
            })
            .catch(error=>{
                    this.emitter.emit("handleApiError",error);
            })
        },
        /**
         * Hàm thực hiện láy dữ liệu employee cập nhật
         * Author: TTPhong (22/01/2024)
         */
        async takeDataEmployeeCloneUpdate(){
            await this.api.get(`${this.URLRequest}Employees/${this.employee.EmployeeId}`)
            .then(res=>{  
                console.log(res.data);
                this.employeeClone = res.data; 
                this.employee = this.employeeClone;
            })
            .catch(error=>{
                    this.emitter.emit("handleApiError",error);
            })
        },
        
       /**
         * Hàm thực hiện lấy dữ liệu phòng ban
         * Author: TTPhong(22/01/2024)
         */
        async takeDataDepartment(){
            await this.api.get(`${this.URLRequest}Departments`)
            .then(res=>{  
                var data  = res.data  
                for (let i = 0; i < data.length; i++) {
                    this.dataCombobox.department.departments.departmentName.push(data[i].DepartmentName);   
                    this.dataCombobox.department.departments.departmentId.push(data[i].DepartmentId);                 
                }   
            })
            .catch(error=>{
                this.emitter.emit("handleApiError",error);
            })

        },
        /**
         * Hàm thực hiện lấy dữ liệu vị trí
         * Author: TTPhong(22/01/2024)
         */
        async takeDataPosition(){
            await this.api.get(`${this.URLRequest}Positions`)
            .then(res=>{  
                var data  = res.data  
                for (let i = 0; i < data.length; i++) {
                    this.dataCombobox.position.positions.positionsName.push(data[i].PositionsName);   
                    this.dataCombobox.position.positions.positionsId.push(data[i].PositionsId);                 
                }   
            })
            .catch(error=>{
                this.emitter.emit("handleApiError",error);
            }) 
        },
        
                /**
         * Hàm thực hiện lấy mã nhân viên mới 
         * Author: TTPhong(22/01/2024)
         */
        takeNewEmployeeCode(){
            this.api.get(`${this.URLRequest}Employees/GetMaxEmployeeCode`)
            .then(res=>{
                this.employee.EmployeeCode = res.data;
            }).catch(error=>{
                 this.emitter.emit("handleApiError",error);
            })
        },
         /**
         * Hàm thực hiện focus vào ô đầu tiên
         * Author : TTphong(22/01/2024)
         */
        focusInputInvalid(inputFocus){
            this.$refs[inputFocus].focusToInput();
        },
        /**
         * Hàm thực hiện lấy giá trị combobox
         * Author: TTPhong (26/01/2024)
         */
        onComboboxDepartmentSelected(selectedCombobox) {
            for (let i = 0; i < this.dataCombobox.department.departments.departmentName.length; i++) {
                if(selectedCombobox === this.dataCombobox.department.departments.departmentName[i]){
                    this.dataCombobox.department.selectedItem = selectedCombobox;
                    this.employee.DepartmentId = this.dataCombobox.department.departments.departmentId[i];
                } 
            }

        },
        /**
         * Hàm thực hiện lấy giá trị combobox
         * Author: TTPhong (26/01/2024)
         */
        onComboboxPositionSelected(selectedCombobox) {
            for (let i = 0; i < this.dataCombobox.position.positions.positionsName.length; i++) {
                if(selectedCombobox === this.dataCombobox.position.positions.positionsName[i]){
                    this.dataCombobox.position.selectedItem = selectedCombobox;
                    this.employee.PositionsId = this.dataCombobox.position.positions.positionsId[i];
                } 
            }
        },
        /**
         * Hàm thực hiện cập nhật giá trị combobox
         * Author: TTPhong (26/01/2024)
         */
        updateSelectedItemPosition(id) {
            for (let i = 0; i < this.dataCombobox.position.positions.positionsId.length; i++) {
                if(id === this.dataCombobox.position.positions.positionsId[i]){
                    this.dataCombobox.position.selectedItem = this.dataCombobox.position.positions.positionsName[i];
                } 
            }
            
        },
        /**
         * Hàm thực hiện cập nhật giá trị combobox
         * Author: TTPhong (26/01/2024)
         */
        updateSelectedItemDepartment(id) {
            for (let i = 0; i < this.dataCombobox.department.departments.departmentName.length; i++) {
                if(id === this.dataCombobox.department.departments.departmentId[i]){
                    this.dataCombobox.department.selectedItem = this.dataCombobox.department.departments.departmentName[i];
                } 
            }
            
        },
        /**
         * Hàm thực hiện hiển thị toast
         * Author: TTPhong(06/12/2023)
         */
        btnShowToast(){
            this.toast.showToast = true;
        },
        /**
         * Hàm thực hiện kiểm tra xem dữ liệu form có thay đổi hay chưa
         * Author: TTPhong (29/01/2024)
         */
        checkDataChange(obj1,obj2){
            var keys1 = Object.keys(obj1);
            console.log(obj1,obj2);
            keys1.forEach(key => {
                if(key !='EmployeeCode'){
                    if(key == 'DateOfBirth' || key == 'IdentityDate'){
                        console.log("abc");
                        console.log(obj1[key]);
                        console.log(obj2[key]);
                        if(String(obj1[key]).substring(0,10) === obj2[key]){
                            console.log("bằng nhau");
                            return false;
                        }
                    }else{
                        if(obj1[key] != obj2[key]){
                            console.log(key);
                            return false;
                        }
                    }
                }
            });
        },
        /**
         * Hàm thực hiện đóng Form thêm mới
         * Author: TTPhong (06/12/2023)
         */
        CloseUpdateFormEmployee(){
            this.employee.EmployeeId = this.idEmployee;
            if(!this.checkDataChange(this.employeeClone,this.employee)){
                this.dialog.title = this.MISAResource["VN"].DataChanged;
                this.dialog.text = this.MISAResource["VN"].QuestionTextChangedFormData;
                this.dialog.icon = this.MISAResource["VN"].IconWarning;
                this.btnShowDialog();
            }else{
                this.closeFormUpdate();
            }
        },
        /**
         * Hàm thực hiện hiển thị dialog cảnh báo
         * Author: TTPhong(06/12/2023)
         */
        btnShowDialog(){
            this.dialog.showDialog = true;
        },
        /**
         * Hàm thực hiện ẩn dialog cảnh báo
         * Author: TTPhong(06/12/2023)
         */
        btnCloseDialog(){
            this.dialog.showDialog = false;
        },
        /**
         * Hàm thực hiện xóa hêt dữ liệu dialog
         * Author: TTPhong (26/01/2024)
         */
        resetDataDialog(){
            this.dialog.title=""
            this.dialog.icon=""
            this.dialog.text=""
            this.dialog.errors={};
        },
        /**
         * Hàm set respone dialog
         *  Author: TTPhong(22/01/2024)
         */
        btnSetTrueResponseDialog(){
            this.dialog.response = true;
            this.dialog.showDialog = false;
            if(Object.keys(this.dialog.errors).length > 0){
                this.focusInputInvalid(this.inputInValid[0]);
            }
            if(Object.keys(this.dialog.errors).length === 0 && this.isChangeForm === true){
                this.checkInvalid(true);
            }
            // this.resetDataDialog();
        },
        /**
         * Hàm set respone dialog
         *  Author: TTPhong(22/01/2024)
         */
        btnSetFalseResponseDialog(){
            this.dialog.response = false;
            this.dialog.showDialog = false;
            if(Object.keys(this.dialog.errors).length > 0){
                this.focusInputInvalid(this.inputInValid[0]);
            }
            this.closeFormUpdate();
            // this.resetDataDialog();
        },
        /**
         * Click đẻ cất dữ liệu 
         * Author: TTPhong (06/12/2023)
         */
        btnSaveCustomer(){
            this.ValidateDataCustomer();            
        },
        /**
         * Hàm kiểm tra phần tử có trong mảng
         *  Author: TTPhong(22/01/2024)
         */
        checkElementFromArray(arr, elementToCheck){
            for (let i = 0; i < arr.length; i++) {
                if(elementToCheck === arr[i]){
                    return true;
                }
                return false;
                
            }
        },
        /**
         * Hàm xóa phần tử khỏi mảng
         *  Author: TTPhong(22/01/2024)
         */
        removeElementFromArray(arr, elementToRemove) {
            const indexToRemove = arr.indexOf(elementToRemove);
            if (indexToRemove !== -1) {
                arr.splice(indexToRemove, 1);
            }
        },
        /**
         * Hàm thực hiện kiểm tra dữ liệu
         *  Author: TTPhong(22/01/2024)
         */
        checkInvalid(closeForm){
            try {
                //1.Check mã nhân viên
                if(this.employee.EmployeeCode === "" ||this.employee.EmployeeCode === null || this.employee.EmployeeCode === undefined){
                    this.dialog.errors.employeeCode = this.MISAResource["VN"].EmployeeCodeNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "employeeCode")){
                        this.inputInValid.push("employeeCode");
                    }

                }else{
                   delete(this.dialog.errors.employeeCode);
                   this.inputInValid=[];
                }
                //2.Check họ tên nhân viên
                if(this.employee.FullName === "" ||this.employee.FullName === null || this.employee.FullName === undefined){
                    this.dialog.errors.fullName = this.MISAResource["VN"].FullNameNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "employeeFullName")){
                        this.inputInValid.push("employeeFullName");
                    }
                }else{
                    delete(this.dialog.errors.fullName);
                    this.removeElementFromArray(this.inputInValid, "employeeFullName") ;
                }
                //5.Check đơn vị
                if(this.dataCombobox.department.selectedItem === "" ||this.dataCombobox.department.selectedItem === null ||this.dataCombobox.department.selectedItem === undefined){
                    this.dialog.errors.department = this.MISAResource["VN"].DepartmentNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "employeeDepartment")){
                        this.inputInValid.push("employeeDepartment");
                    }
                }else{
                    delete( this.dialog.errors.department);
                    this.removeElementFromArray(this.inputInValid, "employeeDepartment") ;
                }

                if(this.dialog.errors.employeeCode || this.dialog.errors.fullName || this.dialog.errors.department){
                    this.dialog.title= this.MISAResource["VN"].InvalidData;
                    this.dialog.icon = this.MISAResource["VN"].IconWarning;
                    this.btnShowDialog();
                }else{
                    this.updateEmployee(closeForm);
                }
            } catch (error) {
                this.emitter.emit("handleApiError",error);
            }
        },
        /**
         * Hàm thực hiện update employee
         *  Author: TTPhong(22/01/2024)
         */

        updateEmployee(closeForm){
            const employeeData = this.employee;
            this.api.put(`${this.URLRequest}Employees/${this.employee.EmployeeId}`,employeeData)
            .then(response => {
                console.log(response);
                if(response.data.Data === 1){
                    this.toast.title =this.MISAResource["VN"].Success;
                    this.toast.text = this.MISAResource["VN"].UpdateSuccess;
                    this.toast.icon = this.MISAResource["VN"].IconSuccessSmall;
                    this.emitter.emit("onShowToastMessage", this.toast.title, this.toast.text,this.toast.icon,this.toast.textColor)
                    this.btnShowToast();
                    setTimeout(() => {
                        this.emitter.emit('loadDataPagingEmployee');
                        if(closeForm === true){
                            this.closeFormUpdate();
                        }
                        this.takeNewEmployeeCode();
                    }, 2000);
                    
                }
            })
            .catch(error =>{
                const response = error.response;
                this.emitter.emit("handleApiError",error);
                const status = response.status;
                switch(status){
                    case 400:
                        this.dialog.title= this.MISAResource["VN"].InvalidData;
                        this.inputInValid = [];
                        this.inputInValid.push("employeeCode");
                        this.dialog.text = response.data.Errors+ " " + this.employee.EmployeeCode ;
                        this.dialog.icon = this.MISAResource["VN"].IconDangerous;
                    break;
                    default:
                        break;
                }
                this.btnShowDialog();
            })
        }
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
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
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

.item-money,.item-department,.item-position{
    grid-column-start: 1;
    grid-column-end: 3;
}
.item-place{
    grid-column-start: 3;
    grid-column-end: 5;
}
.item-bankid{
    grid-column-start: 1;
    grid-column-end: 2;
}
.item-address{
    grid-column-start: 1;
    grid-column-end: 5;
}
.p-radiobutton2__option--item{
    display: flex;
    align-items: center;
    column-gap: 8px;
}
</style>