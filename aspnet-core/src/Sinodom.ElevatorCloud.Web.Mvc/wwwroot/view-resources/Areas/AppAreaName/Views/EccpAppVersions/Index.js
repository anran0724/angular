(function () {
    $(function () {

        var _$eccpAppVersionsTable = $('#EccpAppVersionsTable');
        var _eccpAppVersionsService = abp.services.app.eccpAppVersions;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.EccpAppVersions.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.EccpAppVersions.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.EccpAppVersions.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpAppVersions/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpAppVersions/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpAppVersionModal'
        });

		 var _viewEccpAppVersionModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpAppVersions/VieweccpAppVersionModal',
            modalClass: 'ViewEccpAppVersionModal'
        });

        var dataTable = _$eccpAppVersionsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpAppVersionsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpAppVersionsTableFilter').val()
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
                                    _viewEccpAppVersionModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpAppVersion.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpAppVersion(data.record.eccpAppVersion);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpAppVersion.versionName"   
					},
					{
						targets: 2,
						 data: "eccpAppVersion.versionCode"   
					},
					{
						targets: 3,
						 data: "eccpAppVersion.updateLog"   
					},
					{
						targets: 4,
						 data: "eccpAppVersion.downloadUrl"   
					},
					{
						targets: 5,
						 data: "eccpAppVersion.versionType"   
					}
            ]
        });


        function getEccpAppVersions() {
            dataTable.ajax.reload();
        }

        function deleteEccpAppVersion(eccpAppVersion) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpAppVersionsService.delete({
                            id: eccpAppVersion.id
                        }).done(function () {
                            getEccpAppVersions(true);
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

        $('#CreateNewEccpAppVersionButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpAppVersionModalSaved', function () {
            getEccpAppVersions();
        });

		$('#GetEccpAppVersionsButton').click(function (e) {
            e.preventDefault();
            getEccpAppVersions();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpAppVersions();
		  }
		});

    });
})();