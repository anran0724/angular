(function () {
    $(function () {

        var _$eccpDictWorkOrderTypesTable = $('#EccpDictWorkOrderTypesTable');
        var _eccpDictWorkOrderTypesService = abp.services.app.eccpDictWorkOrderTypes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictWorkOrderTypes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictWorkOrderTypes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictWorkOrderTypes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictWorkOrderTypes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictWorkOrderTypes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictWorkOrderTypeModal'
        });

		 var _viewEccpDictWorkOrderTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictWorkOrderTypes/VieweccpDictWorkOrderTypeModal',
            modalClass: 'ViewEccpDictWorkOrderTypeModal'
        });

        var dataTable = _$eccpDictWorkOrderTypesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictWorkOrderTypesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictWorkOrderTypesTableFilter').val()
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
                                    _viewEccpDictWorkOrderTypeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictWorkOrderType.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictWorkOrderType(data.record.eccpDictWorkOrderType);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictWorkOrderType.name"   
					}
            ]
        });


        function getEccpDictWorkOrderTypes() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictWorkOrderType(eccpDictWorkOrderType) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictWorkOrderTypesService.delete({
                            id: eccpDictWorkOrderType.id
                        }).done(function () {
                            getEccpDictWorkOrderTypes(true);
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

        $('#CreateNewEccpDictWorkOrderTypeButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictWorkOrderTypeModalSaved', function () {
            getEccpDictWorkOrderTypes();
        });

		$('#GetEccpDictWorkOrderTypesButton').click(function (e) {
            e.preventDefault();
            getEccpDictWorkOrderTypes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictWorkOrderTypes();
		  }
		});

    });
})();