(function ($) {
    app.modals.ViewEccpCompanyAuditModal = function () {

        var _eccpCompanyAuditsAppService = abp.services.app.eccpCompanyAudits;
        var _$eccpCompanyAuditLogTable = $('#ECCPCompanyAuditLogTable');

        var _args;

        this.init = function (modalManage, args) {
            
            _args = args;

        };

        var dataTable = _$eccpCompanyAuditLogTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpCompanyAuditsAppService.getCompanyAuditLogs,
                inputFilter: function () {
                    return {
                        tenantId: _args.tenantId
                    };
                }
            },
            columnDefs: [
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 0,
                    data: "checkStateName"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "remarks"
                }
            ]
        });

        function getEccpCompanyAuditLogs() {
            dataTable.ajax.reload();
        }

        $('#GetECCPCompanyAuditLogButton').click(function (e) {
            e.preventDefault();
            getEccpCompanyAuditLogs();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpCompanyAuditLogs();
            }
        });

    };
})(jQuery);

