(function () {
    $(function () {

        var _$eccpMaintenanceWorkLogsTable = $('#EccpMaintenanceWorkLogsTable');
        var _eccpMaintenanceWorkLogsService = abp.services.app.eccpMaintenanceWorkLogs;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkLogs.Create'),
            edit: false
        };

        var _viewEccpMaintenanceWorkLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkLogs/VieweccpMaintenanceWorkLogModal',
            modalClass: 'ViewEccpMaintenanceWorkLogModal'
        });

        var dataTable = _$eccpMaintenanceWorkLogsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkLogsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceWorkLogsTableFilter').val(),
                        eccpMaintenanceUserNameFilter: $('#EccpMaintenanceUserNameFilterId').val(),
                        eccpMaintenanceCompanyNameFilter: $('#EccpMaintenanceCompanyNameFilterId').val(),
                        eccpPropertyCompanyNameFilter: $('#EccpPropertyCompanyNameFilterId').val(),
                        eccpElevatorNumFilter: $('#EccpElevatorNumFilterId').val()
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
                                    _viewEccpMaintenanceWorkLogModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {

                                }
                            }
                        ]
                    }
                },
                {
                    targets: 1,
                    data: "eccpMaintenanceWorkLog.maintenanceItemsName"
                },
                {
                    targets: 2,
                    data: "eccpMaintenanceWorkLog.remark"
                },
                {
                    targets: 3,
                    data: "eccpMaintenanceWorkLog.maintenanceWorkFlowName"
                }
            ]
        });


        function getEccpMaintenanceWorkLogs() {
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


        $('#GetEccpMaintenanceWorkLogsButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceWorkLogs();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceWorkLogs();
            }
        });

    });
})();