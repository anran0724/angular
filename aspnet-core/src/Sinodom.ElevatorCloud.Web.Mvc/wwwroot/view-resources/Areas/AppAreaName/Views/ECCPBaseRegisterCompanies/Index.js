(function () {
    $(function () {

        var _$eCCPBaseRegisterCompaniesTable = $('#ECCPBaseRegisterCompaniesTable');
        var _eCCPBaseRegisterCompaniesService = abp.services.app.eCCPBaseRegisterCompanies;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBaseRegisterCompanies.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBaseRegisterCompanies.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBaseRegisterCompanies.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseRegisterCompanies/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseRegisterCompanies/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPBaseRegisterCompanyModal'
        });

		 var _viewECCPBaseRegisterCompanyModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseRegisterCompanies/VieweCCPBaseRegisterCompanyModal',
            modalClass: 'ViewECCPBaseRegisterCompanyModal'
        });

        var dataTable = _$eCCPBaseRegisterCompaniesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPBaseRegisterCompaniesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ECCPBaseRegisterCompaniesTableFilter').val(),
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
                                    _viewECCPBaseRegisterCompanyModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpBaseRegisterCompany.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteECCPBaseRegisterCompany(data.record.eccpBaseRegisterCompany);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpBaseRegisterCompany.name"   
					},
					{
						targets: 2,
                        data: "eccpBaseRegisterCompany.addresse"   
					},
					{
						targets: 3,
                        data: "eccpBaseRegisterCompany.telephone"   
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


        function getECCPBaseRegisterCompanies() {
            dataTable.ajax.reload();
        }

        function deleteECCPBaseRegisterCompany(eCCPBaseRegisterCompany) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPBaseRegisterCompaniesService.delete({
                            id: eCCPBaseRegisterCompany.id
                        }).done(function () {
                            getECCPBaseRegisterCompanies(true);
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

        $('#CreateNewECCPBaseRegisterCompanyButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eCCPBaseRegisterCompaniesService
                .getECCPBaseRegisterCompaniesToExcel({
				filter : $('#ECCPBaseRegisterCompaniesTableFilter').val(),
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

        abp.event.on('app.createOrEditECCPBaseRegisterCompanyModalSaved', function () {
            getECCPBaseRegisterCompanies();
        });

		$('#GetECCPBaseRegisterCompaniesButton').click(function (e) {
            e.preventDefault();
            getECCPBaseRegisterCompanies();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getECCPBaseRegisterCompanies();
		  }
		});

    });
})();