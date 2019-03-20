(function ($) {
        
    app.modals.MaintenanceWorkOrderTransfersAuditLogsTableModel = function () {        
        var eccpMaintenanceWorkOrderTransferAuditLogsAppService = abp.services.app.eccpMaintenanceWorkOrderTransferAuditLogs;
        var $maintenanceWorkOrderTransfersAuditLogsTable = $('#MaintenanceWorkOrderTransfersAuditLogsTable');        
        var _ransferAuditLogs;
        
        this.init = function (modalManager, ransferAuditLogs) {
            _ransferAuditLogs = ransferAuditLogs;           
        };

       $maintenanceWorkOrderTransfersAuditLogsTable.DataTable({
               paging: true,
               serverSide: true,
               processing: true,
               listAction: {
                   ajaxFunction: eccpMaintenanceWorkOrderTransferAuditLogsAppService
                       .getMaintenanceWorkOrderTransferAuditLogs,
                   inputFilter: function() {
                       return {
                           category: _ransferAuditLogs.category,
                           id: _ransferAuditLogs.id
                       };

                   }
               },
               columnDefs: [

                   {
                       targets: 0,
                       data: "workOrderTransferAuditState"

                   },
                   {
                       targets: 1,
                       data: "auditUserName"
                   },
                   {
                       targets: 2,
                       data: "auditTime",
                       render: function (auditTime) {
                           if (auditTime) {
                               return moment(auditTime).format('L');
                           }
                           return "";
                       }
                   },
                   {
                       targets: 3,
                       data: "title"
                   },
                   {
                       targets: 4,
                       data: "workOrderTransferType"
                   },
                   {
                       targets: 5,
                       data: "orderTypeName"
                   },
                   {
                       targets: 6,
                       data: "orderCreationTime",
                       render: function(orderCreationTime) {
                           if (orderCreationTime) {
                               return moment(orderCreationTime).format('L');
                           }
                           return "";
                       }

                   },
                   {
                       targets: 7,
                       data: "applicationTransferName"
                   },
                   {
                       targets: 8,
                       data: "applicationTransferCreationTime",
                       render: function(applicationTransferCreationTime) {
                           if (applicationTransferCreationTime) {
                               return moment(applicationTransferCreationTime).format('L');
                           }
                           return "";
                       }
                   },
                   {
                       targets: 9,
                       data: "transferUserName"
                   }
               ]
        });

   

    };
})(jQuery);
