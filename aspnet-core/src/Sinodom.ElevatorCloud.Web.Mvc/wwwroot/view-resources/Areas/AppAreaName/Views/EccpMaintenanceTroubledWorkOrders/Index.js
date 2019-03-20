(function () {
    $(function () {

        var _$eccpMaintenanceTroubledWorkOrdersTable = $('#EccpMaintenanceTroubledWorkOrdersTable');
        var _eccpMaintenanceTroubledWorkOrdersService = abp.services.app.eccpMaintenanceTroubledWorkOrders;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTroubledWorkOrders.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTroubledWorkOrders.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTroubledWorkOrders.Delete'),
            'audit': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTroubledWorkOrders.Audit')
        };

        var _auditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTroubledWorkOrders/AuditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTroubledWorkOrders/_AuditModal.js',
            modalClass: 'AuditEccpMaintenanceTroubledWorkOrderModal'
        });

        var _viewEccpMaintenanceTroubledWorkOrderModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTroubledWorkOrders/VieweccpMaintenanceTroubledWorkOrderModal',
            modalClass: 'ViewEccpMaintenanceTroubledWorkOrderModal'
        });

        var dataTable = _$eccpMaintenanceTroubledWorkOrdersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTroubledWorkOrdersService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceTroubledWorkOrdersTableFilter').val(),
                        isAuditFilter: $('#IsAuditId').val(),
                        eccpMaintenanceTroubledDescFilter: $('#EccpMaintenanceTroubledDescFilterId').val()
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
                                    _viewEccpMaintenanceTroubledWorkOrderModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Audit'),
                                visible: function (data) {
                                    if (data.record.eccpMaintenanceTroubledWorkOrder.isAudit == 0) {
                                        return _permissions.audit;
                                    } else {
                                        return false;
                                    }
                                },
                                action: function (data) {
                                    _auditModal.open({ id: data.record.eccpMaintenanceTroubledWorkOrder.id });
                                }
                            }]
                    }
                },
                {
                    targets: 1,
                    data: "eccpMaintenanceTroubledWorkOrder.workOrderStatusName"
                },
                {
                    targets: 2,
                    data: "eccpMaintenanceTroubledWorkOrder.troubledDesc"
                },
                {
                    targets: 3,
                    data: "eccpMaintenanceTroubledWorkOrder.isAudit",
                    render: function (isAudit) {
                        var result;
                        switch (isAudit) {
                            case 1:
                                result = app.localize('Through');
                                break;
                            case 2:
                                result = app.localize('NoThrough');
                                break;
                            default:
                                result = app.localize('NotAudit');
                                break;
                        }
                        return result;
                    }
                },
                {
                    targets: 4,
                    data: "eccpMaintenanceWorkOrderRemark"
                },
                {
                    targets: 5,
                    data: "eccpMaintenanceTroubledWorkOrder.remarks"
                }
            ]
        });


        function getEccpMaintenanceTroubledWorkOrders() {
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

        abp.event.on('app.createOrEditEccpMaintenanceTroubledWorkOrderModalSaved', function () {
            getEccpMaintenanceTroubledWorkOrders();
        });

        $('#GetEccpMaintenanceTroubledWorkOrdersButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceTroubledWorkOrders();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceTroubledWorkOrders();
            }
        });

    });
})();