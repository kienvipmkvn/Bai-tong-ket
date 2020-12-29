$(document).ready(function () {
    new CustomerJS();
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

class CustomerJS extends BaseJS{
    constructor() {
        super("CustomerId");
    }

    setApiRouter() {
        this.entityRouter = "/api/v1/customers";
        this.entityGroupRouter = "/api/v1/customerGroup"
    }
}