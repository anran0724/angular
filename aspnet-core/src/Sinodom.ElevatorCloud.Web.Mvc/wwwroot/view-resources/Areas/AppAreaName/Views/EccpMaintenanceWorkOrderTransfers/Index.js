(function () {
    $(function () {     
        var $eccpMaintenanceWorkOrderTransfersTable = $('#MaintenanceWorkOrderTransfersTable');
        var eccpMaintenanceWorkOrderTransfersService = abp.services.app.eccpMaintenanceWorkOrderTransfers;       
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
        var permissions = {
            audit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrderTransfers.Audit')
          
        };
        var auditLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrderTransfers/MaintenanceWorkOrderTransfersAuditLogsTableModel',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkOrderTransfers/_MaintenanceWorkOrderTransfersAuditLogsTableModel.js',
            modalClass: 'MaintenanceWorkOrderTransfersAuditLogsTableModel',
            modalWidth:"85%"
        });

        var viewEccpMaintenanceWorkOrderTransferModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrderTransfers/ViewMaintenanceWorkOrderTransfersModel',
            modalClass: 'ViewMaintenanceWorkOrderTransfersModel'
        });

        var dataTable = $eccpMaintenanceWorkOrderTransfersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: eccpMaintenanceWorkOrderTransfersService.getAll,
                inputFilter: function() {
                    return {
                        filter: $('#IsApproved').val()
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

                                    debugger;
                                    viewEccpMaintenanceWorkOrderTransferModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('AuditLogModal'),
                                action: function (data) {
                                    auditLogModal.open({ id: data.record.id, category: data.record.category });
                                }
                            },
                            {
                                text: app.localize('AuditFailed'),
                                visible: function (data) {
                                    if (data.record.isApproved != null) {
                                        return false;
                                    } 
                                    return permissions.audit;
                                },
                                action: function(data) {
                                    maintenanceWorkOrderTransfersAudit({ id: data.record.id, IsApproved: false, category: data.record.category });                                    
                                }
                            },
                            {
                                text: app.localize('AuditPass'),
                                visible: function (data) {
                                    if (data.record.isApproved != null) {
                                        return false;
                                    } 
                                    return permissions.audit;
                                },
                                action: function(data) {
                                    maintenanceWorkOrderTransfersAudit({ id: data.record.id, IsApproved: true, category: data.record.category });
                                }
                            }
                        ]
                    }
                },                
                {
                    targets: 1,
                    data: "title"
                },
                {
                    targets: 2,
                    data: "workOrderTransferType"
                },
                {
                    targets: 3,
                    data: "orderTypeName"
                },
                {
                    targets: 4,
                    data: "orderCreationTime",
                    render: function (orderCreationTime) {
                        if (orderCreationTime) {
                            return moment(orderCreationTime).format('L');
                        }
                        return "";
                    }

                },
                {
                    targets: 5,
                    data: "applicationTransferName"
                },
                {
                    targets: 6,
                    data: "applicationTransferCreationTime",
                    render: function (applicationTransferCreationTime) {
                        if (applicationTransferCreationTime) {
                            return moment(applicationTransferCreationTime).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 7,
                    data: "transferUserName"
                },
                {
                    targets: 8,
                    data: "workOrderTransferAuditState"
                   
                }
            ]
        });

        function maintenanceWorkOrderTransfersAudit(maintenanceWorkOrderTransfers) {
            var msg = "AuditFailed";
            if (maintenanceWorkOrderTransfers.IsApproved) {
                msg = "AuditPass";
            }
            abp.message.confirm(
                app.localize(msg),
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        eccpMaintenanceWorkOrderTransfersService.auditMaintenanceWorkOrderTransfer(maintenanceWorkOrderTransfers).done(function (data) {
                            if (data === 1) {
                                getEccpMaintenanceWorkOrderTransfers();
                                abp.notify.success(app.localize('Successfully' + msg));
                            } else {
                                abp.notify.error(app.localize('error' + msg));
                            }                            
                        });
                    }
                }
            );
        }; 

        function getEccpMaintenanceWorkOrderTransfers() {
            dataTable.ajax.reload();
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
       
		$(document).keypress(function(e) {
		  if(e.which === 13) {
		      getEccpMaintenanceWorkOrderTransfers();
		  }
		});

    });
})();