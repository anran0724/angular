(function () {
    $(function () {

        var _$eccpDictTempWorkOrderTypesTable = $('#EccpDictTempWorkOrderTypesTable');
        var _eccpDictTempWorkOrderTypesService = abp.services.app.eccpDictTempWorkOrderTypes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictTempWorkOrderTypes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictTempWorkOrderTypes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictTempWorkOrderTypes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictTempWorkOrderTypes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictTempWorkOrderTypes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictTempWorkOrderTypeModal'
        });

		 var _viewEccpDictTempWorkOrderTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictTempWorkOrderTypes/VieweccpDictTempWorkOrderTypeModal',
            modalClass: 'ViewEccpDictTempWorkOrderTypeModal'
        });

        var dataTable = _$eccpDictTempWorkOrderTypesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictTempWorkOrderTypesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictTempWorkOrderTypesTableFilter').val()
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
                                    _viewEccpDictTempWorkOrderTypeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictTempWorkOrderType.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictTempWorkOrderType(data.record.eccpDictTempWorkOrderType);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictTempWorkOrderType.name"   
					}
            ]
        });


        function getEccpDictTempWorkOrderTypes() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictTempWorkOrderType(eccpDictTempWorkOrderType) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictTempWorkOrderTypesService.delete({
                            id: eccpDictTempWorkOrderType.id
                        }).done(function () {
                            getEccpDictTempWorkOrderTypes(true);
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

        $('#CreateNewEccpDictTempWorkOrderTypeButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictTempWorkOrderTypeModalSaved', function () {
            getEccpDictTempWorkOrderTypes();
        });

		$('#GetEccpDictTempWorkOrderTypesButton').click(function (e) {
            e.preventDefault();
            getEccpDictTempWorkOrderTypes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictTempWorkOrderTypes();
		  }
		});

    });
})();