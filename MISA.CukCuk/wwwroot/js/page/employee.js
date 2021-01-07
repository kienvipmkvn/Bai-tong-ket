$(document).ready(function () {
    new EmployeeJS();
    dialogDetail = $("#dialog-detail").dialog({
        autoOpen: false,
        fluid: true,
        minWidth: 600,
        width: 800,
        resizable: true,
        position: ({ my: "center", at: "center", of: window }),
        modal: true,
        zIndex: 100
    });

    dialogError = $("#dialog-error").dialog({
        autoOpen: false,
        fluid: true,
        minWidth: 350,
        resizable: true,
        position: ({ my: "center", at: "center", of: window }),
        modal: true,
        zIndex: 999,
        closeOnEscape: true,
        minHeight: 0
    });

    dialogConfirm = $("#dialog-confirm").dialog({
        autoOpen: false,
        fluid: true,
        minWidth: 350,
        resizable: true,
        position: ({ my: "center", at: "center", of: window }),
        modal: true,
        zIndex: 999,
        minHeight: 0
    });

    $("#btnCloseDialog").click(function () {
        dialogError.dialog('close');
    })
})

class EmployeeJS extends BaseJS {
    constructor() {
        super("Employee");
        dropdownFunction(-1);
        dropdownFunction(2);
        dropdownFunction(3);
    }

    setApiRouter() {
        this.entityRouter = "/api/v1/employee";
        this.dropdownRouters.push(...["/api/v1/department", "/api/v1/position"]);
    }

    checkOtherData() {
        //check identity number
        var me = this;
        var errMsg = "";
        var inputIdentityNumberCheck = true;
        $("input[identityNumber]").each((id, inputElement) => {
            //số cmt có 9 hoặc 12 chữ số
            var regex = /(^([0-9]){9}$)|(^([0-9]){12}$)/;
            if (inputElement.value != "" && !regex.test(inputElement.value)) {
                inputIdentityNumberCheck = false;
                me.createErrorMessage(inputElement, MISAText.ErrorMessage.IdentityNumber);
            }
        })

        if (!inputIdentityNumberCheck) errMsg += MISAText.ErrorMessage.IdentityNumber;

        return errMsg;
    }
}