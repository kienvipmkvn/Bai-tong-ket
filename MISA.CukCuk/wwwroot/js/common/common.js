$(document).ready(function () {

})

/**
 * Enum
 * CreatedBy: dtkien1 (10/12/2020)
 * */
var MISAEnum = {
    //Giới tính
    GenderId: {
        //Nữ
        Female: 0,
        //Nam
        Male: 1,
        //Khác
        Other: 2,
        //Không xác định
        Unknow: 3
    },
    GenderName: {
        //Nữ
        Female: "Nữ",
        //Nam
        Male: "Nam",
        //Khác
        Other: "Khác",
        //Không xác định
        Unknow: "Không xác định"
    },
    EntityState:
    {
        //Thêm
        AddNew: 1,
        //Sửa
        Update: 2,
        //Xoá
        Delete: 3,
    },
    MISACode: {
        //Thành công
        Success: 200,
        NoContent: 204,
        NotFound: 404
    }
}

var MISAText = {
    ErrorOccured: "Có lỗi xảy ra!",
    ErrorMessage: {
        Required: "Bạn chưa điền đầy đủ thông tin; ",
        PhoneNumber: "Số điện thoại gồm 8-11 ký tự số; ",
        Email: "Email không hợp lệ; "
    },

    Message: {
        DeleteConfirm: "Bạn có chắc chắc muốn xoá?",
        UpdateSuccess: "Cập nhật thành công!",
        InsertSuccess: "Thêm mới thành công!",
        DeleteSuccess: "Xoá thành công!",
    },
}

/**
 * format date dưới dạng dd/MM/yyyy
 * @param {date} date
 * CreatedBy: dtkien1 (10/12/2020)
 */
function formatDate(date) {
    if (date == null) return null;
    var date = new Date(date);
    if (Number.isNaN(date.getTime())) {
        return "";
    } else {
        var day = date.getDate(),
            month = date.getMonth() + 1,
            year = date.getFullYear();
        day = day < 10 ? '0' + day : day;
        month = month < 10 ? '0' + month : month;

        return day + '/' + month + '/' + year;
    }
}

/**
 * format date dưới dạng yyyy-MM-dd để làm value cho input
 * @param {date} date
 * CreatedBy: dtkien1 (10/12/2020)
 */
function formatDateToInput(date) {
    if (date == null) return null;
    var date = new Date(date);
    if (Number.isNaN(date.getTime())) {
        return "";
    } else {
        var day = date.getDate(),
            month = date.getMonth() + 1,
            year = date.getFullYear();
        day = day < 10 ? '0' + day : day;
        month = month < 10 ? '0' + month : month;

        return year + '-' + month + '-' + day;
    }
}

/**
 * Hiển thị giới tính theo genderCode
 * @param {any} gender
 */
function getGender(gender) {
    if (gender == MISAEnum.GenderId.Female) {
        return MISAEnum.GenderName.Female
    }
    else if (gender == MISAEnum.GenderId.Male) {
        return MISAEnum.GenderName.Male
    }
    else if (gender == MISAEnum.GenderId.Other) {
        return MISAEnum.GenderName.Other
    }
    else {
        return null;
    }
}

/** -------------------------------------
 * Hàm định dạng hiển thị tiền tệ
 * @param {Number} money Số tiền
 * createdBy: dtKien (23/12/2020)
 */
function formatMoney(money) {
    if (money) {
        return money.toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
    }
    return "";
}


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}


function formatCurrency(input, blur) {
    // appends $ to value, validates decimal side
    // and puts cursor back in right position.

    // get input value
    var input_val = input.val();

    // don't validate empty input
    if (input_val === "") { return; }

    // original length
    var original_len = input_val.length;

    // initial caret position 
    var caret_pos = input.prop("selectionStart");

    // check for decimal
    if (input_val.indexOf(".") >= 0) {

        // get position of first decimal
        // this prevents multiple decimals from
        // being entered
        var decimal_pos = input_val.indexOf(".");

        // split number by decimal point
        var left_side = input_val.substring(0, decimal_pos);
        var right_side = input_val.substring(decimal_pos);

        // add commas to left side of number
        left_side = formatNumber(left_side);

        // validate right side
        right_side = formatNumber(right_side);

        // On blur make sure 2 numbers after decimal
        if (blur === "blur") {
            right_side += "00";
        }

        // Limit decimal to only 2 digits
        right_side = right_side.substring(0, 2);

        // join number by .
        input_val = "$" + left_side + "." + right_side;

    } else {
        // no decimal entered
        // add commas to number
        // remove all non-digits
        input_val = formatNumber(input_val);
        input_val = "$" + input_val;

        // final formatting
        if (blur === "blur") {
            input_val += ".00";
        }
    }

    // send updated string to input
    input.val(input_val);

    // put caret back in the right position
    var updated_len = input_val.length;
    caret_pos = updated_len - original_len + caret_pos;
    input[0].setSelectionRange(caret_pos, caret_pos);
}

/**
 * Hiển thị phân trang chính xác
 * @param {any} me class đang sử dụng
 * createdBy: dtkien(16/12/2020)
 * */
function setPageIndex(me) {
    //Trường hợp tổng số trang <=4
    if (me.totalPage <= 1) {
        $("#pageButton2").addClass("display-none");
        $("#pageButton3").addClass("display-none");
        $("#pageButton4").addClass("display-none");
    }
    else if (me.totalPage == 2) {
        $("#pageButton2").removeClass("display-none");
        $("#pageButton3").addClass("display-none");
        $("#pageButton4").addClass("display-none");
    }
    else if (me.totalPage == 3) {
        $("#pageButton2").removeClass("display-none");
        $("#pageButton3").removeClass("display-none");
        $("#pageButton4").addClass("display-none");
    }
    else {
        $("#pageButton2").removeClass("display-none");
        $("#pageButton3").removeClass("display-none");
        $("#pageButton4").removeClass("display-none");
    }

    //Trang đầu tiên
    if (me.pageIndex == 0) {
        $("#pageButton1").addClass("btn-pagenumber-selected");
        $("#pageButton2").removeClass("btn-pagenumber-selected");
        $("#pageButton3").removeClass("btn-pagenumber-selected");
        $("#pageButton4").removeClass("btn-pagenumber-selected");
        $("#pageButton1").text(me.pageIndex + 1);
        $("#pageButton2").text(me.pageIndex + 2);
        $("#pageButton3").text(me.pageIndex + 3);
        $("#pageButton4").text(me.pageIndex + 4);
    }
    //Trang thứ 2
    else if (me.pageIndex == 1) {
        $("#pageButton1").removeClass("btn-pagenumber-selected");
        $("#pageButton2").addClass("btn-pagenumber-selected");
        $("#pageButton3").removeClass("btn-pagenumber-selected");
        $("#pageButton4").removeClass("btn-pagenumber-selected");
        $("#pageButton1").text(me.pageIndex);
        $("#pageButton2").text(me.pageIndex + 1);
        $("#pageButton3").text(me.pageIndex + 2);
        $("#pageButton4").text(me.pageIndex + 3);
    }
    else if (me.pageIndex == 2 && me.totalPage == 3) {
        $("#pageButton1").removeClass("btn-pagenumber-selected");
        $("#pageButton2").removeClass("btn-pagenumber-selected");
        $("#pageButton3").addClass("btn-pagenumber-selected");
        $("#pageButton4").removeClass("btn-pagenumber-selected");
        $("#pageButton1").text(1);
        $("#pageButton2").text(2);
        $("#pageButton3").text(3);
        $("#pageButton4").text(4);
    }

    //Trang cuối cùng
    else if (me.pageIndex == me.totalPage - 1) {
        $("#pageButton1").removeClass("btn-pagenumber-selected");
        $("#pageButton2").removeClass("btn-pagenumber-selected");
        $("#pageButton3").removeClass("btn-pagenumber-selected");
        $("#pageButton4").addClass("btn-pagenumber-selected");
        $("#pageButton1").text(me.pageIndex - 2);
        $("#pageButton2").text(me.pageIndex - 1);
        $("#pageButton3").text(me.pageIndex);
        $("#pageButton4").text(me.pageIndex + 1);
    }

    //Trang gần cuối cùng
    else if (me.pageIndex == me.totalPage - 2) {
        $("#pageButton1").removeClass("btn-pagenumber-selected");
        $("#pageButton2").removeClass("btn-pagenumber-selected");
        $("#pageButton3").addClass("btn-pagenumber-selected");
        $("#pageButton4").removeClass("btn-pagenumber-selected");
        $("#pageButton1").text(me.pageIndex - 1);
        $("#pageButton2").text(me.pageIndex + 0);
        $("#pageButton3").text(me.pageIndex + 1);
        $("#pageButton4").text(me.pageIndex + 2);
    }

    else {
        $("#pageButton1").removeClass("btn-pagenumber-selected");
        $("#pageButton2").addClass("btn-pagenumber-selected");
        $("#pageButton3").removeClass("btn-pagenumber-selected");
        $("#pageButton4").removeClass("btn-pagenumber-selected");
        $("#pageButton1").text(me.pageIndex);
        $("#pageButton2").text(me.pageIndex + 1);
        $("#pageButton3").text(me.pageIndex + 2);
        $("#pageButton4").text(me.pageIndex + 3);
    }
}