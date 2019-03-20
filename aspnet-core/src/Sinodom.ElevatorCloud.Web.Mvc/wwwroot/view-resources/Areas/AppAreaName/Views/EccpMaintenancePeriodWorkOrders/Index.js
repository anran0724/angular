(function () {
    $(function () {

        var _$eccpMaintenanceWorkOrdersTable = $('#EccpMaintenanceWorkOrdersTable');
        var _eccpMaintenanceWorkOrdersService = abp.services.app.eccpMaintenanceWorkOrders;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'YYYY-MM-DD'
        });


        var _viewEccpMaintenanceWorkOrderModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrders/VieweccpMaintenanceWorkOrderModal',
            modalClass: 'ViewEccpMaintenanceWorkOrderModal'
        });

        var dataTable = _$eccpMaintenanceWorkOrdersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkOrdersService.getPeriodAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceWorkOrdersTableFilter').val(),
                        isPassedFilter: $('#IsPassedFilterId').val(),
                        eccpMaintenancePlanPollingPeriodFilter: $('#EccpMaintenancePlanPollingPeriodFilterId').val(),
                        eccpDictMaintenanceTypeNameFilter: $('#EccpDictMaintenanceTypeNameFilterId').val(),
                        eccpDictMaintenanceStatusNameFilter: $('#EccpDictMaintenanceStatusNameFilterId').val(),
                        eccpElevatorNameFilter: $('#EccpElevatorNameFilterId').val(),
                        eccpMaintenanceUserNameFilter: $('#EccpMaintenanceUserNameFilterId').val(),
                        eccpPropertyUserNameFilter: $('#EccpPropertyUserNameFilterId').val()
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
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewEccpMaintenanceWorkOrderModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('CloseWorkOrder'),
                                visible: function (data) {
                                    if (data.record.eccpMaintenanceWorkOrder.isClosed) {
                                        return false;
                                    } else {
                                        return _permissions.closeWorkOrder;
                                    }
                                },
                                action: function (data) {
                                    closeEccpMaintenanceWorkOrder(data.record.eccpMaintenanceWorkOrder);
                                }
                            },
                            {
                                text: app.localize('ViewWorkOrderEvaluations'),
                                visible: function (data) {
                                    return _permissions.viewWorkOrderEvaluations;
                                },
                                action: function (data) {
                                    _viewEccpMaintenanceWorkOrderEvaluationsTableModal.open({ workOrderId: data.record.eccpMaintenanceWorkOrder.id });
                                }
                            }
                        ]
                    }
                },
                {
                    targets: 2,
                    data: "eccpDictMaintenanceTypeName"
                },
                {
                    targets: 3,
                    data: "eccpElevatorName"
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
                    data: "eccpDictMaintenanceStatusName"
                },
                {
                    targets: 7,
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
                    targets: 8,
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



        $('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });



        $('#ExportToExcelButton').click(function () {
            _eccpMaintenanceWorkOrdersService
                .getEccpMaintenanceWorkOrdersToExcel({
                    filter: $('#EccpMaintenanceWorkOrdersTableFilter').val(),
                    isPassedFilter: $('#IsPassedFilterId').val(),
                    minLongitudeFilter: $('#MinLongitudeFilterId').val(),
                    maxLongitudeFilter: $('#MaxLongitudeFilterId').val(),
                    minLatitudeFilter: $('#MinLatitudeFilterId').val(),
                    maxLatitudeFilter: $('#MaxLatitudeFilterId').val(),
                    minPlanCheckDateFilter: $('#MinPlanCheckDateFilterId').val(),
                    maxPlanCheckDateFilter: $('#MaxPlanCheckDateFilterId').val(),
                    eccpMaintenancePlanPollingPeriodFilter: $('#EccpMaintenancePlanPollingPeriodFilterId').val(),
                    eccpDictMaintenanceTypeNameFilter: $('#EccpDictMaintenanceTypeNameFilterId').val(),
                    eccpDictMaintenanceStatusNameFilter: $('#EccpDictMaintenanceStatusNameFilterId').val()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });


        $('#GetEccpMaintenanceWorkOrdersButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceWorkOrders();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceWorkOrders();
            }
        });

    });
})();