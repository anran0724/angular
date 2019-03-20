(function () {
    $(function () {

        var _$eCCPEditionsTypesTable = $('#ECCPEditionsTypesTable');
        var _eCCPEditionsTypesService = abp.services.app.eCCPEditionsTypes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpEditionsTypes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpEditionsTypes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpEditionsTypes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPEditionsTypes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPEditionsTypes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPEditionsTypeModal'
        });

		 var _viewECCPEditionsTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPEditionsTypes/VieweCCPEditionsTypeModal',
            modalClass: 'ViewECCPEditionsTypeModal'
        });

        var dataTable = _$eCCPEditionsTypesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPEditionsTypesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ECCPEditionsTypesTableFilter').val()
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
                                    _viewECCPEditionsTypeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpEditionsType.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteECCPEditionsType(data.record.eccpEditionsType);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpEditionsType.name"   
					}
            ]
        });


        function getECCPEditionsTypes() {
            dataTable.ajax.reload();
        }

        function deleteECCPEditionsType(eccpEditionsType) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPEditionsTypesService.delete({
                            id: eccpEditionsType.id
                        }).done(function () {
                            getECCPEditionsTypes(true);
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

        $('#CreateNewECCPEditionsTypeButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditECCPEditionsTypeModalSaved', function () {
            getECCPEditionsTypes();
        });

		$('#GetECCPEditionsTypesButton').click(function (e) {
            e.preventDefault();
            getECCPEditionsTypes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getECCPEditionsTypes();
		  }
		});

    });
})();