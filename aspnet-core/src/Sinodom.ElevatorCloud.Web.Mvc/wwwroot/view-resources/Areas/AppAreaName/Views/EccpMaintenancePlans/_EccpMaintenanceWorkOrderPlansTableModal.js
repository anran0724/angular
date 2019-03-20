(function () {

    app.modals.ViewEccpMaintenanceWorkOrderPlansTableModal = function () {
        var _args;
        var _eccpMaintenancePlansService = abp.services.app.eccpMaintenancePlans;
        var _$eccpMaintenanceWorkOrdersTable = $('#EccpMaintenanceWorkOrdersTable');

        this.init = function (modalManager, args) {
            _args = args;
        };

        var dataTable = _$eccpMaintenanceWorkOrdersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenancePlansService.getAllWorkOrdersByPlanId,
                inputFilter: function () {
                    return {
                        eccpDictMaintenanceStatusNameFilter: $('#EccpDictMaintenanceStatusNameFilterId').val(),
                        eccpElevatorNameFilter: $('#EccpElevatorNameFilterId').val(),
                        planIdFilter: _args.planId
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    targets: 1,
                    data: "eccpDictMaintenanceTypeName"
                }, {
                    targets: 2,
                    data: "eccpElevatorName"
                },
                {
                    targets: 3,
                    data: "eccpMaintenanceWorkOrder.planCheckDate",
                    render: function (planCheckDate) {
                        if (planCheckDate) {
                            return moment(planCheckDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 4,
                    data: "eccpMaintenancePlanPollingPeriod"
                },
                {
                    targets: 5,
                    data: "eccpDictMaintenanceStatusName"
                },
                {
                    targets: 6,
                    data: "eccpMaintenanceUserNameList",
                    render: function (eccpMaintenanceUserNameList) {
                        if (eccpMaintenanceUserNameList) {
                            var eccpMaintenanceUserName = "";
                            for (var i = 0; i < eccpMaintenanceUserNameList.length; i++) {
                                eccpMaintenanceUserName += eccpMaintenanceUserNameList[i] + ",";
                            }
                            return eccpMaintenanceUserName.substring(0, eccpMaintenanceUserName.length - 1);
                        }
                        return "";
                    }
                },
                {
                    targets: 7,
                    data: "eccpMaintenanceWorkOrder.isClosed",
                    render: function (isClosed) {
                        return isClosed ? app.localize('Yes') : app.localize('No');
                    }
                }
            ]
        });

        function getEccpMaintenanceWorkOrders() {
            dataTable.ajax.reload();
        }

        $('#GetEccpMaintenanceWorkOrdersButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceWorkOrders();
        });
    }

})();