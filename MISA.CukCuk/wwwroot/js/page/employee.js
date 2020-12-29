$(document).ready(function () {
    new EmployeeJS();
    dialogDetail = $("#dialog-detail").dialog({
        autoOpen: false,
        fluid: true,
        minWidth: 700,
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
        zIndex: 999
    })
})

class EmployeeJS extends BaseJS {
    constructor() {
        super("EmployeeId", false);
        dropdownFunction();
    }

    setApiRouter() {
        this.entityRouter = "/api/v1/employee";
    }
}