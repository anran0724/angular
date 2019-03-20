(function () {
    $(function () {

        var _$eCCPBaseProductionCompaniesTable = $('#ECCPBaseProductionCompaniesTable');
        var _eCCPBaseProductionCompaniesService = abp.services.app.eCCPBaseProductionCompanies;
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBaseProductionCompanies.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBaseProductionCompanies.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBaseProductionCompanies.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseProductionCompanies/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseProductionCompanies/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPBaseProductionCompanyModal'
        });

		 var _viewECCPBaseProductionCompanyModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseProductionCompanies/VieweCCPBaseProductionCompanyModal',
            modalClass: 'ViewECCPBaseProductionCompanyModal'
        });

        var dataTable = _$eCCPBaseProductionCompaniesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPBaseProductionCompaniesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ECCPBaseProductionCompaniesTableFilter').val(),
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
                                   
                                    _viewECCPBaseProductionCompanyModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                
                                _createOrEditModal.open({ id: data.record.eccpBaseProductionCompany.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                
                                deleteECCPBaseProductionCompany(data.record.eccpBaseProductionCompany);
                            }
                        }]
                    }
                },
					{
						targets: 1,
                        data: "eccpBaseProductionCompany.name"   
					},
					{
						targets: 2,
                        data: "eccpBaseProductionCompany.addresse"   
					},
					{
						targets: 3,
                        data: "eccpBaseProductionCompany.telephone"   
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


        function getECCPBaseProductionCompanies() {
            dataTable.ajax.reload();
        }

        function deleteECCPBaseProductionCompany(eCCPBaseProductionCompany) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPBaseProductionCompaniesService.delete({
                            id: eCCPBaseProductionCompany.id
                        }).done(function () {
                            getECCPBaseProductionCompanies(true);
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

        $('#CreateNewECCPBaseProductionCompanyButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eCCPBaseProductionCompaniesService
                .getECCPBaseProductionCompaniesToExcel({
				filter : $('#ECCPBaseProductionCompaniesTableFilter').val(),
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

        abp.event.on('app.createOrEditECCPBaseProductionCompanyModalSaved', function () {
            getECCPBaseProductionCompanies();
        });

		$('#GetECCPBaseProductionCompaniesButton').click(function (e) {
            e.preventDefault();
            getECCPBaseProductionCompanies();
        });

        $('#RefreshProductionButton').click(function (e) {
            e.preventDefault();
            getECCPBaseProductionCompanies();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getECCPBaseProductionCompanies();
		  }
		});

    });
})();