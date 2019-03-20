(function () {
    $(function () {

        var _$eccpElevatorQrCodesTable = $('#EccpElevatorQrCodesTable');
        var _eccpElevatorQrCodesService = abp.services.app.eccpElevatorQrCodes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpElevator.EccpElevatorQrCodes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpElevator.EccpElevatorQrCodes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpElevator.EccpElevatorQrCodes.Delete')
        };
        var _EccpElevatorQrCodeBindLogs = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/EccpElevatorQrCodeBindLogs',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorQrCodes/_EccpElevatorQrCodeBindLogs.js',
            modalClass: 'EccpElevatorQrCodeBindLogs'
        });
        var _ModifyQRCode = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/ModifyQRCode',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorQrCodes/_ModifyQRCode.js',
            modalClass: 'CreateOrEditEccpElevatorQrCodeModal'
        });
        var _Modify = new app.ModalManager({
             viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/Modify',
             scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorQrCodes/_Modify.js',
            modalClass: 'CreateOrEditEccpElevatorQrCodeModal'
        });
        var _ModifyEccp = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/ModifyEccp',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorQrCodes/_ModifyEccp.js',
            modalClass: 'CreateOrEditEccpElevatorQrCodeModal'
        });
        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorQrCodes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpElevatorQrCodeModal'
        });
        var _createOrEditModals = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/CreateOrEditModals',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorQrCodes/_CreateOrEditModals.js',
            modalClass: 'CreateOrEditEccpElevatorQrCodeModal'
        });
		 var _viewEccpElevatorQrCodeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/VieweccpElevatorQrCodeModal',
            modalClass: 'ViewEccpElevatorQrCodeModal'
        });
        var _Binding = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/Binding',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorQrCodes/_Binding.js',
            modalClass: 'CreateOrEditEccpElevatorQrCodeModal'
        });

            var dataTable = _$eccpElevatorQrCodesTable.DataTable({
                paging: true,
                serverSide: true,
                processing: true,
                listAction: {
                    ajaxFunction: _eccpElevatorQrCodesService.getAll,
                    inputFilter: function() {
                        return {
                            filter: $('#EccpElevatorQrCodesTableFilter').val(),
                            areaNameFilter: $('#AreaNameFilterId').val(),
                            elevatorNumFilter: $('#ElevatorNumFilterId').val(),
                            isInstallFilter: $('#IsInstallFilterId').val(),
                            isGrantFilter: $('#IsGrantFilterId').val(),
                            minInstallDateTimeFilter: $('#MinInstallDateTimeFilterId').val(),
                            maxInstallDateTimeFilter: $('#MaxInstallDateTimeFilterId').val(),
                            minGrantDateTimeFilter: $('#MinGrantDateTimeFilterId').val(),
                            maxGrantDateTimeFilter: $('#MaxGrantDateTimeFilterId').val(),
                            eccpBaseElevatorNameFilter: $('#EccpBaseElevatorNameFilterId').val()
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
                            text: '<i class="fa fa-cog"></i> ' +
                                app.localize('Actions') +
                                ' <span class="caret"></span>',
                            items: [
                                {
                                    text: app.localize('View'),
                                    action: function(data) {
                                        _viewEccpElevatorQrCodeModal.open({ data: data.record });
                                    }
                                },
                                {
                                    text: app.localize('Edit'),
                                    visible: function() {
                                        return _permissions.edit;
                                    },
                                    action: function(data) {
                                        _createOrEditModals.open({ id: data.record.eccpElevatorQrCode.id });
                                    }
                                },
                                {
                                    text: app.localize('Delete'),
                                    visible: function() {
                                        return _permissions.delete;
                                    },
                                    action: function(data) {
                                        deleteEccpElevatorQrCode(data.record.eccpElevatorQrCode);
                                    }
                                }
                                ,
                                {
                                    text: app.localize('Binding'),
                                    visible: function () {
                                        return _permissions.edit;
                                    },
                                    action: function (data) {
                                        _Binding.open({ id: data.record.eccpElevatorQrCode.id });
                                    }


                                },
                                {
                                    text: app.localize('ModifyEccp'),
                                    visible: function () {
                                        return _permissions.edit;
                                    },
                                    action: function (data) {
                                        _ModifyEccp.open({ id: data.record.eccpElevatorQrCode.id });
                                    }


                                }
                                ,
                                {
                                    text: app.localize('ModifyQRCode'),
                                    visible: function () {
                                        return _permissions.edit;
                                    },
                                    action: function (data) {
                                        _ModifyQRCode.open({ id: data.record.eccpElevatorQrCode.id});
                                    }


                                }
                                ,
                                {
                                    text: app.localize('Modify'),
                                    visible: function () {
                                        return _permissions.edit;
                                    },
                                    action: function (data) {
                                        _Modify.open({ id: data.record.eccpElevatorQrCode.id });
                                    }


                                }
                                ,
                                {
                                    text: app.localize('BindingQrCodeJournal'),
                                    visible: function () {
                                        return _permissions.edit;
                                    },
                                    action: function (data) {
                           
                                        _EccpElevatorQrCodeBindLogs.open({ id: data.record.eccpElevatorQrCode.id, NewElevatorId: data.record.eccpElevatorQrCode.elevatorId ,newid: data.record.eccpElevatorQrCode.id});
                                    }


                                }
                            ]
                        }
                    },
                    {
                        targets: 1,
                        data: "eccpElevatorQrCode.areaName"
                    },
                    {
                        targets: 2,
                        data: "eccpElevatorQrCode.elevatorNum"
                    },
                    {
                        targets: 3,
                        data: "eccpElevatorQrCode.imgPicture",
                     
                        render: function (isInstall) {
                            if (isInstall ==null) {
                                return '';
                            }
                            console.log(isInstall);
                            return '<img class="double-border" style="width:50%" src="/AppAreaName/EccpElevatorQrCodes/GetProfilePictureById/' + isInstall + '">';
                        }

					},
					{
						targets: 4,
						 data: "eccpElevatorQrCode.isInstall"  ,
						render: function (isInstall) {
							if (isInstall) {
								return '<div class="text-center"><i class="fa fa-check-circle m--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 5,
						 data: "eccpElevatorQrCode.isGrant"  ,
						render: function (isGrant) {
							if (isGrant) {
								return '<div class="text-center"><i class="fa fa-check-circle m--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 6,
						 data: "eccpElevatorQrCode.installDateTime" ,
					render: function (installDateTime) {
						if (installDateTime) {
							return moment(installDateTime).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 7,
						 data: "eccpElevatorQrCode.grantDateTime" ,
					render: function (grantDateTime) {
						if (grantDateTime) {
							return moment(grantDateTime).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 8,
						 data: "eccpBaseElevatorName" 
					}
            ]
        });


        function getEccpElevatorQrCodes() {
            dataTable.ajax.reload();
        }

        function deleteEccpElevatorQrCode(eccpElevatorQrCode) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpElevatorQrCodesService.delete({
                            id: eccpElevatorQrCode.id
                        }).done(function () {
                            getEccpElevatorQrCodes(true);
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

        $('#CreateNewEccpElevatorQrCodeButton').click(function () {
            _createOrEditModal.open();
        });

		$('#ExportToExcelButton').click(function () {
            _eccpElevatorQrCodesService
                .getEccpElevatorQrCodesToExcel({
				filter : $('#EccpElevatorQrCodesTableFilter').val(),
					areaNameFilter: $('#AreaNameFilterId').val(),
					elevatorNumFilter: $('#ElevatorNumFilterId').val(),
					isInstallFilter: $('#IsInstallFilterId').val(),
					isGrantFilter: $('#IsGrantFilterId').val(),
					minInstallDateTimeFilter: $('#MinInstallDateTimeFilterId').val(),
					maxInstallDateTimeFilter: $('#MaxInstallDateTimeFilterId').val(),
					minGrantDateTimeFilter: $('#MinGrantDateTimeFilterId').val(),
					maxGrantDateTimeFilter: $('#MaxGrantDateTimeFilterId').val(),
					eccpBaseElevatorNameFilter: $('#EccpBaseElevatorNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpElevatorQrCodeModalSaved', function () {
            getEccpElevatorQrCodes();
        });

		$('#GetEccpElevatorQrCodesButton').click(function (e) {
            e.preventDefault();
            getEccpElevatorQrCodes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpElevatorQrCodes();
		  }
		});

    });
})();