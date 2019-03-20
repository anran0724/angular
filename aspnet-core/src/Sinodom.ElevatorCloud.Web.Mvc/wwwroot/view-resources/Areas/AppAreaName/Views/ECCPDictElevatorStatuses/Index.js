(function () {
    $(function () {

        var _$eCCPDictElevatorStatusesTable = $('#ECCPDictElevatorStatusesTable');
        var _eCCPDictElevatorStatusesService = abp.services.app.eCCPDictElevatorStatuses;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictElevatorStatuses.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictElevatorStatuses.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictElevatorStatuses.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPDictElevatorStatuses/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPDictElevatorStatuses/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPDictElevatorStatusModal'
        });

        var _viewECCPDictElevatorStatusModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPDictElevatorStatuses/VieweCCPDictElevatorStatusModal',
            modalClass: 'ViewECCPDictElevatorStatusModal'
        });

        var dataTable = _$eCCPDictElevatorStatusesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPDictElevatorStatusesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPDictElevatorStatusesTableFilter').val()
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
                                    _viewECCPDictElevatorStatusModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.eccpDictElevatorStatus.id });
                                }
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteECCPDictElevatorStatus(data.record.eccpDictElevatorStatus);
                                }
                            }]
                    }
                },
                {
                    targets: 1,
                    data: "eccpDictElevatorStatus.name"
                },
                {
                    targets: 2,
                    data: "eccpDictElevatorStatus.colorStyle"
                }
            ]
        });


        function getECCPDictElevatorStatuses() {
            dataTable.ajax.reload();
        }

        function deleteECCPDictElevatorStatus(eccpDictElevatorStatus) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPDictElevatorStatusesService.delete({
                            id: eccpDictElevatorStatus.id
                        }).done(function () {
                            getECCPDictElevatorStatuses(true);
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

        $('#CreateNewECCPDictElevatorStatusButton').click(function () {
            _createOrEditModal.open();
        });



        abp.event.on('app.createOrEditECCPDictElevatorStatusModalSaved', function () {
            getECCPDictElevatorStatuses();
        });

        $('#GetECCPDictElevatorStatusesButton').click(function (e) {
            e.preventDefault();
            getECCPDictElevatorStatuses();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPDictElevatorStatuses();
            }
        });

    });
})();