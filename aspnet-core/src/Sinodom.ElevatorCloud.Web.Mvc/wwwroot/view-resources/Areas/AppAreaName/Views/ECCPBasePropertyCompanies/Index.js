(function () {
    $(function () {

        var _$eCCPBasePropertyCompaniesTable = $('#ECCPBasePropertyCompaniesTable');
        var _eCCPBasePropertyCompaniesService = abp.services.app.eCCPBasePropertyCompanies;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBasePropertyCompanies.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBasePropertyCompanies.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBasePropertyCompanies.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBasePropertyCompanies/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBasePropertyCompanies/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPBasePropertyCompanyModal'
        });

		 var _viewECCPBasePropertyCompanyModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBasePropertyCompanies/VieweCCPBasePropertyCompanyModal',
            modalClass: 'ViewECCPBasePropertyCompanyModal'
        });

        var dataTable = _$eCCPBasePropertyCompaniesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPBasePropertyCompaniesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ECCPBasePropertyCompaniesTableFilter').val(),
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
                                    _viewECCPBasePropertyCompanyModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpBasePropertyCompany.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteECCPBasePropertyCompany(data.record.eccpBasePropertyCompany);
                            }
                        }]
                    }
                },
					{
						targets: 1,
                        data: "eccpBasePropertyCompany.name"   
					},
					{
						targets: 2,
                        data: "eccpBasePropertyCompany.addresse"   
					},
					{
						targets: 3,
                        data: "eccpBasePropertyCompany.longitude"   
					},
					{
						targets: 4,
                        data: "eccpBasePropertyCompany.latitude"   
					},
					{
						targets: 5,
                        data: "eccpBasePropertyCompany.telephone"   
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


        function getECCPBasePropertyCompanies() {
            dataTable.ajax.reload();
        }

        function deleteECCPBasePropertyCompany(eCCPBasePropertyCompany) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPBasePropertyCompaniesService.delete({
                            id: eCCPBasePropertyCompany.id
                        }).done(function () {
                            getECCPBasePropertyCompanies(true);
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

        $('#CreateNewECCPBasePropertyCompanyButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eCCPBasePropertyCompaniesService
                .getECCPBasePropertyCompaniesToExcel({
				filter : $('#ECCPBasePropertyCompaniesTableFilter').val(),
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

        abp.event.on('app.createOrEditECCPBasePropertyCompanyModalSaved', function () {
            getECCPBasePropertyCompanies();
        });

		$('#GetECCPBasePropertyCompaniesButton').click(function (e) {
            e.preventDefault();
            getECCPBasePropertyCompanies();
        });

        $('#RefreshPropertyButton').click(function (e) {
            e.preventDefault();
            getECCPBasePropertyCompanies();
        });


		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getECCPBasePropertyCompanies();
		  }
		});

    });
})();