(function () {
    $(function () {

        var _$eccpDictPlaceTypesTable = $('#EccpDictPlaceTypesTable');
        var _eccpDictPlaceTypesService = abp.services.app.eccpDictPlaceTypes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictPlaceTypes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictPlaceTypes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictPlaceTypes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictPlaceTypes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictPlaceTypes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictPlaceTypeModal'
        });

		 var _viewEccpDictPlaceTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictPlaceTypes/VieweccpDictPlaceTypeModal',
            modalClass: 'ViewEccpDictPlaceTypeModal'
        });

        var dataTable = _$eccpDictPlaceTypesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictPlaceTypesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictPlaceTypesTableFilter').val()
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
                                    _viewEccpDictPlaceTypeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictPlaceType.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictPlaceType(data.record.eccpDictPlaceType);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictPlaceType.name"   
					}
            ]
        });


        function getEccpDictPlaceTypes() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictPlaceType(eccpDictPlaceType) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictPlaceTypesService.delete({
                            id: eccpDictPlaceType.id
                        }).done(function () {
                            getEccpDictPlaceTypes(true);
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

        $('#CreateNewEccpDictPlaceTypeButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictPlaceTypeModalSaved', function () {
            getEccpDictPlaceTypes();
        });

		$('#GetEccpDictPlaceTypesButton').click(function (e) {
            e.preventDefault();
            getEccpDictPlaceTypes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictPlaceTypes();
		  }
		});

    });
})();