(function () {
    $(function () {

        var _$eccpDictNodeTypesTable = $('#EccpDictNodeTypesTable');
        var _eccpDictNodeTypesService = abp.services.app.eccpDictNodeTypes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictNodeTypes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictNodeTypes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictNodeTypes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictNodeTypes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictNodeTypes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictNodeTypeModal'
        });

		 var _viewEccpDictNodeTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictNodeTypes/VieweccpDictNodeTypeModal',
            modalClass: 'ViewEccpDictNodeTypeModal'
        });

        var dataTable = _$eccpDictNodeTypesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictNodeTypesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictNodeTypesTableFilter').val()
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
                                    _viewEccpDictNodeTypeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictNodeType.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictNodeType(data.record.eccpDictNodeType);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictNodeType.name"   
					}
            ]
        });


        function getEccpDictNodeTypes() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictNodeType(eccpDictNodeType) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictNodeTypesService.delete({
                            id: eccpDictNodeType.id
                        }).done(function () {
                            getEccpDictNodeTypes(true);
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

        $('#CreateNewEccpDictNodeTypeButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictNodeTypeModalSaved', function () {
            getEccpDictNodeTypes();
        });

		$('#GetEccpDictNodeTypesButton').click(function (e) {
            e.preventDefault();
            getEccpDictNodeTypes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictNodeTypes();
		  }
		});

    });
})();