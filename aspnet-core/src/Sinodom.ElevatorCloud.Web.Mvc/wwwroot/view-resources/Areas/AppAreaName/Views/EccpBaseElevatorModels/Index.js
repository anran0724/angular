(function () {
    $(function () {

        var _$eccpBaseElevatorModelsTable = $('#EccpBaseElevatorModelsTable');
        var _eccpBaseElevatorModelsService = abp.services.app.eccpBaseElevatorModels;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBaseElevatorModels.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBaseElevatorModels.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBaseElevatorModels.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorModels/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorModels/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpBaseElevatorModelModal'
        });

		 var _viewEccpBaseElevatorModelModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorModels/VieweccpBaseElevatorModelModal',
            modalClass: 'ViewEccpBaseElevatorModelModal'
        });

        var dataTable = _$eccpBaseElevatorModelsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorModelsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpBaseElevatorModelsTableFilter').val(),
					eccpBaseElevatorBrandNameFilter: $('#EccpBaseElevatorBrandNameFilterId').val()
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
                                    _viewEccpBaseElevatorModelModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpBaseElevatorModel.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpBaseElevatorModel(data.record.eccpBaseElevatorModel);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpBaseElevatorModel.name"   
					},
					{
						targets: 2,
						 data: "eccpBaseElevatorBrandName" 
					}
            ]
        });


        function getEccpBaseElevatorModels() {
            dataTable.ajax.reload();
        }

        function deleteEccpBaseElevatorModel(eccpBaseElevatorModel) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpBaseElevatorModelsService.delete({
                            id: eccpBaseElevatorModel.id
                        }).done(function () {
                            getEccpBaseElevatorModels(true);
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

        $('#CreateNewEccpBaseElevatorModelButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eccpBaseElevatorModelsService
                .getEccpBaseElevatorModelsToExcel({
				filter : $('#EccpBaseElevatorModelsTableFilter').val(),
					eccpBaseElevatorBrandNameFilter: $('#EccpBaseElevatorBrandNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpBaseElevatorModelModalSaved', function () {
            getEccpBaseElevatorModels();
        });

		$('#GetEccpBaseElevatorModelsButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevatorModels();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpBaseElevatorModels();
		  }
		});

    });
})();