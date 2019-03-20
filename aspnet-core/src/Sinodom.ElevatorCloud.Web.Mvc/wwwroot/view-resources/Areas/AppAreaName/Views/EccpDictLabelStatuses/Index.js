(function () {
    $(function () {

        var _$eccpDictLabelStatusesTable = $('#EccpDictLabelStatusesTable');
        var _eccpDictLabelStatusesService = abp.services.app.eccpDictLabelStatuses;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictLabelStatuses.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictLabelStatuses.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictLabelStatuses.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictLabelStatuses/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictLabelStatuses/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictLabelStatusModal'
        });

		 var _viewEccpDictLabelStatusModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictLabelStatuses/VieweccpDictLabelStatusModal',
            modalClass: 'ViewEccpDictLabelStatusModal'
        });

        var dataTable = _$eccpDictLabelStatusesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictLabelStatusesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictLabelStatusesTableFilter').val()
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
                                    _viewEccpDictLabelStatusModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictLabelStatus.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictLabelStatus(data.record.eccpDictLabelStatus);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictLabelStatus.name"   
					}
            ]
        });


        function getEccpDictLabelStatuses() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictLabelStatus(eccpDictLabelStatus) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictLabelStatusesService.delete({
                            id: eccpDictLabelStatus.id
                        }).done(function () {
                            getEccpDictLabelStatuses(true);
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

        $('#CreateNewEccpDictLabelStatusButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictLabelStatusModalSaved', function () {
            getEccpDictLabelStatuses();
        });

		$('#GetEccpDictLabelStatusesButton').click(function (e) {
            e.preventDefault();
            getEccpDictLabelStatuses();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictLabelStatuses();
		  }
		});

    });
})();