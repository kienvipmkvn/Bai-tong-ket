﻿/**
 * Base class dùng chung
 */
class BaseJS {
    constructor(_idPropertyName, isHaveDropdown = true) {
        //tên trường khoá chính trong db
        this.idPropertyName = _idPropertyName;
        this.pageIndex = 0;
        this.pageSize = 17;
        this.totalPage = 0;
        this.host = "";
        this.setApiRouter();
        this.initEvents();
        this.loadData(this.pageIndex, this.pageSize);
        if (isHaveDropdown) this.loadGroup();
        this.entityState = MISAEnum.EntityState.AddNew;
        this.currentEntity = null;
        this.currentSearchKey = null;
        this.currentEntityGroup = null;
        this.isUpdating = false;
    }

    /**
     * set đường dẫn api
     * createdBy: dtkien1 (16/12/2020)
     */
    setApiRouter() {
    }

    /**
     * Đăng ký các sự kiện
     * createdBy: dtkien1 (16/12/2020)
     */
    initEvents() {
        var me = this;
        // Sự kiện click khi nhấn thêm mới:
        $('#btnAdd').click(me.btnAddOnClick.bind(me));

        // Sự kiện click khi nhấn lưu:
        $('#btnSave').click(me.btnSaveOnClick.bind(me));

        // Sự kiện click khi nhấn xoá:
        $('#btnDelete').click(me.btnDeleteOnClick.bind(me));

        // Load lại dữ liệu khi nhấn button nạp:
        $('#btnRefresh').click(function () {
            me.loadData(me.pageIndex, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
        })

        //sự kiện click first page
        $(".m-btn-firstpage").click(function () {
            if (me.pageIndex != 0) {
                me.pageIndex = 0;
                me.loadData(0, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
            }
        })

        //sự kiện click last page
        $(".m-btn-lastpage").click(function () {
            if (me.pageIndex != me.totalPage - 1) {
                me.pageIndex = me.totalPage - 1;
                me.loadData(me.totalPage - 1, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
            }
        })

        //sự kiện click previous page
        $(".m-btn-prev-page").click(function () {
            if (me.pageIndex - 1 >= 0) {
                me.loadData(--me.pageIndex, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
            }
        })

        //sự kiện click next page
        $(".m-btn-next-page").click(function () {
            if (me.pageIndex + 1 < me.totalPage) {
                me.loadData(++me.pageIndex, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
            }
        })

        //sự kiện click page button
        $("#pageButton1").click(function () {
            if ($("#pageButton1").hasClass("btn-pagenumber-selected")) return;
            if (me.totalPage < 1) return;
            me.loadData(parseInt($("#pageButton1").text()) - 1, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
        })
        $("#pageButton2").click(function () {
            if ($("#pageButton2").hasClass("btn-pagenumber-selected")) return;
            if (me.totalPage < 2) return;
            me.loadData(parseInt($("#pageButton2").text()) - 1, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
        })
        $("#pageButton3").click(function () {
            if ($("#pageButton3").hasClass("btn-pagenumber-selected")) return;
            if (me.totalPage < 3) return;
            me.loadData(parseInt($("#pageButton3").text()) - 1, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
        })
        $("#pageButton4").click(function () {
            if ($("#pageButton4").hasClass("btn-pagenumber-selected")) return;
            if (me.totalPage < 4) return;
            me.loadData(parseInt($("#pageButton4").text()) - 1, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
        })

        // Ẩn form chi tiết khi nhấn hủy:
        $('#btnCancel').click(function () {
            // Hiển thị dialog thông tin chi tiết:
            dialogDetail.dialog('close');
        })

        //validate input
        $('input[required]').blur(function (e) {
            if (e.currentTarget.value == '') {
                $(this).addClass('border-red');
            }
        }).focus(function () {
            $(this).removeClass('border-red');
        })

        //Hiển thị thông tin chi tiết khi nhấn đúp chuột chọn 1 bản ghi trên danh sách dữ liệu:
        $('#tableBody').on('dblclick', 'tr', function (e) {
            me.rowDbClick(e);
        })

        //sự kiện thay đổi nội dung tìm kiếm
        $("#txtSearch").change(() => {
            me.currentSearchKey = $("#txtSearch").val();
            me.loadData(0, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
        })
    }

    /**
     * Hiển thị đúng group của entity được chọn
     * @param {any} id
     * createdBy: dtkien1 (24/12/2020)
     */
    getEntityGroup(id) {
        var divs = $("#dialog-detail .select-items div");

        if (id == null) {
            for (var div of divs) {
                div.classList.remove("same-as-selected");
            }

            divs[0].classList.add("same-as-selected");
            $("#dialog-detail .select-selected").text(divs[0].innerText);
            return;
        }

        //biến handle trường hợp id khác null và không có groupId = id;
        var isGroupValid = false;

        for (var div of divs) {
            div.classList.remove("same-as-selected");
            if (div.getAttribute("value") == id) {
                div.classList.add("same-as-selected");
                $("#dialog-detail .select-selected").text(div.innerText);
                isGroupValid = true;
            }
        }

        if (!isGroupValid) {
            divs[0].classList.add("same-as-selected");
            $("#dialog-detail .select-selected").text(divs[0].innerText);
        }
    }

    /**
     * load dữ liệu vào grid
     * CreatedBy: dtkien1 (10/12/2020)
     * @param {any} pageIndex: số thứ tự trang
     * @param {any} pageSize: số bản ghi trong 1 trang
     * @param {any} searchKey: từ khoá tìm kiếm theo tên khách hàng
     * @param {any} entityGroupId: id nhóm khách hàng
     */
    loadData(pageIndex, pageSize, searchKey = null, entityGroupId = null) {
        var me = this;
        $('#tableBody').empty();

        var url = me.host + me.entityRouter + "/" + pageSize + "/" + pageIndex;

        if (searchKey != null) {
            url += `?searchKey=${me.currentSearchKey}`;
            if (entityGroupId != null && entityGroupId != 0) url += `&entityGroupId=${me.currentEntityGroup}`;
        }
        else if (entityGroupId != null && entityGroupId != 0) url += `?entityGroupId=${me.currentEntityGroup}`;

        //hiện loading
        $('.loading-modal').removeClass('loaded');
        try {
            $.ajax({
                url: url,
                method: "GET",
                async: true,
            }).done(function (res) {
                if (res.MISACode != MISAEnum.MISACode.Success) throw res.Message;
                me.entities = res.Data.Entities;
                me.totalPage = res.Data.TotalPage;
                me.pageIndex = pageIndex;
                me.pageSize = pageSize;

                var listProp = [];


                //Bind dữ liệu vào grid
                var appendElement = "";
                var i = pageIndex * pageSize + 1;
                for (var entity of me.entities) {
                    if (me.isUpdating && me.currentEntity[me.idPropertyName] == entity[me.idPropertyName]) {
                        appendElement += `<tr class="selected-row">`;
                        me.isUpdating = false;
                    }
                    else appendElement += `<tr>`;
                    appendElement += `<td class= "max-width-50px text-align-center" > ${i}</td >`;

                    $("table thead th[fieldName]").each((id, el) => {
                        listProp.push(el.getAttribute("fieldName"));
                        var prop = el.getAttribute("fieldName");
                        var isGender = el.getAttribute("gender");
                        var isDate = el.getAttribute("date");
                        var isSalary = el.getAttribute("salary");

                        if (isGender) {
                            appendElement += `<td class="${el.classList.value}" title="${getGender(entity[prop]) ?? "Không có dữ liệu!"}">${getGender(entity[prop]) ?? ""}</td>`;
                        } else if (isDate) {
                            appendElement += `<td class="${el.classList.value}" title="${formatDate(entity[prop]) ?? "Không có dữ liệu!"}">${formatDate(entity[prop]) ?? ""}</td >`;
                        } else if (isSalary) {
                            appendElement += `<td class="${el.classList.value}" title="${formatMoney(entity[prop]) ?? "Không có dữ liệu!"}">${formatMoney(entity[prop]) ?? ""}</td >`;
                        }
                        else appendElement += `<td class="${el.classList.value}" title="${entity[prop] ?? "Không có dữ liệu!"}">${entity[prop] ?? ""}</td>`;
                    })

                    appendElement += `</tr>`;
                    i++;
                }
                $("#tableBody").append(appendElement)

                //Set pagination data
                var startIndex = pageIndex * pageSize + 1;
                var endIndex = startIndex + pageSize - 1;
                var totalItem = res.Data.TotalItem;
                if (totalItem == 0) startIndex = 0;
                if (endIndex > totalItem) endIndex = totalItem;
                $("#displayRange").text(startIndex + "-" + endIndex + "/" + totalItem);
                $("#pageSize").text(pageSize);

                //Set paging button cho trang đang được hiển thị
                setPageIndex(me);

                //ẩn loading
                $('.loading-modal').addClass('loaded');
            }).fail(function (res) {
                //ẩn loading
                $('.loading-modal').addClass('loaded');
                console.log(res)
            })
        } catch (e) {
            $('.loading-modal').addClass('loaded');
            console.log(e);
        }
    }

    //load danh sách nhóm khách hàng
    loadGroup() {
        var me = this;
        try {
            $.ajax({
                url: me.host + me.entityGroupRouter,
                method: "GET",
                async: true,
            }).done(function (res) {
                if (res.MISACode != MISAEnum.MISACode.Success) throw res.Message;
                me.entityGroups = res.Data;

                var propId;
                var propName;
                $("#groupSelectFilterBar").each((id, el) => {
                    propId = el.getAttribute("fieldId");
                    propName = el.getAttribute("fieldName");
                });

                for (var group of me.entityGroups) {
                    $("#groupSelectFilterBar").append(
                        `<option value="${group[propId]}">${group[propName]}</option>`
                    );
                    $("#groupSelectDialog").append(
                        `<option value="${group[propId]}">${group[propName]}</option>`
                    )
                }
                dropdownFunction();

                //bind event filter nhóm khách hàng
                $("#customGroupSelect .select-items div").click(function (e) {
                    me.currentEntityGroup = e.target.getAttribute("value");
                    me.loadData(0, me.pageSize, me.currentSearchKey, me.currentEntityGroup)
                })
            })
                .fail(function (res) {
                    console.log(res);
                    dropdownFunction();
                })
        } catch (e) {
            dropdownFunction();
            console.log(e);
        }
    }

    /**
     * sự kiện dbclick 1 bản ghi
     * CreatedBy: dtkien1 (10/12/2020)
     */
    rowDbClick(e) {
        var me = this;
        me.method = "PUT"
        dialogDetail.dialog('open');
        $("#btnDelete").removeClass("display-none");

        var index = parseInt(e.currentTarget.cells[0].innerText);
        index = index - me.pageIndex * me.pageSize;
        var selectedEntity = me.entities[index - 1];
        me.currentEntity = selectedEntity;
        me.entityState = MISAEnum.EntityState.Update;

        $("#dialog-detail input[fieldName], #dialog-detail select[fieldName]").each((id, el) => {
            var prop = el.getAttribute("fieldName");
            //TH input
            if (el.tagName == "INPUT") {
                //trường hợp date
                if (el.getAttribute("type") == "date") $('#dt' + prop).val(formatDateToInput(selectedEntity[prop]));
                //trường hợp radio
                else if (el.getAttribute("type") == "radio") {
                    if (selectedEntity[prop] == el.getAttribute("value")) {
                        el.checked = true;
                    }
                    else {
                        el.checked = false;
                    }
                }
                //trường hợp khác
                else {
                    $('#txt' + prop).val(selectedEntity[prop]);
                }
            }
            //TH dropdown
            else {
                me.getEntityGroup(selectedEntity[prop]);
            }
        })
    }

    /**
     * sự kiện click button thêm mới
     * CreatedBy: dtkien1 (10/12/2020)
     */
    btnAddOnClick() {
        this.currentEntity = null;
        this.method = "POST"
        this.entityState = MISAEnum.EntityState.AddNew;
        dialogDetail.dialog('open');
        $("#btnDelete").addClass("display-none");
        $('input').val(null);
    }

    /**
     * sự kiện click button lưu
     * CreatedBy: dtkien1 (14/12/2020)
     */
    btnSaveOnClick() {
        var me = this;

        var inputs = $('#dialog-detail input[fieldName], #dialog-detail select[fieldName]');
        var entity = {};
        $.each(inputs, function (index, input) {
            var propertyName = $(this).attr('fieldName');
            var value = $(this).val();

            // Check với trường hợp input là radio, thì chỉ lấy value của input có attribute là checked:
            if ($(this).attr('type') == "radio") {
                if (this.checked) {
                    entity[propertyName] = value;
                }
            }

            else if (this.tagName == "SELECT") {
                var select = $(this).next().next();
                var selectedValue = $(".same-as-selected", select).attr("value");
                if (!!selectedValue && selectedValue != 0)
                    entity[propertyName] = selectedValue;
            }

            else {
                entity[propertyName] = value;
            }
        })

        //nếu input k hợp lệ thì return
        var validateMsg = me.checkInputData();
        if (validateMsg != "") {
            me.setResultDialogTitle(MISAText.ErrorOccured, validateMsg);
            return;
        }
        $('.loading-modal').removeClass('loaded');
        if (me.entityState == MISAEnum.EntityState.AddNew) me.insertData(entity)
        else if (me.entityState == MISAEnum.EntityState.Update) {
            this.isUpdating = true;
            me.updateData(entity);
        }
    }

    /**
     * sự kiện click button xoá
     * CreatedBy: dtkien1 (14/12/2020)
     */
    btnDeleteOnClick() {
        var me = this;
        me.method = "DELETE";
        var yes = confirm(MISAText.Message.DeleteConfirm);
        if (yes) me.deleteData();
    }

    /**
     * Cập nhập dữ liệu
     * @param {any} data
     * cretedBy: dtkien (14/12/2020)
     */
    insertData(data) {
        var me = this;
        try {
            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                url: me.host + me.entityRouter,
                method: me.method,
                async: true,
                data: JSON.stringify(data)
            }).done(res => {
                me.setResultDialogTitle(MISAText.Message.InsertSuccess);
                dialogDetail.dialog('close');
                me.loadData(0, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
            })
                .fail(res => {
                    console.log(res)
                    $('.loading-modal').addClass('loaded');
                    var msgs = "";
                    if (res.responseJSON.MISACode == 900) {
                        for (var msg of res.responseJSON.Data) msgs = msgs.concat(msg, "\n");
                    }
                    else if (res.responseJSON.MISACode == 500) {
                        msgs = msgs.concat(res.responseJSON.Data.cusMsg, "\n");
                    }
                    me.setResultDialogTitle(MISAText.ErrorOccured, msgs);
                })
        } catch (e) {
            $('.loading-modal').addClass('loaded');
            console.log(e);
            me.setResultDialogTitle(MISAText.ErrorOccured)
        }
    }

    /**
     * Cập nhập dữ liệu
     * @param {any} data
     * cretedBy: dtkien (14/12/2020)
     */
    updateData(data) {
        var me = this;
        try {
            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                url: me.host + me.entityRouter + "/" + me.currentEntity[me.idPropertyName],
                method: me.method,
                async: true,
                data: JSON.stringify(data)
            }).done(res => {
                me.setResultDialogTitle(MISAText.Message.UpdateSuccess);
                me.loadData(me.pageIndex, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
            })
                .fail(res => {
                    $('.loading-modal').addClass('loaded');
                    console.log(res)
                    var msgs = "";
                    if (res.responseJSON.MISACode == 900) {
                        for (var msg of res.responseJSON.Data) msgs = msgs.concat(msg, "\n");
                    }
                    else if (res.responseJSON.MISACode == 500) {
                        msgs = msgs.concat(res.responseJSON.Data.cusMsg, "\n");
                    }
                    me.setResultDialogTitle(MISAText.ErrorOccured, msgs);
                })
        } catch (e) {
            $('.loading-modal').addClass('loaded');
            console.log(e);
            me.setResultDialogTitle(MISAText.ErrorOccured)
        }
    }

    /**
     * Xoá dữ liệu
     * cretedBy: dtkien (14/12/2020)
     */
    deleteData() {
        var me = this;
        $('.loading-modal').removeClass('loaded');
        try {
            $.ajax({
                url: me.host + me.entityRouter + "/" + me.currentEntity[me.idPropertyName],
                method: me.method,
                async: true
            }).done(() => {
                me.setResultDialogTitle(MISAText.Message.DeleteSuccess);
                dialogDetail.dialog('close');
                me.loadData(me.pageIndex, me.pageSize, me.currentSearchKey, me.currentEntityGroup);
            })
                .fail(res => {
                    $('.loading-modal').addClass('loaded');
                    console.log(res)
                    var msgs = "";
                    if (res.responseJSON.MISACode == 900) {
                        for (var msg of res.responseJSON.Data) msgs = msgs.concat(msg, "\n");
                    }
                    else if (res.responseJSON.MISACode == 500) {
                        msgs = msgs.concat(res.responseJSON.Data.cusMsg, "\n");
                    }
                    me.setResultDialogTitle(MISAText.ErrorOccured, msgs);
                })
        } catch (e) {
            $('.loading-modal').addClass('loaded');
            console.log(e);
            me.setResultDialogTitle(MISAText.ErrorOccured)
        }
    }


    /**
     * Lấy giá trị trong custom dropdown
     * createdBy: dtkien(16/12/2020)
     */
    getCustomDropdownValue() {
        var value = $("#groupSelectDialog .same-as-selected").attr("value");
        if (value == 0) return null;
        else return value;
    }

    /**
     * Lấy giá trị giới tính trong radio button group
     * createdBy: dtkien(16/12/2020)
     */
    getGender() {
        if ($("#rdGender #rdMale")[0].checked) return MISAEnum.GenderId.Male;
        else if ($("#rdGender #rdFemale")[0].checked) return MISAEnum.GenderId.Female;
        else if ($("#rdGender #rdOther")[0].checked) return MISAEnum.GenderId.Other;
        else return MISAEnum.GenderId.Unknow;
    }

    /**
     * Check dữ liệu nhập vào
     * createdBy: dtkien(16/12/2020)
     */
    checkInputData() {
        //check required
        var errorList = "";
        var inputRequiredCheck = true;
        $("input[required]").each((id, inputElement) => {
            if (inputElement.value == "") {
                inputRequiredCheck = false;
            }
        })

        if (!inputRequiredCheck) errorList += MISAText.ErrorMessage.Required;

        //check số điện thoại
        var inputPhoneNumberCheck = true;
        $("input[phoneNumber]").each((id, inputElement) => {
            var regex = new RegExp(/^(\+)?([0-9]){8,11}$/);
            if (!regex.test(inputElement.value)) {
                inputPhoneNumberCheck = false;
            }
        })

        if (!inputPhoneNumberCheck) errorList += MISAText.ErrorMessage.PhoneNumber;

        return errorList;
    }

    /**
     * Hiển thị phân trang chính xác
     * createdBy: dtkien(16/12/2020)
     * */
    setResultDialogTitle(title, message = "") {
        $("#ui-id-2").text(title);
        $("#errorMsg").text(message)
        dialogError.dialog('open');
    }
}