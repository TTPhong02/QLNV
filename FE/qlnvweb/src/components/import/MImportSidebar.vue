<template>
<div class="p-import-sidebar">
    <div class="p-sidebar">
        <div class="p-sidebar__list">
            <div class="p-sidebar__list--item">
                1. Chọn tệp nguồn
            </div>
            <div class="p-sidebar__list--item">
                2. Kiểm tra dữ liệu
            </div>
            <div class="p-sidebar__list--item">
                3. Kết quả nhập khẩu
            </div>
        </div>
        
    </div>
</div>
</template>

<script>
export default {
    name:"MImportSidebar"
}
</script>

<style>
.p-sidebar{
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;

}

.p-sidebar__list{
    margin-top: 24px;
}
.p-sidebar__shorter--item,
.p-sidebar__list--item{
    display: flex;
    font-size: 14px;
    font-weight: 700;
    color: #869AB8;
    margin: 0px 12px 4px 12px;
    height: 44px;
    justify-content: left;
    align-items: center;
    column-gap: 12px;
}
.p-sidebar__list--item:hover{
    background-color: #27395E;
    color: #fff;
    border-radius: 4px;
    cursor: pointer;
}
.p-sidebar__shorter{
    cursor: pointer;
    border-top: 1px solid #869AB8;
    float: bottom;
}
.p-sidebar__shorter--item--icon{
    margin-left: 18px;
}
.p-sidebar__list--item--icon{
    margin-left: 12px;
}
</style>