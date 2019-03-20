(function () {
    $(function () {

        var _$eccpBaseElevatorBrandsTable = $('#EccpBaseElevatorBrandsTable');
        var _eccpBaseElevatorBrandsService = abp.services.app.eccpBaseElevatorBrands;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBaseElevatorBrands.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBaseElevatorBrands.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBaseElevatorBrands.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorBrands/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorBrands/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpBaseElevatorBrandModal'
        });

		 var _viewEccpBaseElevatorBrandModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorBrands/VieweccpBaseElevatorBrandModal',
            modalClass: 'ViewEccpBaseElevatorBrandModal'
        });

        var dataTable = _$eccpBaseElevatorBrandsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorBrandsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpBaseElevatorBrandsTableFilter').val(),
					eCCPBaseProductionCompanyNameFilter: $('#ECCPBaseProductionCompanyNameFilterId').val()
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
                                    _viewEccpBaseElevatorBrandModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpBaseElevatorBrand.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpBaseElevatorBrand(data.record.eccpBaseElevatorBrand);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpBaseElevatorBrand.name"   
					},
					{
						targets: 2,
                        data: "eccpBaseProductionCompanyName" 
					}
            ]
        });


        function getEccpBaseElevatorBrands() {
            dataTable.ajax.reload();
        }

        function deleteEccpBaseElevatorBrand(eccpBaseElevatorBrand) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpBaseElevatorBrandsService.delete({
                            id: eccpBaseElevatorBrand.id
                        }).done(function () {
                            getEccpBaseElevatorBrands(true);
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

        $('#CreateNewEccpBaseElevatorBrandButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eccpBaseElevatorBrandsService
                .getEccpBaseElevatorBrandsToExcel({
				filter : $('#EccpBaseElevatorBrandsTableFilter').val(),
					eCCPBaseProductionCompanyNameFilter: $('#ECCPBaseProductionCompanyNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpBaseElevatorBrandModalSaved', function () {
            getEccpBaseElevatorBrands();
        });

		$('#GetEccpBaseElevatorBrandsButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevatorBrands();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpBaseElevatorBrands();
		  }
		});

    });
})();