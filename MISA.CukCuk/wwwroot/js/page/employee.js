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
        zIndex: 999
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
})

class EmployeeJS extends BaseJS {
    constructor() {
        super("EmployeeId");
        dropdownFunction(-1);
        dropdownFunction(2);
    }

    setApiRouter() {
        this.entityRouter = "/api/v1/employee";
        this.dropdownRouters.push(...["/api/v1/department", "/api/v1/position"]);
    }
}