(function () {
    $(function () {

        var _$eccpMaintenancePlansTable = $('#EccpMaintenancePlansTable');
        var _eccpMaintenancePlansService = abp.services.app.eccpMaintenancePlans;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenancePlans.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenancePlans.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenancePlans.Delete'),
            'closePlan': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenancePlans.ClosePlan'),
            'maintenanceWorkOrders': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenancePlans.MaintenanceWorkOrders')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenancePlans/_CreateOrEditModal.js',
            modalWidth: "900px",
            modalClass: 'CreateOrEditEccpMaintenancePlanModal'
        });

        var _guideCreateModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/GuideCreateModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenancePlans/_GuideCreateModal.js',
            modalWidth: "1050px",
            modalClass: 'GuideCreateEccpMaintenancePlanModal'
        });

        var _viewEccpMaintenancePlanModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/VieweccpMaintenancePlanModal',
            scriptUrl: abp.appPath + 'view-resources/Views/Shared/Views/_ViewStyle.js',
            modalClass: 'ViewStyleModal'
        });

        var _viewEccpMaintenanceWorkOrderPlans = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/EccpMaintenanceWorkOrderPlansTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenancePlans/_EccpMaintenanceWorkOrderPlansTableModal.js',
            modalWidth: "1200px",
            modalClass: 'ViewEccpMaintenanceWorkOrderPlansTableModal'
        });


        var dataTable = _$eccpMaintenancePlansTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenancePlansService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenancePlansTableFilter').val(),
                        minPollingPeriodFilter: $('#MinPollingPeriodFilterId').val(),
                        maxPollingPeriodFilter: $('#MaxPollingPeriodFilterId').val(),
                        minRemindHourFilter: $('#MinRemindHourFilterId').val(),
                        maxRemindHourFilter: $('#MaxRemindHourFilterId').val(),
                        minElevatorNumFilter: $('#MinElevatorNumFilterId').val(),
                        maxElevatorNumFilter: $('#MaxElevatorNumFilterId').val(),
                        isCloseFilter: $('#IsCloseFilterId').val()
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
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewEccpMaintenancePlanModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function (data) {
                                    if (data.record.eccpMaintenancePlan.isClose) {
                                        return false;
                                    } else {
                                        return _permissions.edit;
                                    }

                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.eccpMaintenancePlan.id });
                                }
                            },
                            //{
                            //    text: app.localize('Delete'),
                            //    visible: function (data) {
                            //        if (data.record.eccpMaintenancePlan.isClose) {
                            //            return false;
                            //        } else {
                            //            return _permissions.delete;
                            //        }
                            //    },
                            //    action: function (data) {
                            //        deleteEccpMaintenancePlan(data.record.eccpMaintenancePlan);
                            //    }
                            //},
                            {
                                text: app.localize('ClosePlan'),
                                visible: function (data) {
                                    if (data.record.eccpMaintenancePlan.isClose) {
                                        return false;
                                    } else {
                                        return _permissions.closePlan;
                                    }
                                },
                                action: function (data) {
                                    closePlanEccpMaintenancePlan(data.record.eccpMaintenancePlan);
                                }
                            },
                            {
                                text: app.localize('EccpMaintenanceWorkOrders'),
                                visible: function () {
                                    return _permissions.maintenanceWorkOrders;
                                },
                                action: function (data) {
                                    _viewEccpMaintenanceWorkOrderPlans.open({ planId: data.record.eccpMaintenancePlan.id });
                                }
                            }]
                    }
                },
                {
                    targets: 1,
                    data: "eccpMaintenancePlan.pollingPeriod"
                },
                {
                    targets: 2,
                    data: "eccpMaintenancePlan.remindHour"
                },
                {
                    targets: 3,
                    data: "eccpBaseElevatorNum"
                },
                {
                    targets: 4,
                    data: "eccpMaintenancePlan.isClose",
                    render: function (isClose) {
                        return isClose ? app.localize('Yes') : app.localize('No');
                    }
                }
            ]
        });


        function getEccpMaintenancePlans() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenancePlan(eccpMaintenancePlan) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenancePlansService.delete({
                            id: eccpMaintenancePlan.id
                        }).done(function () {
                            getEccpMaintenancePlans(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        function closePlanEccpMaintenancePlan(eccpMaintenancePlan) {
            if (!eccpMaintenancePlan.isClose) {
                abp.message.confirm(
                    '',
                    function (isConfirmed) {
                        if (isConfirmed) {
                            _eccpMaintenancePlansService.closePlan(
                                eccpMaintenancePlan.planGroupGuid
                            ).done(function () {
                                getEccpMaintenancePlans(true);
                                abp.notify.success(app.localize('SuccessfullyClosePlan'));
                            });
                        }
                    }
                );
            } else {
                abp.notify.warn(app.localize('PlanClosed'));
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

        $('#CreateNewEccpMaintenancePlanButton').click(function () {
            _createOrEditModal.open();
        });
        $('#GuideCreateNewEccpMaintenancePlanButton').click(function () {
            _guideCreateModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _eccpMaintenancePlansService
                .getEccpMaintenancePlansToExcel({
                    filter: $('#EccpMaintenancePlansTableFilter').val(),
                    minPollingPeriodFilter: $('#MinPollingPeriodFilterId').val(),
                    maxPollingPeriodFilter: $('#MaxPollingPeriodFilterId').val(),
                    minRemindHourFilter: $('#MinRemindHourFilterId').val(),
                    maxRemindHourFilter: $('#MaxRemindHourFilterId').val(),
                    eccpBaseElevatorNameFilter: $('#EccpBaseElevatorNameFilterId').val()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpMaintenancePlanModalSaved', function () {
            getEccpMaintenancePlans();
        });

        $('#GetEccpMaintenancePlansButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenancePlans();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenancePlans();
            }
        });

    });
})();