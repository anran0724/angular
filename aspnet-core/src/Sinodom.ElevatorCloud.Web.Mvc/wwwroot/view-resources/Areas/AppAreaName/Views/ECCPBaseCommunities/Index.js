(function () {
    $(function () {

        var _$eCCPBaseCommunitiesTable = $('#ECCPBaseCommunitiesTable');
        var _eCCPBaseCommunitiesService = abp.services.app.eCCPBaseCommunities;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBaseCommunities.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBaseCommunities.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBaseCommunities.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseCommunities/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseCommunities/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPBaseCommunityModal'
        });

		 var _viewECCPBaseCommunityModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseCommunities/VieweCCPBaseCommunityModal',
            modalClass: 'ViewECCPBaseCommunityModal'
        });

        var dataTable = _$eCCPBaseCommunitiesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPBaseCommunitiesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ECCPBaseCommunitiesTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
					addressFilter: $('#AddressFilterId').val(),
					longitudeFilter: $('#LongitudeFilterId').val(),
					latitudeFilter: $('#LatitudeFilterId').val(),
					provinceNameFilter: $('#ProvinceNameFilterId').val(),
					cityNameFilter: $('#CityNameFilterId').val(),
					districtNameFilter: $('#DistrictNameFilterId').val(),
					streetNameFilter: $('#StreetNameFilterId').val()
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
                                    _viewECCPBaseCommunityModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpBaseCommunity.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteECCPBaseCommunity(data.record.eccpBaseCommunity);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpBaseCommunity.name"   
					},
					{
						targets: 2,
                        data: "eccpBaseCommunity.address"   
					},
					{
						targets: 3,
                        data: "eccpBaseCommunity.longitude"   
					},
					{
						targets: 4,
                        data: "eccpBaseCommunity.latitude"   
					},
					{
						targets: 5,
						 data: "provinceName" 
					},
					{
						targets: 6,
                        data: "cityName" 
					},
					{
						targets: 7,
                        data: "districtName" 
					},
					{
						targets: 8,
                        data: "streetName" 
					}
            ]
        });


        function getECCPBaseCommunities() {
            dataTable.ajax.reload();
        }

        function deleteECCPBaseCommunity(eccpBaseCommunity) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPBaseCommunitiesService.delete({
                            id:eccpBaseCommunity.id
                        }).done(function () {
                            getECCPBaseCommunities(true);
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

        $('#CreateNewECCPBaseCommunityButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eCCPBaseCommunitiesService
                .getECCPBaseCommunitiesToExcel({
				filter : $('#ECCPBaseCommunitiesTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
					addressFilter: $('#AddressFilterId').val(),
					longitudeFilter: $('#LongitudeFilterId').val(),
					latitudeFilter: $('#LatitudeFilterId').val(),
					provinceNameFilter: $('#ProvinceNameFilterId').val(),
					cityNameFilter: $('#CityNameFilterId').val(),
					districtNameFilter: $('#DistrictNameFilterId').val(),
					streetNameFilter: $('#StreetNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditECCPBaseCommunityModalSaved', function () {
            getECCPBaseCommunities();
        });

		$('#GetECCPBaseCommunitiesButton').click(function (e) {
            e.preventDefault();
            getECCPBaseCommunities();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getECCPBaseCommunities();
		  }
		});

    });
})();