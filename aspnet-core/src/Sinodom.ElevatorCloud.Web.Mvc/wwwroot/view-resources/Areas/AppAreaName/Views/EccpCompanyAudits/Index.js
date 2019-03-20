(function () {
    $(function () {

        var _$eccpCompanyAuditsTable = $('#EccpCompanyAuditsTable');
        var _eccpCompanyAuditsService = abp.services.app.eccpCompanyAudits;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            edit: abp.auth.hasPermission('Pages.Administration.EccpCompanyAudits.Edit')
        };

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpCompanyAudits/EditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpCompanyAudits/_EditModal.js',
            modalClass: 'EditEccpCompanyAuditModal'
        });

        var _viewEccpCompanyAuditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpCompanyAudits/ViewEccpCompanyAuditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpCompanyAudits/_ViewEccpCompanyAuditModal.js',
            modalClass: 'ViewEccpCompanyAuditModal'
        });

        var dataTable = _$eccpCompanyAuditsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpCompanyAuditsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpCompanyAuditsTableFilter').val()
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
                                text: app.localize('ViewLog'),
                                action: function (data) {
                                    _viewEccpCompanyAuditModal.open({ tenantId: data.record.id });
                                }
                            },
                            {
                                text: app.localize('Audit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _editModal.open({ id: data.record.id });
                                }
                            }
                        ]
                    }
                },
                {
                    targets: 1,
                    data: "eccpCompanyInfo.name"
                },
                {
                    targets: 2,
                    data: "editionTypeName"
                },
                {
                    targets: 3,
                    data: "eccpCompanyInfo.addresse"
                },
                {
                    targets: 4,
                    data: "eccpCompanyInfoExtension.legalPerson"
                },
                {
                    targets: 5,
                    data: "eccpCompanyInfoExtension.mobile"
                },
                {
                    targets: 6,
                    data: "eccpCompanyInfoExtension.isMember",
                    render: function (isActive) {
                        if (isActive) {
                            return '<span class="label label-success">' + app.localize('Yes') + '</span>';
                        } else {
                            return '<span class="label label-default">' + app.localize('No') + '</span>';
                        }
                    }
                },
                {
                    targets: 7,
                    data: "checkStateName"
                }
            ]
        });


        function getECCPBaseRegisterCompanies() {
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

        $('#CreateNewECCPBaseRegisterCompanyButton').click(function () {
            _createOrEditModal.open();
        });

        abp.event.on('app.editEccpCompanyAuditModalSaved', function () {
            getECCPBaseRegisterCompanies();
        });

        $('#GetEccpCompanyAuditsButton').click(function (e) {
            e.preventDefault();
            getECCPBaseRegisterCompanies();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseRegisterCompanies();
            }
        });

    });
})();