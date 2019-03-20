(function () {
    $(function () {

        var _$eccpMaintenanceTempWorkOrdersTable = $('#EccpMaintenanceTempWorkOrdersTable');
        var _eccpMaintenanceTempWorkOrdersService = abp.services.app.eccpMaintenanceTempWorkOrders;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTempWorkOrders.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTempWorkOrders.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTempWorkOrders.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrders/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTempWorkOrders/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceTempWorkOrderModal'
        });

        var _viewEccpMaintenanceTempWorkOrderModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrders/VieweccpMaintenanceTempWorkOrderModal',
            modalClass: 'ViewEccpMaintenanceTempWorkOrderModal'
        });

        var dataTable = _$eccpMaintenanceTempWorkOrdersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTempWorkOrdersService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceTempWorkOrdersTableFilter').val(),
                        minCheckStateFilter: $('#MinCheckStateFilterId').val(),
                        maxCheckStateFilter: $('#MaxCheckStateFilterId').val(),
                        minCompletionTimeFilter: $('#MinCompletionTimeFilterId').val(),
                        maxCompletionTimeFilter: $('#MaxCompletionTimeFilterId').val(),
                        eCCPBaseMaintenanceCompanyNameFilter: $('#ECCPBaseMaintenanceCompanyNameFilterId').val(),
                        userNameFilter: $('#UserNameFilterId').val()
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
                                    _viewEccpMaintenanceTempWorkOrderModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.eccpMaintenanceTempWorkOrder.id });
                                }
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteEccpMaintenanceTempWorkOrder(data.record.eccpMaintenanceTempWorkOrder);
                                }
                            }]
                    }
                },
                {
                    targets: 1,
                    data: "eccpMaintenanceTempWorkOrder.title"
                },
                {
                    targets: 2,
                    data: "tempOrderTypeName"
                },
                {
                    targets: 3,
                    data: "eccpMaintenanceTempWorkOrder.completionTime",
                    render: function (completionTime) {
                        if (completionTime) {
                            return moment(completionTime).format('L');
                        }
                        return "";
                    }

                },
                {
                    targets: 4,
                    data: "eccpBaseElevatorName"
                },
                {
                    targets: 5,
                    data: "eccpBaseMaintenanceCompanyName"
                },
                {
                    targets: 6,
                    data: "userName"
                }
            ]
        });


        function getEccpMaintenanceTempWorkOrders() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceTempWorkOrder(eccpMaintenanceTempWorkOrder) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceTempWorkOrdersService.delete({
                            id: eccpMaintenanceTempWorkOrder.id
                        }).done(function () {
                            getEccpMaintenanceTempWorkOrders(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
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

        $('#CreateNewEccpMaintenanceTempWorkOrderButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _eccpMaintenanceTempWorkOrdersService
                .getEccpMaintenanceTempWorkOrdersToExcel({
                    filter: $('#EccpMaintenanceTempWorkOrdersTableFilter').val(),
                    minCheckStateFilter: $('#MinCheckStateFilterId').val(),
                    maxCheckStateFilter: $('#MaxCheckStateFilterId').val(),
                    minCompletionTimeFilter: $('#MinCompletionTimeFilterId').val(),
                    maxCompletionTimeFilter: $('#MaxCompletionTimeFilterId').val(),
                    eCCPBaseMaintenanceCompanyNameFilter: $('#ECCPBaseMaintenanceCompanyNameFilterId').val(),
                    userNameFilter: $('#UserNameFilterId').val()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpMaintenanceTempWorkOrderModalSaved', function () {
            getEccpMaintenanceTempWorkOrders();
        });

        $('#GetEccpMaintenanceTempWorkOrdersButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceTempWorkOrders();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceTempWorkOrders();
            }
        });

    });
})();