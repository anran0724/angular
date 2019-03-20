(function () {
    $(function () {

        var _$eccpMaintenanceWorkOrdersTable = $('#EccpMaintenanceWorkOrdersTable');
        var _eccpMaintenanceWorkOrdersService = abp.services.app.eccpMaintenanceWorkOrders;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrders.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrders.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrders.Delete'),
            'closeWorkOrder': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrders.CloseWorkOrder'),
            'viewWorkOrderEvaluations': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrders.ViewEvaluations')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrders/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkOrders/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceWorkOrderModal'
        });

        var _viewEccpMaintenanceWorkOrderModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrders/VieweccpMaintenanceWorkOrderModal',
            modalWidth: "1000px",
            modalClass: 'ViewEccpMaintenanceWorkOrderModal'
        });

        //维保评价查看
        var _viewEccpMaintenanceWorkOrderEvaluationsTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrders/EccpMaintenanceWorkOrderEvaluationsTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkOrders/_EccpMaintenanceWorkOrderEvaluationsTableModal.js',
            modalWidth: "1200px",
            modalClass: 'ViewEccpMaintenanceWorkOrderEvaluationsTableModal'
        });

        var dataTable = _$eccpMaintenanceWorkOrdersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkOrdersService.getAll,
                inputFilter: function () {
                    return {
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
                        eccpDictMaintenanceStatusNameFilter: $('#EccpDictMaintenanceStatusNameFilterId').val(),
                        eccpElevatorNameFilter: $('#EccpElevatorNameFilterId').val(),
                        eccpMaintenanceUserNameFilter: $('#EccpMaintenanceUserNameFilterId').val(),
                        eccpPropertyUserNameFilter: $('#EccpPropertyUserNameFilterId').val(),
                        isClosedFilter: $('#IsClosedFilterId').val()
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
                            //{
                            //    text: app.localize('Edit'),
                            //    visible: function (data) {
                            //        if (data.record.eccpMaintenanceWorkOrder.isClosed) {
                            //            return false;
                            //        } else {
                            //            return _permissions.edit;
                            //        }
                            //    },
                            //    action: function (data) {
                            //        _createOrEditModal.open({ id: data.record.eccpMaintenanceWorkOrder.id });
                            //    }
                            //},
                            //{
                            //    text: app.localize('Delete'),
                            //    visible: function (data) {
                            //        if (data.record.eccpMaintenanceWorkOrder.isClosed) {
                            //            return false;
                            //        } else {
                            //            return _permissions.delete;
                            //        }
                            //    },
                            //    action: function (data) {
                            //        deleteEccpMaintenanceWorkOrder(data.record.eccpMaintenanceWorkOrder);
                            //    }
                            //},
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
                    data: "eccpMaintenanceWorkOrder.complateDate",
                    render: function (complateDate) {
                        if (complateDate) {
                            return moment(complateDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 8,
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
                //{
                //    targets: 8,
                //    data: "eccpPropertyUserNameList",
                //    render: function (eccpPropertyUserNameList) {
                //        if (eccpPropertyUserNameList) {
                //            var eccpPropertyUserName = "";
                //            for (var i = 0; i < eccpPropertyUserNameList.length; i++) {
                //                eccpPropertyUserName += eccpPropertyUserNameList[i] + ",";
                //            }
                //            return eccpPropertyUserName.substring(0, eccpPropertyUserName.length - 1);
                //        }
                //        return "";
                //    }
                //},
                {
                    targets: 9,
                    data: "eccpMaintenanceWorkOrder.isClosed",
                    render: function (isClosed) {
                        return isClosed ? app.localize('Yes') : app.localize('No');
                    }
                },
                //{
                //    targets: 9,
                //    data: "eccpMaintenanceWorkOrder.longitude"
                //},
                //{
                //    targets: 10,
                //    data: "eccpMaintenanceWorkOrder.latitude"
                //},
                //{
                //    targets: 11,
                //    data: "eccpMaintenanceWorkOrder.isPassed",
                //    render: function (isPassed) {
                //        if (isPassed) {
                //            return '<div class="text-center"><i class="fa fa-check-circle m--font-success" title="True"></i></div>';
                //        }
                //        return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
                //    }

                //}
            ]
        });


        function getEccpMaintenanceWorkOrders() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceWorkOrder(eccpMaintenanceWorkOrder) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceWorkOrdersService.delete({
                            id: eccpMaintenanceWorkOrder.id
                        }).done(function () {
                            getEccpMaintenanceWorkOrders(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        function closeEccpMaintenanceWorkOrder(eccpMaintenanceWorkOrder) {
            if (!eccpMaintenanceWorkOrder.isClosed) {
                abp.message.confirm(
                    '',
                    function (isConfirmed) {
                        if (isConfirmed) {
                            _eccpMaintenanceWorkOrdersService.closeWorkOrder({
                                id: eccpMaintenanceWorkOrder.id
                            }).done(function () {
                                getEccpMaintenanceWorkOrders(true);
                                abp.notify.success(app.localize('SuccessfullyCloseWorkOrder'));
                            });
                        }
                    }
                );
            } else {
                abp.notify.warn(app.localize('WorkOrderClose'));
            }
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

        $('#CreateNewEccpMaintenanceWorkOrderButton').click(function () {
            _createOrEditModal.open();
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

        abp.event.on('app.createOrEditEccpMaintenanceWorkOrderModalSaved', function () {
            getEccpMaintenanceWorkOrders();
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