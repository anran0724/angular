(function () {
    $(function () {

        var _$eccpDictMaintenanceTypesTable = $('#EccpDictMaintenanceTypesTable');
        var _eccpDictMaintenanceTypesService = abp.services.app.eccpDictMaintenanceTypes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceTypes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceTypes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceTypes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictMaintenanceTypes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictMaintenanceTypes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictMaintenanceTypeModal'
        });

		 var _viewEccpDictMaintenanceTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictMaintenanceTypes/VieweccpDictMaintenanceTypeModal',
            modalClass: 'ViewEccpDictMaintenanceTypeModal'
        });

        var dataTable = _$eccpDictMaintenanceTypesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictMaintenanceTypesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictMaintenanceTypesTableFilter').val()
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
                                    _viewEccpDictMaintenanceTypeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictMaintenanceType.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictMaintenanceType(data.record.eccpDictMaintenanceType);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictMaintenanceType.name"   
					}
            ]
        });


        function getEccpDictMaintenanceTypes() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictMaintenanceType(eccpDictMaintenanceType) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictMaintenanceTypesService.delete({
                            id: eccpDictMaintenanceType.id
                        }).done(function () {
                            getEccpDictMaintenanceTypes(true);
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

        $('#CreateNewEccpDictMaintenanceTypeButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictMaintenanceTypeModalSaved', function () {
            getEccpDictMaintenanceTypes();
        });

		$('#GetEccpDictMaintenanceTypesButton').click(function (e) {
            e.preventDefault();
            getEccpDictMaintenanceTypes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictMaintenanceTypes();
		  }
		});

    });
})();