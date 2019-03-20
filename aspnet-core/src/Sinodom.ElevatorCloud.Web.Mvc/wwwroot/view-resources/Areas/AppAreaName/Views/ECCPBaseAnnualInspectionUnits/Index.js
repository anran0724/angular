(function () {
    $(function () {

        var _$eCCPBaseAnnualInspectionUnitsTable = $('#ECCPBaseAnnualInspectionUnitsTable');
        var _eCCPBaseAnnualInspectionUnitsService = abp.services.app.eCCPBaseAnnualInspectionUnits;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBaseAnnualInspectionUnits.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBaseAnnualInspectionUnits.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBaseAnnualInspectionUnits.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseAnnualInspectionUnits/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseAnnualInspectionUnits/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPBaseAnnualInspectionUnitModal'
        });

        var _viewECCPBaseAnnualInspectionUnitModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseAnnualInspectionUnits/VieweCCPBaseAnnualInspectionUnitModal',
            modalClass: 'ViewECCPBaseAnnualInspectionUnitModal'
        });

        var dataTable = _$eCCPBaseAnnualInspectionUnitsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPBaseAnnualInspectionUnitsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseAnnualInspectionUnitsTableFilter').val(),
                        nameFilter: $('#NameFilterId').val(),
                        addresseFilter: $('#AddresseFilterId').val(),
                        telephoneFilter: $('#TelephoneFilterId').val(),
                        provinceNameFilter: $('#ProvinceNameFilterId').val(),
                        cityNameFilter: $('#CityNameFilterId').val(),
                        districtNameFilter: $('#DistrictNameFilterId').val(),
                        streetNameFilter: $('#StreetNameFilterId').val()
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
                                    _viewECCPBaseAnnualInspectionUnitModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.eccpBaseAnnualInspectionUnit.id });
                                }
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteECCPBaseAnnualInspectionUnit(data.record.eccpBaseAnnualInspectionUnit);
                                }
                            }]
                    }
                },
                {
                    targets: 1,
                    data: "eccpBaseAnnualInspectionUnit.name"
                },
                {
                    targets: 2,
                    data: "eccpBaseAnnualInspectionUnit.addresse"
                },
                {
                    targets: 3,
                    data: "eccpBaseAnnualInspectionUnit.telephone"
                },
                {
                    targets: 4,
                    data: "provinceName"
                },
                {
                    targets: 5,
                    data: "cityName"
                },
                {
                    targets: 6,
                    data: "districtName"
                },
                {
                    targets: 7,
                    data: "streetName"
                }
            ]
        });


        function getECCPBaseAnnualInspectionUnits() {
            dataTable.ajax.reload();
        }

        function deleteECCPBaseAnnualInspectionUnit(eccpBaseAnnualInspectionUnit) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPBaseAnnualInspectionUnitsService.delete({
                            id: eccpBaseAnnualInspectionUnit.id
                        }).done(function () {
                            getECCPBaseAnnualInspectionUnits(true);
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

        $('#CreateNewECCPBaseAnnualInspectionUnitButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _eCCPBaseAnnualInspectionUnitsService
                .getECCPBaseAnnualInspectionUnitsToExcel({
                    filter: $('#ECCPBaseAnnualInspectionUnitsTableFilter').val(),
                    nameFilter: $('#NameFilterId').val(),
                    addresseFilter: $('#AddresseFilterId').val(),
                    telephoneFilter: $('#TelephoneFilterId').val(),
                    provinceNameFilter: $('#ProvinceNameFilterId').val(),
                    cityNameFilter: $('#CityNameFilterId').val(),
                    districtNameFilter: $('#DistrictNameFilterId').val(),
                    streetNameFilter: $('#StreetNameFilterId').val()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditECCPBaseAnnualInspectionUnitModalSaved', function () {
            getECCPBaseAnnualInspectionUnits();
        });

        $('#GetECCPBaseAnnualInspectionUnitsButton').click(function (e) {
            e.preventDefault();
            getECCPBaseAnnualInspectionUnits();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseAnnualInspectionUnits();
            }
        });

    });
})();