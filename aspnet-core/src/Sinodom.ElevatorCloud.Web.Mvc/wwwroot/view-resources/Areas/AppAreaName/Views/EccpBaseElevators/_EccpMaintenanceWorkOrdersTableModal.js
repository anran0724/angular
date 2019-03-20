(function () {

    app.modals.ViewEccpMaintenanceWorkOrdersTableModal = function () {
        var _args;
        var _eccpMaintenanceWorkOrdersService = abp.services.app.eccpMaintenanceWorkOrders;
        var _$eccpMaintenanceWorkOrdersTable = $('#EccpMaintenanceWorkOrdersTable');

        this.init = function (modalManager, args) {
            _args = args;
        };

        var dataTable = _$eccpMaintenanceWorkOrdersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkOrdersService.getAllByElevatorId,
                inputFilter: function () {
                    return {
                        elevatorIdFilter: _args.elevatorId,
                        filter: $('#EccpMaintenanceWorkOrdersTableFilter').val(),
                    };
                }
            },
            columnDefs: [
                {
                    width: 120,
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>'
                    }
                },
                {
                    targets: 1,
                    data: "eccpMaintenanceWorkOrder.isPassed",
                    render: function (isPassed) {
                        if (isPassed) {
                            return '<div class="text-center"><i class="fa fa-check-circle m--font-success" title="True"></i></div>';
                        }
                        return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
                    }
                },
                {
                    targets: 2,
                    data: "eccpMaintenanceWorkOrder.longitude"
                },
                {
                    targets: 3,
                    data: "eccpMaintenanceWorkOrder.latitude"
                },
                {
                    targets: 4,
                    data: "eccpMaintenanceWorkOrder.planCheckDate",
                    render: function (planCheckDate) {
                        if (planCheckDate) {
                            return moment(planCheckDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 5,
                    data: "eccpMaintenancePlanPollingPeriod"
                },
                {
                    targets: 6,
                    data: "eccpDictMaintenanceTypeName"
                },
                {
                    targets: 7,
                    data: "eccpDictMaintenanceStatusName"
                },
                {
                    targets: 8,
                    data: "eccpElevatorName"
                },
                {
                    targets: 9,
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
                    targets: 10,
                    data: "eccpPropertyUserNameList",
                    render: function (eccpPropertyUserNameList) {
                        if (eccpPropertyUserNameList) {
                            var eccpPropertyUserName = "";
                            for (var i = 0; i < eccpPropertyUserNameList.length; i++) {
                                eccpPropertyUserName += eccpPropertyUserNameList[i] + ",";
                            }
                            return eccpPropertyUserName.substring(0, eccpPropertyUserName.length - 1);
                        }
                        return "";
                    }
                },
                {
                    targets: 11,
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