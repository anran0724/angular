(function () {
    $(function () {

        var _$eCCPBaseMaintenanceCompaniesTable = $('#ECCPBaseMaintenanceCompaniesTable');
        var _eCCPBaseMaintenanceCompaniesService = abp.services.app.eCCPBaseMaintenanceCompanies;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBaseMaintenanceCompanies.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBaseMaintenanceCompanies.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBaseMaintenanceCompanies.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseMaintenanceCompanies/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseMaintenanceCompanies/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPBaseMaintenanceCompanyModal'
        });

		 var _viewECCPBaseMaintenanceCompanyModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseMaintenanceCompanies/VieweCCPBaseMaintenanceCompanyModal',
            modalClass: 'ViewECCPBaseMaintenanceCompanyModal'
        });

        var dataTable = _$eCCPBaseMaintenanceCompaniesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPBaseMaintenanceCompaniesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ECCPBaseMaintenanceCompaniesTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
					addresseFilter: $('#AddresseFilterId').val(),
					longitudeFilter: $('#LongitudeFilterId').val(),
					latitudeFilter: $('#LatitudeFilterId').val(),
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
                                    _viewECCPBaseMaintenanceCompanyModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpBaseMaintenanceCompany.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteECCPBaseMaintenanceCompany(data.record.eccpBaseMaintenanceCompany);
                            }
                        }]
                    }
                },
					{
						targets: 1,
                        data: "eccpBaseMaintenanceCompany.name"   
					},
					{
						targets: 2,
                        data: "eccpBaseMaintenanceCompany.addresse"   
					},
					{
						targets: 3,
                        data: "eccpBaseMaintenanceCompany.longitude"   
					},
					{
						targets: 4,
                        data: "eccpBaseMaintenanceCompany.latitude"   
					},
					{
						targets: 5,
                        data: "eccpBaseMaintenanceCompany.telephone"   
					},
					{
						targets: 6,
                        data: "provinceName" 
					},
					{
						targets: 7,
                        data: "cityName" 
					},
					{
						targets: 8,
                        data: "districtName" 
					},
					{
						targets: 9,
                        data: "streetName" 
					}
            ]
        });


        function getECCPBaseMaintenanceCompanies() {
            dataTable.ajax.reload();
        }

        function deleteECCPBaseMaintenanceCompany(eccpBaseMaintenanceCompany) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPBaseMaintenanceCompaniesService.delete({
                            id: eccpBaseMaintenanceCompany.id
                        }).done(function () {
                            getECCPBaseMaintenanceCompanies(true);
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

        $('#CreateNewECCPBaseMaintenanceCompanyButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eCCPBaseMaintenanceCompaniesService
                .getECCPBaseMaintenanceCompaniesToExcel({
				filter : $('#ECCPBaseMaintenanceCompaniesTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
					addresseFilter: $('#AddresseFilterId').val(),
					longitudeFilter: $('#LongitudeFilterId').val(),
					latitudeFilter: $('#LatitudeFilterId').val(),
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

        abp.event.on('app.createOrEditECCPBaseMaintenanceCompanyModalSaved', function () {
            getECCPBaseMaintenanceCompanies();
        });

		$('#GetECCPBaseMaintenanceCompaniesButton').click(function (e) {
            e.preventDefault();
            getECCPBaseMaintenanceCompanies();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getECCPBaseMaintenanceCompanies();
		  }
		});

    });
})();