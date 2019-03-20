(function () {
    $(function () {

        var _$eccpDictElevatorTypesTable = $('#EccpDictElevatorTypesTable');
        var _eccpDictElevatorTypesService = abp.services.app.eccpDictElevatorTypes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictElevatorTypes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictElevatorTypes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictElevatorTypes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictElevatorTypes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictElevatorTypes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictElevatorTypeModal'
        });

		 var _viewEccpDictElevatorTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictElevatorTypes/VieweccpDictElevatorTypeModal',
            modalClass: 'ViewEccpDictElevatorTypeModal'
        });

        var dataTable = _$eccpDictElevatorTypesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictElevatorTypesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictElevatorTypesTableFilter').val()
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
                                    _viewEccpDictElevatorTypeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictElevatorType.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictElevatorType(data.record.eccpDictElevatorType);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictElevatorType.name"   
					}
            ]
        });


        function getEccpDictElevatorTypes() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictElevatorType(eccpDictElevatorType) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictElevatorTypesService.delete({
                            id: eccpDictElevatorType.id
                        }).done(function () {
                            getEccpDictElevatorTypes(true);
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

        $('#CreateNewEccpDictElevatorTypeButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eccpDictElevatorTypesService
                .getEccpDictElevatorTypesToExcel({
				filter : $('#EccpDictElevatorTypesTableFilter').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpDictElevatorTypeModalSaved', function () {
            getEccpDictElevatorTypes();
        });

		$('#GetEccpDictElevatorTypesButton').click(function (e) {
            e.preventDefault();
            getEccpDictElevatorTypes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictElevatorTypes();
		  }
		});

    });
})();