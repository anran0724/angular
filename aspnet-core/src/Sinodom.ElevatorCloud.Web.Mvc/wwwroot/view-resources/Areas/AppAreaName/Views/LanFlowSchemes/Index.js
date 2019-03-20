(function () {
    $(function () {

        var _$lanFlowSchemesTable = $('#LanFlowSchemesTable');
        var _lanFlowSchemesService = abp.services.app.lanFlowSchemes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.LanFlowSchemes.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.LanFlowSchemes.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.LanFlowSchemes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/LanFlowSchemes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/LanFlowSchemes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditLanFlowSchemeModal'
        });

		 var _viewLanFlowSchemeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/LanFlowSchemes/ViewlanFlowSchemeModal',
            modalClass: 'ViewLanFlowSchemeModal'
        });

        var dataTable = _$lanFlowSchemesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _lanFlowSchemesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#LanFlowSchemesTableFilter').val(),
					schemeTypeFilter: $('#SchemeTypeFilterId').val(),
					tableNameFilter: $('#TableNameFilterId').val(),
					minAuthorizeTypeFilter: $('#MinAuthorizeTypeFilterId').val(),
					maxAuthorizeTypeFilter: $('#MaxAuthorizeTypeFilterId').val()
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
                                    _viewLanFlowSchemeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.lanFlowScheme.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteLanFlowScheme(data.record.lanFlowScheme);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "lanFlowScheme.schemeName"   
					},
					{
						targets: 2,
						 data: "lanFlowScheme.schemeType"   
					},
					{
						targets: 3,
						 data: "lanFlowScheme.schemeContent"   
					},
					{
						targets: 4,
						 data: "lanFlowScheme.tableName"   
					},
					{
						targets: 5,
						 data: "lanFlowScheme.authorizeType"   
					},
					{
						targets: 6,
						 data: "lanFlowScheme.sortCode"   
					}
            ]
        });


        function getLanFlowSchemes() {
            dataTable.ajax.reload();
        }

        function deleteLanFlowScheme(lanFlowScheme) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _lanFlowSchemesService.delete({
                            id: lanFlowScheme.id
                        }).done(function () {
                            getLanFlowSchemes(true);
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

        $('#CreateNewLanFlowSchemeButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditLanFlowSchemeModalSaved', function () {
            getLanFlowSchemes();
        });

		$('#GetLanFlowSchemesButton').click(function (e) {
            e.preventDefault();
            getLanFlowSchemes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getLanFlowSchemes();
		  }
		});

    });
})();