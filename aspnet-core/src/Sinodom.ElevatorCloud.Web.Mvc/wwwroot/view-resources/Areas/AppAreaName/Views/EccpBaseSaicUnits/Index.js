(function () {
    $(function () {

        var _$eccpBaseSaicUnitsTable = $('#EccpBaseSaicUnitsTable');
        var _eccpBaseSaicUnitsService = abp.services.app.eccpBaseSaicUnits;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.EccpBaseSaicUnits.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.EccpBaseSaicUnits.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.EccpBaseSaicUnits.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseSaicUnits/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseSaicUnits/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpBaseSaicUnitModal'
        });

		 var _viewEccpBaseSaicUnitModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseSaicUnits/VieweccpBaseSaicUnitModal',
            modalClass: 'ViewEccpBaseSaicUnitModal'
        });

		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }

        var dataTable = _$eccpBaseSaicUnitsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseSaicUnitsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpBaseSaicUnitsTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
					addressFilter: $('#AddressFilterId').val(),
					telephoneFilter: $('#TelephoneFilterId').val(),
					eccpBaseAreaNameFilter: $('#ECCPBaseAreaNameFilterId').val(),
					eccpBaseAreaName2Filter: $('#ECCPBaseAreaName2FilterId').val(),
					eccpBaseAreaName3Filter: $('#ECCPBaseAreaName3FilterId').val(),
					eccpBaseAreaName4Filter: $('#ECCPBaseAreaName4FilterId').val()
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
                                    _viewEccpBaseSaicUnitModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpBaseSaicUnit.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpBaseSaicUnit(data.record.eccpBaseSaicUnit);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpBaseSaicUnit.name"   
					},
					{
						targets: 2,
						 data: "eccpBaseSaicUnit.address"   
					},
					{
						targets: 3,
						 data: "eccpBaseSaicUnit.telephone"   
					},
					{
						targets: 4,
						 data: "eccpBaseAreaName" 
					},
					{
						targets: 5,
						 data: "eccpBaseAreaName2" 
					},
					{
						targets: 6,
						 data: "eccpBaseAreaName3" 
					},
					{
						targets: 7,
						 data: "eccpBaseAreaName4" 
					}
            ]
        });


        function getEccpBaseSaicUnits() {
            dataTable.ajax.reload();
        }

        function deleteEccpBaseSaicUnit(eccpBaseSaicUnit) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpBaseSaicUnitsService.delete({
                            id: eccpBaseSaicUnit.id
                        }).done(function () {
                            getEccpBaseSaicUnits(true);
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

        $('#CreateNewEccpBaseSaicUnitButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpBaseSaicUnitModalSaved', function () {
            getEccpBaseSaicUnits();
        });

		$('#GetEccpBaseSaicUnitsButton').click(function (e) {
            e.preventDefault();
            getEccpBaseSaicUnits();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpBaseSaicUnits();
		  }
		});

    });
})();