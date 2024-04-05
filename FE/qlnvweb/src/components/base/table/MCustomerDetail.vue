<template>
    <table id="p-tblCustomer" class="p-table small-first-col">
        <thead>
            <tr class="p-table__row ">
                <th>
                    <input type="checkbox" id="allchecked" class="checkAll">
                </th>
                <th>Mã khách hàng</th>
                <th>Họ tên</th>
                <th>Giới tính</th>
                <th>Ngày sinh</th>
                <th>Địa chỉ</th>
                <th>Email</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item in customers" :key="item.CustomersId" class="p-table__row">
                <td ><input type="checkbox"></td>
                <td>{{item.CustomerCode}}</td>
                <td>{{item.FullName}}</td>
                <td>{{formatGender(item.Gender)}}</td>
                <td class="date">{{formatDate(item.DateOfBirth)}}</td>
                <td>{{item.Address}}</td>
                <td>{{item.Email}}</td>
                <div class="p-tbl-option-fixed">
                    <div class="p-tbl-option-fixed-position">
                        <span class="p-button-table-option icon-button-update">                      
                        </span>
                        <span class="p-button-table-option rotate  icon-button-expand">                   
                        </span>
                    </div>
                </div>
            </tr>
        </tbody>
    </table> 
</template>
<script>
export default {
    name: 'MTableCustomer',
    created() {

        this.api.get("https://localhost:7134/api/v1/Customers")
        .then(res=>{
            console.log(res.data);
            this.customers = res.data;
        })
        .catch(error=>{
            const response = error.response;
            console.log(response);
        })
        
    },
    methods: {
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
                }else if(value === 2) 
                {
                    return "Nữ";
                }
                 return "Khác";   
                    
            }catch(error){
                console.error(error);
            }
        }

    },
    data() {
        return{
            customers:[]
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

